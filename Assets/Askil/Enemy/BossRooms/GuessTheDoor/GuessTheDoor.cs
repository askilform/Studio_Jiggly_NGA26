using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class GuessTheDoor : MonoBehaviour
{
    float timeBetweenDoorChange = 1;
   
    int DoorToChoose;

    [Header("Assign")]
    public List<DoorToGuess> sides = new List<DoorToGuess>();
    public Animator animator;
    public AnimationCurve CountDownTime;


    [Header("Tewaks")]
    public float timeBeforeWallBreak;

    public void StartGameRound()
    {
        if (sides.Count > 1)
        {


            if (timeBetweenDoorChange > 0) StartCoroutine(DoorChanger());
            else StartCoroutine(DoorBreak());
        }

        else animator.SetTrigger("OpenDore");
    }

    IEnumerator DoorChanger()
    {
        yield return new WaitForSeconds(timeBetweenDoorChange);
        foreach (DoorToGuess side in sides) side.BecomeInactive();
        yield return null;
        DoorToChoose = Random.Range(0, sides.Count);
        sides[DoorToChoose].BecomeActiveDoor();
     
        timeBetweenDoorChange -= 0.1f;


        StartGameRound(); //Restart Loop
    }


    IEnumerator DoorBreak()
    {
        if (sides.Count != 0)
        {
            yield return new WaitForSeconds(timeBeforeWallBreak);
            sides[DoorToChoose].ActivateEnemy();
        }

        else animator.SetTrigger("OpenDore");

        timeBetweenDoorChange = 1;
    }
}
