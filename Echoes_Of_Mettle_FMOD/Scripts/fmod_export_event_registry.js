/*
  FMOD -> Notion Event Registry Exporter + Sync
  ---------------------------------------------
  This project-local FMOD Studio script exports:
    <FMOD_PROJECT>/fmod-event-registry.json

  Then it starts:
    <FMOD_PROJECT>/.fmod-notion/sync_from_fmod.sh

  FMOD Studio menu:
    Scripts > Notion > Export + Sync FMOD Event Registry
    Scripts > Notion > Export FMOD Event Registry Only
*/

(function () {
    function safe(label, fn, fallback) {
        try {
            var value = fn();
            return value === undefined || value === null ? fallback : value;
        } catch (e) {
            return fallback;
        }
    }

    function toArray(value) {
        if (!value) return [];
        if (Object.prototype.toString.call(value) === "[object Array]") return value;
        var arr = [];
        for (var i = 0; i < value.length; i++) arr.push(value[i]);
        return arr;
    }

    function projectFilePath() {
        return safe("project file path", function () {
            return studio.project.filepath || studio.project.filePath || "";
        }, "");
    }

    function dirname(path) {
        if (!path) return "";
        var lastSlash = Math.max(path.lastIndexOf("/"), path.lastIndexOf("\\"));
        if (lastSlash < 0) return "";
        return path.substring(0, lastSlash);
    }

    function joinPath(dir, file) {
        if (!dir) return file;
        var sep = dir.indexOf("\\") >= 0 ? "\\" : "/";
        if (dir.charAt(dir.length - 1) === "/" || dir.charAt(dir.length - 1) === "\\") return dir + file;
        return dir + sep + file;
    }

    function stringValue(value) {
        if (value === undefined || value === null) return "";
        return String(value);
    }

    function eventPath(eventObj) {
        return safe("event.getPath", function () {
            return eventObj.getPath();
        }, "") || ("event:/" + stringValue(eventObj.name));
    }

    function categoryFromPath(path) {
        if (!path || path.indexOf("event:/") !== 0) return "Uncategorised";
        var rest = path.substring("event:/".length);
        var parts = rest.split("/");
        return parts.length > 1 && parts[0] ? parts[0] : "Uncategorised";
    }

    function getBanks(eventObj) {
        var banks = [];
        var bankObjects = safe("event.banks", function () { return toArray(eventObj.banks); }, []);

        for (var i = 0; i < bankObjects.length; i++) {
            var bank = bankObjects[i];
            var path = safe("bank.getPath", function () { return bank.getPath(); }, "");
            banks.push({
                name: stringValue(bank.name || path || "Unknown Bank"),
                path: stringValue(path),
                guid: stringValue(bank.id)
            });
        }

        return banks;
    }

    function getParameters(eventObj) {
        var params = [];
        var presets = safe("event.getParameterPresets", function () {
            return toArray(eventObj.getParameterPresets());
        }, []);

        for (var i = 0; i < presets.length; i++) {
            var preset = presets[i];
            var owner = safe("parameter preset owner", function () { return preset.presetOwner; }, preset);

            params.push({
                name: stringValue(owner.name || preset.name || "Unnamed Parameter"),
                guid: stringValue(owner.id || preset.id),
                scope: safe("parameter scope", function () { return owner.isGlobal ? "Global" : "Local"; }, ""),
                minimum: safe("parameter min", function () { return owner.minimum; }, null),
                maximum: safe("parameter max", function () { return owner.maximum; }, null)
            });
        }

        return params;
    }

    function getMarkers(eventObj) {
        var markers = [];
        var markerTracks = safe("event.markerTracks", function () { return toArray(eventObj.markerTracks); }, []);

        for (var i = 0; i < markerTracks.length; i++) {
            var track = markerTracks[i];
            var trackMarkers = safe("markerTrack.markers", function () { return toArray(track.markers); }, []);

            for (var j = 0; j < trackMarkers.length; j++) {
                var marker = trackMarkers[j];
                markers.push({
                    name: stringValue(marker.name || marker.label || marker.text || "Marker"),
                    position: safe("marker.position", function () { return marker.position; }, null),
                    type: stringValue(marker.entity || marker.type || "")
                });
            }
        }

        return markers;
    }

    function getUserProperties(eventObj) {
        var props = [];
        var userProperties = safe("event.userProperties", function () { return toArray(eventObj.userProperties); }, []);

        for (var i = 0; i < userProperties.length; i++) {
            var p = userProperties[i];
            props.push({
                name: stringValue(p.name),
                value: stringValue(p.value)
            });
        }

        return props;
    }

    function buildWarnings(eventData) {
        var warnings = [];
        if (!eventData.path || eventData.path.indexOf("event:/") !== 0) warnings.push("Path is missing or invalid");
        if (!eventData.banks || eventData.banks.length === 0) warnings.push("Event is not assigned to a bank");
        if (eventData.category === "Uncategorised") warnings.push("No category folder in event path");
        return warnings;
    }

    function writeRegistry() {
        var events = safe("Event.findInstances", function () {
            return studio.project.model.Event.findInstances();
        }, []);

        var projectPath = projectFilePath();
        var projectDir = dirname(projectPath);
        var outputPath = joinPath(projectDir, "fmod-event-registry.json");

        var registry = {
            schemaVersion: 1,
            exportedAt: new Date().toISOString(),
            fmodVersion: safe("studio.version", function () {
                return studio.version.productVersion + "." + studio.version.majorVersion + "." + studio.version.minorVersion;
            }, ""),
            projectFile: projectPath,
            eventCount: events.length,
            events: []
        };

        for (var i = 0; i < events.length; i++) {
            var e = events[i];
            var path = eventPath(e);
            var data = {
                name: stringValue(e.name),
                path: path,
                guid: stringValue(e.id),
                category: categoryFromPath(path),
                banks: getBanks(e),
                parameters: getParameters(e),
                markers: getMarkers(e),
                userProperties: getUserProperties(e)
            };
            data.warnings = buildWarnings(data);
            registry.events.push(data);
        }

        var file = studio.system.getFile(outputPath);
        file.open(studio.system.openMode.WriteOnly);
        file.writeText(JSON.stringify(registry, null, 2));
        file.close();

        return {
            count: registry.events.length,
            outputPath: outputPath,
            projectDir: projectDir
        };
    }

    function startNotionSync(projectDir) {
        var shell = "/bin/zsh";
        var helperDir = joinPath(projectDir, ".fmod-notion");
        var syncScript = joinPath(helperDir, "sync_from_fmod.sh");

        return studio.system.startAsync(shell, {
            workingDir: helperDir,
            args: [syncScript]
        });
    }

    function exportOnly() {
        var result = writeRegistry();
        alert("Exported " + result.count + " FMOD events to:\n" + result.outputPath);
    }

    function exportAndSync() {
        var result = writeRegistry();

        try {
            startNotionSync(result.projectDir);
            alert(
                "Exported " + result.count + " FMOD events to:\n" +
                result.outputPath +
                "\n\nNotion sync started. Check .fmod-notion/logs/last-sync.log if Notion does not update."
            );
        } catch (err) {
            alert(
                "Exported " + result.count + " FMOD events to:\n" +
                result.outputPath +
                "\n\nBut Notion sync failed to start:\n" + err
            );
        }
    }

    studio.menu.addMenuItem({
        name: "Notion\\Export + Sync FMOD Event Registry",
        execute: exportAndSync
    });

    studio.menu.addMenuItem({
        name: "Notion\\Export FMOD Event Registry Only",
        execute: exportOnly
    });
})();
