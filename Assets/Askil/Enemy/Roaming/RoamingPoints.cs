using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoamingPoints : MonoBehaviour
{
    public List<GameObject> points = new List<GameObject>();
    public GameObject activeRoamingPoint;

    private void Start()
    {
        foreach (GameObject point in points)
        {
            point.GetComponent<MeshCollider>().enabled = false;
        }

        StartCoroutine(ChangeRoamingPoint());
    }
    private IEnumerator ChangeRoamingPoint()
    {
        activeRoamingPoint = points[Random.Range(0, (points.Count - 1))];
        yield return new WaitForSeconds(Random.Range(0, 10));
    }
}
