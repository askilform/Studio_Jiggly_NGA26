using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCam : MonoBehaviour
{
    public List<GameObject> CameraTargets = new List<GameObject>();
    public float timeBetweenTargets;

    private int nextTargetIndex;
    private Camera PlayerCamera;


    private IEnumerator Start()
    {
        // inherit PlayerCams Transform and disable PlayerCam
        PlayerCamera = GameObject.Find("PlayerCam").GetComponent<Camera>();
        transform.SetPositionAndRotation(PlayerCamera.transform.position, PlayerCamera.transform.rotation);
        PlayerCamera.enabled = false;

        // Wait To Return
        yield return new WaitForSeconds(5);

        PlayerCamera.enabled = true;
        Destroy(transform.parent);
    }

}
