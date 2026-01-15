using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CinematicCam : MonoBehaviour
{
    private Camera PlayerCamera;
    private Vector3 startPosition;
    private Quaternion startRotation;

    public GameObject CameraTarget;
    public float TimeBeforeReset;
    public bool In = true;

    [Header("CameraLerp")]
    public float interpolationFactor = 0;


    private void Awake()
    {
        PlayerCamera = GameObject.Find("PlayerCam").GetComponent<Camera>();

        startPosition = PlayerCamera.transform.position;
        startRotation = PlayerCamera.transform.rotation;

        transform.position = startPosition;
        transform.rotation = startRotation;
    }


    public void SetReferences(GameObject CameraTargetSend, float TimeBeforeResetSend)
    {
        CameraTarget = CameraTargetSend;
        TimeBeforeReset =  TimeBeforeResetSend;

        StartCoroutine(AfterReferenceSet());
    }

    private IEnumerator AfterReferenceSet()
    {
        yield return new WaitForSeconds(TimeBeforeReset / 2);
        In = false;
        interpolationFactor = 1;

        yield return new WaitForSeconds(TimeBeforeReset / 2);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (In)
        {
            transform.position = Vector3.Lerp(startPosition, CameraTarget.transform.position, interpolationFactor);
            transform.rotation = Quaternion.Lerp(startRotation, CameraTarget.transform.rotation, interpolationFactor);

            if (interpolationFactor < 1) interpolationFactor += Time.deltaTime;
        }

        else
        {
            transform.position = Vector3.Lerp(PlayerCamera.transform.position, CameraTarget.transform.position, interpolationFactor);
            transform.rotation = Quaternion.Lerp(PlayerCamera.transform.rotation, CameraTarget.transform.rotation, interpolationFactor);

            interpolationFactor -= Time.deltaTime;
        }
    } 
}
