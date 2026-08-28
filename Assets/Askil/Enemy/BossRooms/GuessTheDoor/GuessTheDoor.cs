using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GuessTheDoor : MonoBehaviour
{
    [Header("Assign")]
    public List<GameObject> sidesEnemy = new List<GameObject>();
    public Animator animator;

    [Header("Tewaks")]
    public float timeBeforeWallBreak;

    public void randomDoorBreak()
    {
        StartCoroutine(RandomDoorBreak());
    }
    IEnumerator RandomDoorBreak()
    {
        if (sidesEnemy.Count != 0)
        {
            yield return new WaitForSeconds(timeBeforeWallBreak);
            sidesEnemy[Random.Range(0, sidesEnemy.Count)].SetActive(true);
        }

        else animator.SetTrigger("OpenDore");
    }

    public void RemoveGameObjectFromList(GameObject objectToRemove)
    {
        sidesEnemy.Remove(objectToRemove);
    }
     
}
