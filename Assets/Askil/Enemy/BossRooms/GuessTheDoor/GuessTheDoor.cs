using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GuessTheDoor : MonoBehaviour
{
    public List<GameObject> sidesEnemy = new List<GameObject>();
    public Animator animator;

    public void randomDoorBreak()
    {
        StartCoroutine(RandomDoorBreak());
    }
    IEnumerator RandomDoorBreak()
    {
        if (sidesEnemy.Count != 0)
        {
            yield return new WaitForSeconds(3);
            sidesEnemy[Random.Range(0, sidesEnemy.Count)].SetActive(true);
        }

        else animator.SetTrigger("OpenDore");
    }

    public void RemoveGameObjectFromList(GameObject objectToRemove)
    {
        sidesEnemy.Remove(objectToRemove);
    }
     
}
