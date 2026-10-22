using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

public class GuessTheDoor : MonoBehaviour
{
    // float timeBetweenDoorChange = 1;
    int CurrentIntervalIndex = 1;
   
    int DoorToChoose;

    public UnityEvent onLastKill;

    [Header("Assign")]
    public List<DoorToGuess> sides = new List<DoorToGuess>();
    public List<float> LightIntervals = new List<float>();
    public AnimationCurve graf;


    [Header("Tewaks")]
    public float timeBeforeWallBreak;

    public void StartGameRound()
    {
        print("Index:" + CurrentIntervalIndex + "and listcount is" + LightIntervals.Count);
        // Checks if there are more enemies
        // Only restarts loop if there are 2 sides left, 1 would be wierd
        if (sides.Count > 1) 
        {
            if (CurrentIntervalIndex < LightIntervals.Count) StartCoroutine(DoorChanger());
            else StartCoroutine(DoorBreak());
        }

        // Finihses GuessTheDoor if there is only one available enemy
        else
        {
            onLastKill.Invoke();
        }
    }

    IEnumerator DoorChanger()
    {
        yield return new WaitForSeconds(LightIntervals[CurrentIntervalIndex]);

        foreach (DoorToGuess side in sides)
            side.BecomeInactive();

        yield return null;

        int previousDoor = DoorToChoose;

        do
        {
            DoorToChoose = Random.Range(0, sides.Count);
        }
        while (DoorToChoose == previousDoor);

        sides[DoorToChoose].BecomeActiveDoor();

        CurrentIntervalIndex += 1;

        StartGameRound();
    }

    IEnumerator DoorBreak()
    {
        if (sides.Count != 0)
        {
            yield return new WaitForSeconds(timeBeforeWallBreak);
            sides[DoorToChoose].ActivateEnemy();
        }

        else
        {
            onLastKill.Invoke();
        }

        // timeBetweenDoorChange = 1;

        // Delay picks from first index og Interval-list again
        CurrentIntervalIndex = 1;
    }
}
