using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessTheDoor : MonoBehaviour
{
    public List<GameObject> sides = new List<GameObject>();

    private void OnEnable()
    {
        StartCoroutine(RandomDoorBreak());
    }

    public void randomDoorBreak()
    {
        StartCoroutine(RandomDoorBreak());
    }
    IEnumerator RandomDoorBreak()
    {
        yield return new WaitForSeconds(3);
        sides[Random.Range(0, sides.Count)].SetActive(true);
    }
}
