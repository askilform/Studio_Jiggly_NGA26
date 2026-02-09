using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObject : MonoBehaviour
{

    private IEnumerator Start()
    {
        MeshCollider[] colliders  = GetComponentsInChildren<MeshCollider>();
        yield return null;

        foreach (MeshCollider collider in colliders) collider.isTrigger = true;

        yield return new WaitForSeconds(2f);
        foreach(MeshCollider collider in colliders) Destroy(collider.gameObject);
    }
}
