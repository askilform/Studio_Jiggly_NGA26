#!/bin/zsh
# Project-local wrapper started by FMOD Studio.
# It loads <FMOD_PROJECT>/.fmod-notion/local.env and runs the Notion sync worker.

set -euo pipefail

# macOS apps launched from Finder/FMOD often do not inherit your Terminal PATH.
# These paths cover Apple Silicon Homebrew, Intel Homebrew, and system tools.
export PATH="/opt/homebrew/bin:/usr/local/bin:/usr/bin:/bin:/usr/sbin:/sbin:$PATH"

SCRIPT_DIR="${0:A:h}"
PROJECT_DIR="${SCRIPT_DIR:h}"
ENV_FILE="$SCRIPT_DIR/local.env"
LOG_DIR="$SCRIPT_DIR/logs"
LOG_FILE="$LOG_DIR/last-sync.log"

mkdir -p "$LOG_DIR"

if [[ ! -f "$ENV_FILE" ]]; then
  echo "Missing local config: $ENV_FILE" | tee "$LOG_FILE"
  echo "Run: node install_for_project.mjs" | tee -a "$LOG_FILE"
  exit 1
fi

set -a
source "$ENV_FILE"
set +a

export FMOD_REGISTRY_PATH="${FMOD_REGISTRY_PATH:-$PROJECT_DIR/fmod-event-registry.json}"

{
  echo "FMOD -> Notion sync started: $(date)"
  echo "Project: $PROJECT_DIR"
  echo "Registry: $FMOD_REGISTRY_PATH"
  echo
  cd "$SCRIPT_DIR"

  NODE_TO_USE="${NODE_BIN:-}"
  if [[ -z "$NODE_TO_USE" ]]; then
    if command -v node >/dev/null 2>&1; then
      NODE_TO_USE="$(command -v node)"
    elif [[ -x "/opt/homebrew/bin/node" ]]; then
      NODE_TO_USE="/opt/homebrew/bin/node"
    elif [[ -x "/usr/local/bin/node" ]]; then
      NODE_TO_USE="/usr/local/bin/node"
    else
      echo "Could not find Node.js."
      echo "Install Node.js from https://nodejs.org/ and rerun the installer."
      exit 127
    fi
  fi

  echo "Node: $NODE_TO_USE"
  "$NODE_TO_USE" "$SCRIPT_DIR/notion_sync_fmod_events.mjs"
  echo
  echo "FMOD -> Notion sync finished: $(date)"
} 2>&1 | tee "$LOG_FILE"
