using FMODUnity;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CarFuntion : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 playerPreRot;
    public CarExitCheck exitSc;
    public GameObject SeatPosition;

    [NonSerialized] public GameObject player;
    public CarMovement carMovementSc;
    public StudioEventEmitter carEnterAudio;

    private TextPopUp uiSc;

    private void OnEnable()
    {
        CarEnter();
    }

    public void CarEnter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMovement2>().movementAllowed = false;
        player.GetComponent<PlayerMovement2>().cameraMovementAllowed = false;
        player.GetComponent<CharacterController>().enabled = false;
        uiSc = GameObject.FindFirstObjectByType<TextPopUp>();

        rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        if (carEnterAudio != null) carEnterAudio.Play();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, SeatPosition.transform.position, 0.5f);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, SeatPosition.transform.rotation, 0.1f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!exitSc.isColliding) StartCoroutine(OnCarExit());
            else uiSc.StartCoroutine(uiSc.FlashText("Door Is Blocked!", 0.5f, false, false));
        }
    }
    private IEnumerator OnCarExit()
    {
        yield return new WaitForSeconds(0.1f);

        player.transform.position = exitSc.transform.position;
        player.transform.rotation = Quaternion.identity;

        player.GetComponent<PlayerMovement2>().movementAllowed = true;
        player.GetComponent<PlayerMovement2>().cameraMovementAllowed = true;
        player.GetComponent<CharacterController>().enabled = true;

        carMovementSc.carEngineAudio.SetParameter("RPM", 0);
        carMovementSc.enabled = false;
        if (carEnterAudio != null) carEnterAudio.Stop();
        enabled = false;
    }
}
