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


    bool turnDark = true;
    int IntervalsHappened;

    public void StartLoop()
    {
       startLoop();
       foreach (var sfx in sfxs) sfx.Play();
    }

    void startLoop()
    {
        if (IntervalsHappened == intervalsBeforeEvent)
        {
            print("ShouldHappenLOL");
            eventToHappen.Invoke();
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

        startLoop();
    }
}
