using Unity.VisualScripting;
using UnityEngine;

public class CarFuntion : MonoBehaviour
{
    public Rigidbody rb;
    public CarExitCheck exitSc;
    public GameObject SeatPosition;

    public GameObject player;
    public CarMovement carMovementSc;
    private TextPopUp uiSc;

    private void OnEnable()
    {
        player.GetComponent<PlayerMovement2>().movementAllowed = false;
        player.GetComponent<CharacterController>().enabled = false;
        uiSc = GameObject.FindFirstObjectByType<TextPopUp>();

        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        player.transform.position = SeatPosition.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!exitSc.isColliding) OnCarExit();
            else uiSc.StartCoroutine(uiSc.FlashText("Door Is Blocked!", 0.5f, false));
        }
    }
    private void OnCarExit()
    {
        player.transform.position = exitSc.transform.position;

        player.GetComponent<PlayerMovement2>().movementAllowed = true;
        player.GetComponent<CharacterController>().enabled = true;
        carMovementSc.enabled = false;
        enabled = false;
    }
}
