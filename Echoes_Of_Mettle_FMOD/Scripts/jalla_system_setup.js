/*
  JallaSound FMOD setup
  ---------------------
  Keeps every existing JallaSound event and GUID intact, then:
  - routes each event to the existing project mixer buses;
  - makes 2D/3D behavior consistent by category;
  - creates a synchronized adaptive Haunted Echo music event.

  FMOD Studio menu:
    Scripts > JallaSound > Build Systems
*/

var JallaSoundSystems = (function () {
    var ROOT = "event:/JallaSound/";
    var MUSIC_LENGTH_MS = 36728.156;

    var categories = {
        Music: [
            "chasing_beat",
            "Haunted Echo T1_BASE",
            "Haunted Echo T1_BUILD",
            "Haunted Echo T1_HITS v5",
            "Haunted Echo T1_LEAD"
        ],
        Stingers: [
            "Enemychase",
            "InEnemyZone",
            "JumpscareSFX",
            "PlayerLost",
            "PlayerSpotted"
        ],
        Player: [
            "cloth_thud_rustle_medium",
            "custom fleshy sandy punch hitmarker",
            "JumpNew",
            "LandNew2",
            "PickupPart.SFX",
            "WalkingTimeline"
        ],
        Weapons: [
            "cyber-tennis-smash-100733",
            "GMS Chargeup Timeline",
            "GMS Cooldown",
            "GMS shot",
            "minigun_overheat-84899",
            "pistol-shot-loud",
            "power-charge-6798"
        ],
        Enemy: [
            "EnemyWalk",
            "EnemyWalkTimeline"
        ],
        World: [
            "DoorOn",
            "Electricity",
            "giant-fall-impact-352446",
            "OnBreak",
            "SciFi-Hum",
            "SplatSFX",
            "SpotLight"
        ],
        UI: [
            "ScreenMessage",
            "Tap"
        ]
    };

    function toArray(value) {
        if (!value) return [];
        if (Object.prototype.toString.call(value) === "[object Array]") return value;
        var result = [];
        for (var i = 0; i < value.length; i++) result.push(value[i]);
        return result;
    }

    function requireObject(path) {
        var value = studio.project.lookup(path);
        if (!value) throw new Error("Missing required FMOD object: " + path);
        return value;
    }

    function findEvent(name) {
        return requireObject(ROOT + name);
    }

    function spatializers(eventObject) {
        var effects = toArray(eventObject.masterTrack.mixerGroup.effectChain.effects);
        var result = [];
        for (var i = 0; i < effects.length; i++) {
            if (effects[i].isOfExactType("SpatialiserEffect")) result.push(effects[i]);
        }
        return result;
    }

    function setSpatial(eventObject, shouldBe3D) {
        var existing = spatializers(eventObject);

        if (shouldBe3D && existing.length === 0) {
            eventObject.masterTrack.mixerGroup.effectChain.addEffect("SpatialiserEffect");
        }

        if (!shouldBe3D) {
            for (var i = 0; i < existing.length; i++) studio.project.deleteObject(existing[i]);
        }
    }

    function configureCategory(categoryName, busPath, shouldBe3D, color) {
        var bus = requireObject(busPath);
        var names = categories[categoryName];

        for (var i = 0; i < names.length; i++) {
            var eventObject = findEvent(names[i]);
            eventObject.mixerInput.output = bus;
            eventObject.color = color;
            setSpatial(eventObject, shouldBe3D);
        }
    }

    function ensureFolder(parent, name) {
        var existing = parent.getItem(name);
        if (existing) return existing;

        var folder = studio.project.create("EventFolder");
        folder.name = name;
        folder.folder = parent;
        return folder;
    }

    function addNestedStem(systemEvent, intensity, label, sourceEventName, points) {
        var track = systemEvent.addGroupTrack(label);
        var sound = track.addSound(systemEvent.timeline, "EventSound", 0, MUSIC_LENGTH_MS);
        sound.event = findEvent(sourceEventName);

        if (points && points.length) {
            var automator = track.mixerGroup.addAutomator("volume");
            var curve = automator.addAutomationCurve(intensity);
            for (var i = 0; i < points.length; i++) {
                curve.addAutomationPoint(points[i][0], points[i][1]);
            }
        }
    }

    function createAdaptiveMusicSystem() {
        var existing = studio.project.lookup(ROOT + "Systems/Music/Haunted Echo System");
        if (existing) return existing;

        var jallaFolder = requireObject("eventfolder:/JallaSound");
        var systemsFolder = ensureFolder(jallaFolder, "Systems");
        var musicFolder = ensureFolder(systemsFolder, "Music");
        var eventObject = studio.project.workspace.addEvent("Haunted Echo System", false);

        eventObject.folder = musicFolder;
        eventObject.color = "Blue Light 2";
        eventObject.mixerInput.output = requireObject("bus:/Music");
        eventObject.relationships.banks.add(requireObject("bank:/JallaBank"));

        var intensity = eventObject.addGameParameter({
            name: "Intensity",
            type: studio.project.parameterType.User,
            min: 0,
            max: 3
        });

        addNestedStem(eventObject, intensity, "Base", "Haunted Echo T1_BASE", []);
        addNestedStem(eventObject, intensity, "Hits", "Haunted Echo T1_HITS v5", [
            [0, -80],
            [0.75, -80],
            [1.0, -6],
            [1.5, 0],
            [3, 0]
        ]);
        addNestedStem(eventObject, intensity, "Build", "Haunted Echo T1_BUILD", [
            [0, -80],
            [1.5, -80],
            [2.0, -3],
            [2.5, 0],
            [3, 0]
        ]);
        addNestedStem(eventObject, intensity, "Lead", "Haunted Echo T1_LEAD", [
            [0, -80],
            [2.25, -80],
            [2.75, -6],
            [3, 0]
        ]);

        eventObject.markerTracks[0].addRegion(
            0,
            MUSIC_LENGTH_MS,
            "Main Loop",
            studio.project.regionLoopMode.Looping
        );

        return eventObject;
    }

    function buildSystems(showResult) {
        configureCategory("Music", "bus:/Music", false, "Blue Light 2");
        configureCategory("Stingers", "bus:/Music/Stingers", false, "Purple Light 2");
        configureCategory("Player", "bus:/Player", false, "Green Light 2");
        configureCategory("Weapons", "bus:/Weapons", false, "Red Light 2");
        configureCategory("Enemy", "bus:/Enemy", true, "Orange Light 2");
        configureCategory("World", "bus:/World", true, "Yellow Light 2");
        configureCategory("UI", "bus:/UI", false, "Cyan Light 2");

        var musicSystem = createAdaptiveMusicSystem();
        studio.project.save();

        if (showResult !== false) {
            alert(
                "JallaSound setup complete.\n\n" +
                "34 source events routed and spatialized.\n" +
                "Adaptive music: " + musicSystem.getPath() + "\n" +
                "Parameter: Intensity (0-3)"
            );
        }
    }

    studio.menu.addMenuItem({
        name: "JallaSound\\Build Systems",
        execute: function () { buildSystems(true); }
    });

    return { build: buildSystems };
})();
