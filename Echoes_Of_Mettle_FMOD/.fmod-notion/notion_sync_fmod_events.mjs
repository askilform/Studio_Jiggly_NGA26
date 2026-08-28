#!/usr/bin/env node
/**
 * Project-local FMOD -> Notion sync worker.
 * This file is copied into <FMOD_PROJECT>/.fmod-notion/ by install_for_project.mjs.
 * It reads configuration from environment variables, usually loaded by sync_from_fmod.sh.
 */

import fs from "node:fs";
import path from "node:path";

const NOTION_VERSION = process.env.NOTION_VERSION || "2026-03-11";
const token = process.env.NOTION_TOKEN;
const dataSourceId = process.env.NOTION_DATA_SOURCE_ID;
const registryPath = process.env.FMOD_REGISTRY_PATH || "./fmod-event-registry.json";
const archiveMissing = /^true$/i.test(process.env.ARCHIVE_MISSING || "");

if (!token || !dataSourceId) {
  console.error("Missing NOTION_TOKEN or NOTION_DATA_SOURCE_ID.");
  process.exit(1);
}

function cleanId(id) {
  return String(id || "").trim().replace(/-/g, "");
}

function truncate(text, max = 2000) {
  const value = String(text ?? "");
  return value.length > max ? value.slice(0, max - 1) + "…" : value;
}

function optionName(value) {
  return truncate(String(value ?? "").replace(/[\n\r]/g, " ").trim() || "None", 100);
}

function textProp(value) {
  const content = truncate(value);
  return content ? { rich_text: [{ type: "text", text: { content } }] } : { rich_text: [] };
}

function titleProp(value) {
  return { title: [{ type: "text", text: { content: truncate(value || "Untitled FMOD Event") } }] };
}

function selectProp(value) {
  return { select: value ? { name: optionName(value) } : null };
}

function multiSelectProp(values) {
  const seen = new Set();
  const options = [];

  for (const raw of values || []) {
    const name = optionName(raw);
    if (!name || seen.has(name)) continue;
    seen.add(name);
    options.push({ name });
    if (options.length >= 100) break;
  }

  return { multi_select: options };
}

function dateProp(value) {
  return { date: { start: value || new Date().toISOString() } };
}

function getPlainText(prop) {
  if (!prop) return "";
  if (prop.type === "rich_text") return (prop.rich_text || []).map(x => x.plain_text || x.text?.content || "").join("");
  if (prop.type === "title") return (prop.title || []).map(x => x.plain_text || x.text?.content || "").join("");
  return "";
}

function sleep(ms) {
  return new Promise(resolve => setTimeout(resolve, ms));
}

async function notion(pathname, options = {}, retry = 0) {
  const res = await fetch(`https://api.notion.com/v1${pathname}`, {
    method: options.method || "GET",
    headers: {
      Authorization: `Bearer ${token}`,
      "Notion-Version": NOTION_VERSION,
      "Content-Type": "application/json",
      ...(options.headers || {})
    },
    body: options.body ? JSON.stringify(options.body) : undefined
  });

  const text = await res.text();
  let json = {};
  if (text) {
    try {
      json = JSON.parse(text);
    } catch {
      json = { raw: text };
    }
  }

  if ((res.status === 429 || res.status === 529) && retry < 5) {
    const retryAfter = Number(res.headers.get("retry-after") || "1");
    await sleep(retryAfter * 1000 + 300);
    return notion(pathname, options, retry + 1);
  }

  if (!res.ok) {
    throw new Error(`${res.status} ${res.statusText}: ${JSON.stringify(json, null, 2)}`);
  }

  return json;
}

async function queryAllPages() {
  const pages = [];
  let start_cursor;

  do {
    const body = { page_size: 100 };
    if (start_cursor) body.start_cursor = start_cursor;

    const result = await notion(`/data_sources/${cleanId(dataSourceId)}/query`, {
      method: "POST",
      body
    });

    pages.push(...(result.results || []));
    start_cursor = result.has_more ? result.next_cursor : undefined;
  } while (start_cursor);

  return pages;
}

