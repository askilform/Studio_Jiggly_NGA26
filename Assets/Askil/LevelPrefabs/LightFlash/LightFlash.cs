using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class LightFlash : MonoBehaviour
{
    public List<float> flashIntervals;
    public int intervalsBeforeEvent;
    public UnityEvent eventToHappen;

    public Volume PostProcessing;
    public List <StudioEventEmitter> sfxs = new List<StudioEventEmitter>();
    public bool DecaySound = true;

    public bool TeleportPlayer;
    public Vector3 teleportPlayerTo;

    bool turnDark = true;
    int IntervalsHappened;

    public List<float> OgflashIntervals = new List<float>();

    public bool OnStartDebug;
    public float StartDelay;

    private void Start()
    {
        OgflashIntervals = new List<float>(flashIntervals);
        if (OnStartDebug) StartLoop();
    }

    public void StartLoop()
    {
        StartCoroutine(LoopIen());
    }

    public IEnumerator LoopIen()
    {
        yield return new WaitForSeconds(StartDelay);

        print("StartedLOPPPP");
        IntervalsHappened = 0;
        flashIntervals = new List<float>(OgflashIntervals);
        startLoopLocal();
        if (DecaySound) foreach (var sfx in sfxs) sfx.Play();
        else sfxs[0].Play();
    }

    void startLoopLocal()
    {
        if (IntervalsHappened == intervalsBeforeEvent)
        {
            print("ShouldHappenLOL");
            eventToHappen.Invoke();

            if (TeleportPlayer) GameObject.FindGameObjectWithTag("Player").transform.localPosition = teleportPlayerTo;
        }

        if (flashIntervals.Count != 0) StartCoroutine(FLashLoop());
        else PostProcessing.weight = 0;

        flashIntervals.RemoveAt(0);
    }

    private IEnumerator FLashLoop()
    {
        PostProcessing.weight = turnDark ? 1 : 0;
        turnDark = !turnDark;

        print((flashIntervals[0]) + "[]");

        yield return new WaitForSeconds((flashIntervals[0]));

        IntervalsHappened += 1;

        startLoopLocal();
    }
}
