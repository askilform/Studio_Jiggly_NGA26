using UnityEngine;

public class AutoHideMesh : MonoBehaviour
{
    private MeshRenderer theMesh;

    void Start()
    {
        theMesh = GetComponent<MeshRenderer>();
        theMesh.enabled = false;
    }
}