function eventWarnings(event) {
  const warnings = new Set(event.warnings || []);
  if (!event.path?.startsWith("event:/")) warnings.add("Path is missing or invalid");
  if (!event.banks || event.banks.length === 0) warnings.add("Event is not assigned to a bank");
  if (!event.category || event.category === "Uncategorised") warnings.add("No category folder in event path");
  return [...warnings];
}

function markerSummary(event) {
  const markers = event.markers || [];
  if (!markers.length) return "";
  return markers.slice(0, 30).map(marker => marker.name || "Marker").join(", ");
}

function eventProperties(event, exportedAt, isCreate = false) {
  const bankNames = (event.banks || []).map(bank => bank.path || bank.name).filter(Boolean);
  const parameterNames = (event.parameters || []).map(parameter => parameter.name).filter(Boolean);
  const warnings = eventWarnings(event).join("; ");

  const props = {
    Event: titleProp(event.name || event.path),
    Path: textProp(event.path),
    GUID: textProp(event.guid),
    Category: selectProp(event.category || "Uncategorised"),
    Banks: multiSelectProp(bankNames),
    Parameters: multiSelectProp(parameterNames),
    Markers: textProp(markerSummary(event)),
    Warnings: textProp(warnings),
    "Last FMOD Export": dateProp(exportedAt)
  };

  // Do not overwrite human/project-management fields on updates.
  if (isCreate) {
    props.Status = selectProp(warnings ? "Needs Mix" : "Found in FMOD");
    props["Unity Implemented"] = { checkbox: false };
    props["Mix Checked"] = { checkbox: false };
  }

  return props;
}

function validateRegistry(registry) {
  if (!registry || !Array.isArray(registry.events)) {
    throw new Error("Registry JSON must contain an events array.");
  }
}

try {
  const absoluteRegistryPath = path.resolve(registryPath);

  if (!fs.existsSync(absoluteRegistryPath)) {
    throw new Error(`FMOD registry file does not exist: ${absoluteRegistryPath}\nRun FMOD → Scripts → Notion → Export + Sync FMOD Event Registry first, or check FMOD_REGISTRY_PATH.`);
  }

  const registry = JSON.parse(fs.readFileSync(absoluteRegistryPath, "utf8"));
  validateRegistry(registry);

  console.log(`Reading ${registry.events.length} FMOD events from ${absoluteRegistryPath}`);
  console.log("Querying existing Notion pages...");

  const pages = await queryAllPages();
  const byPath = new Map();

  for (const page of pages) {
    const fmodPath = getPlainText(page.properties?.Path);
    if (fmodPath) byPath.set(fmodPath, page);
  }

  const fmodPaths = new Set();
  let created = 0;
  let updated = 0;
  let skipped = 0;
  let archived = 0;

  for (const event of registry.events) {
    if (!event.path) {
      skipped++;
      continue;
    }

    fmodPaths.add(event.path);
    const existing = byPath.get(event.path);

    if (existing) {
      await notion(`/pages/${cleanId(existing.id)}`, {
        method: "PATCH",
        body: { properties: eventProperties(event, registry.exportedAt, false) }
      });
      updated++;
    } else {
      await notion("/pages", {
        method: "POST",
        body: {
          parent: { type: "data_source_id", data_source_id: cleanId(dataSourceId) },
          properties: eventProperties(event, registry.exportedAt, true)
        }
      });
      created++;
    }

    await sleep(350);
  }

  if (archiveMissing) {
    for (const page of pages) {
      const fmodPath = getPlainText(page.properties?.Path);
      if (fmodPath && !fmodPaths.has(fmodPath)) {
        await notion(`/pages/${cleanId(page.id)}`, {
          method: "PATCH",
          body: { in_trash: true }
        });
        archived++;
        await sleep(350);
      }
    }
  }

  console.log("\nFMOD -> Notion sync complete.");
  console.log(`Created: ${created}`);
  console.log(`Updated: ${updated}`);
  console.log(`Skipped: ${skipped}`);
  if (archiveMissing) console.log(`Archived missing: ${archived}`);
} catch (err) {
  console.error("\nFMOD -> Notion sync failed:");
  console.error(err.message || err);
  process.exit(1);
}
