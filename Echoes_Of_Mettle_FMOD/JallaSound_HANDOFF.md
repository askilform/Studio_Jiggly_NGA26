# JallaSound FMOD handoff

The original `event:/JallaSound/*` event paths and GUIDs remain available. The setup pass routes them through the existing mixer hierarchy and standardizes their spatial behavior.

## Adaptive music

Use `event:/JallaSound/Systems/Music/Haunted Echo System` instead of starting the four Haunted Echo stem events separately.

Keep one event instance alive and set its local continuous parameter:

- `Intensity = 0`: base layer only
- `Intensity = 1`: adds hits
- `Intensity = 2`: adds build
- `Intensity = 3`: adds lead

The four stems start together and loop together at 36.728 seconds. Volume automation crossfades the added layers, so changing `Intensity` does not restart or desynchronize the music.

## Mixer routing

| Bus | Events | Spatial behavior |
| --- | --- | --- |
| `bus:/Music` | Haunted Echo stems, `chasing_beat`, adaptive music system | 2D |
| `bus:/Music/Stingers` | enemy state cues, jumpscare, player spotted/lost | 2D |
| `bus:/Player` | player movement, pickup, fuel-rod handling | 2D |
| `bus:/Weapons` | GMS, pistol, charge, overheat, weapon impacts | 2D |
| `bus:/Enemy` | enemy footsteps | 3D |
| `bus:/World` | doors, electricity, breakables, machinery, impacts | 3D |
| `bus:/UI` | tap and screen-message cues | 2D |

## Programmer notes

- Prefer event GUID references over hard-coded paths.
- Attach `Enemy` and `World` events to the relevant GameObject and update their 3D attributes.
- `Music`, `Stingers`, `Player`, `Weapons`, and `UI` events do not require 3D attributes.
- Stop looping or persistent instances with `ALLOWFADEOUT`, then release them.
- One-shots may be released immediately after `start()`.
- The original four Haunted Echo events are retained as building blocks and for compatibility. New code should use the adaptive system event.

The repeatable setup command is available in FMOD Studio under `Scripts > JallaSound > Build Systems`.

## Bank status

The FMOD source metadata is ready, but the Desktop bank was not regenerated in this pass. FMOD's command-line loader hangs while opening both this working copy and an unchanged checkout of the project on this machine. Open the project in FMOD Studio, validate it, and build `JallaBank` before integrating the new adaptive event.
