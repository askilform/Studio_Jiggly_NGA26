# Audio asset organisation

Organised on 18 October 2026: 318 audio files in 50 folders containing audio. The Assets browser now follows the event categories **Ambiance**, **JallaSound**, **Music**, **SFX**, and **UI**.

Folders use readable versions of event names. The events themselves retain their original names and GUIDs. Sound files use descriptive names with spaces and two-digit variation numbers, for example `Concrete Footstep Heel 01.wav`, `Typewriter Key 01.wav`, and `Fluorescent Light Flicker 01.wav`.

## Where to look

- **Ambiance:** car and engine, forest night, industrial room, lights, scattered sounds and thunder.
- **SFX/Player:** breathing, jump, concrete and gravel footsteps. Toe samples in Steps are also used by landing events.
- **SFX/Player/Shared:** clothing used by crouch, steps and landings; grunts used by several landing events.
- **SFX/Enemy**, **SFX/Weapons**, **SFX/World:** folders for the corresponding events.
- **Music:** main menu, road trip, chase beat, Haunted Music stems and stingers. Scary Horn contains the 43 reverse-horn variations.
- **UI:** confirm, hover, pause, unpause and screen message.
- **JallaSound:** sounds used by the existing JallaSound events.

All 318 source files were referenced by events. Shared sounds remain single files, with every existing event reference preserved.

## Original filenames and recovery

[Audio Asset Map.csv](Audio%20Asset%20Map.csv) records every original filename, new path, owning events, asset GUID and audio checksum. Variation numbers are ordered within each sound family; the map retains the original source suffixes and Soundly catalogue references.

The full pre-change audio and metadata backup is here:

`/Users/koplego/.codex/visualizations/2026/10/10/01a1279a-2ccf-7b10-a10d-59b31ff7fd4a/FMOD-Asset-Organisation-2026-10-18/Backup-Before-Organisation`

`Backup-Saved-Session/Metadata` alongside it contains the saved session immediately before renaming, including the work that had been unsaved when this task began.

## Verification

- All 318 audio files exist at their registered paths.
- Every audio file has the same SHA-256 checksum as before.
- Every asset retains its GUID.
- Saved metadata differs only in AudioFile asset paths. Events, playlists, edits, parameters, routing, snapshots and bank assignments are unchanged.
- FMOD's Validate Project reports: “No errors were found in the project.”
- The Assets browser searches for `#missing` and `#unimported`, with Flatten enabled, both return no files.

## Event-to-folder reference

| Existing event | Asset folder(s) |
| --- | --- |
| Ambiance/CarStartStop | Assets/Ambiance/Car Start Stop |
| Ambiance/Engine | Assets/Ambiance/Engine |
| Ambiance/ForestNight | Assets/Ambiance/Forest Night |
| Ambiance/IndustrialRoom | Assets/Ambiance/Industrial Room |
| Ambiance/Light buzz | Assets/Ambiance/Light Buzz |
| Ambiance/LightFlick | Assets/Ambiance/Light Flicker |
| Ambiance/ScatterAmbiance | Assets/Ambiance/Scatter Ambiance |
| Ambiance/Thunder | Assets/Ambiance/Thunder |
| JallaSound/DoorOn | Assets/JallaSound/Door On |
| JallaSound/Electricity | Assets/JallaSound/Electricity |
| JallaSound/OnBreak | Assets/JallaSound/On Break |
| JallaSound/PickupPart.SFX | Assets/JallaSound/Pickup Part |
| JallaSound/SplatSFX | Assets/JallaSound/Splat |
| JallaSound/Tap | Assets/JallaSound/Tap |
| JallaSound/custom fleshy sandy punch hitmarker | Assets/JallaSound/Hitmarker |
| JallaSound/cyber-tennis-smash-100733 | Assets/JallaSound/Cyber Smash |
| JallaSound/giant-fall-impact-352446 | Assets/JallaSound/Giant Impact |
| JallaSound/minigun_overheat-84899 | Assets/JallaSound/Minigun Overheat |
| Music/Haunted music/Haunted Echo T1_BASE | Assets/Music/Haunted Music |
| Music/Haunted music/Haunted Echo T1_BUILD | Assets/Music/Haunted Music |
| Music/Haunted music/Haunted Echo T1_HITS v5 | Assets/Music/Haunted Music |
| Music/Haunted music/Haunted Echo T1_LEAD | Assets/Music/Haunted Music |
| Music/Main Menu | Assets/Music/Main Menu |
| Music/RoadTrip | Assets/Music/Road Trip |
| Music/Stingers/InEnemyZone | Assets/Music/Stingers |
| Music/Stingers/JumpscareSFX | Assets/Music/Stingers |
| Music/Stingers/PlayerLost | Assets/Music/Stingers |
| Music/Stingers/PlayerSpotted | Assets/Music/Stingers |
| Music/Stingers/Scaryhorn | Assets/Music/Stingers/Scary Horn |
| Music/Stingers/SciFi-Hum | Assets/Music/Stingers |
| Music/chasing_beat | Assets/Music/Chasing Beat |
| SFX/Enemy/EnemyWalk | Assets/SFX/Enemy/Enemy Walk |
| SFX/Enemy/Enemychase | Assets/SFX/Enemy/Enemy Chase |
| SFX/Player/Breathing | Assets/SFX/Player/Breathing |
| SFX/Player/Crouch | Assets/SFX/Player/Shared/Clothing |
| SFX/Player/Jump | Assets/SFX/Player/Jump |
| SFX/Player/Land/ConcreteLand | Assets/SFX/Player/Shared/Clothing; Assets/SFX/Player/Shared/Landing Grunts; Assets/SFX/Player/Steps/Concrete |
| SFX/Player/Land/GravelLand | Assets/SFX/Player/Shared/Clothing; Assets/SFX/Player/Shared/Landing Grunts; Assets/SFX/Player/Steps/Gravel |
| SFX/Player/Steps/Concrete Step | Assets/SFX/Player/Shared/Clothing; Assets/SFX/Player/Steps/Concrete |
| SFX/Player/Steps/Gravel | Assets/SFX/Player/Shared/Clothing; Assets/SFX/Player/Steps/Gravel |
| SFX/Weapons/GMS Chargeup Timeline | Assets/SFX/Weapons/GMS Chargeup |
| SFX/Weapons/GMS Cooldown | Assets/SFX/Weapons/GMS Cooldown |
| SFX/Weapons/GMS shot | Assets/SFX/Weapons/GMS Shot |
| SFX/Weapons/pistol-shot-loud | Assets/SFX/Weapons/Pistol Shot |
| SFX/Weapons/power-charge-6798 | Assets/SFX/Weapons/Power Charge |
| SFX/World/CigaretteThrow | Assets/SFX/World/Cigarette Throw |
| SFX/World/DoorClose | Assets/SFX/World/Door Close |
| SFX/World/DoorOpen | Assets/SFX/World/Door Open |
| SFX/World/FuelCellHover | Assets/SFX/World/Fuel Cell Hover |
| SFX/World/PaperPickup | Assets/SFX/World/Paper Pickup |
| SFX/World/SpotLight | Assets/SFX/World/Spotlight |
| SFX/World/Threat | Assets/SFX/World/Threat |
| SFX/World/Typewriter | Assets/SFX/World/Typewriter |
| UI/Confirm | Assets/UI/Confirm |
| UI/Hover | Assets/UI/Hover |
| UI/Pause | Assets/UI/Pause |
| UI/ScreenMessage | Assets/UI/Screen Message |
| UI/Unpause | Assets/UI/Unpause |
