using Unity.VisualScripting;
using UnityEngine;

public class CarFuntion : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 playerPreRot;
    public CarExitCheck exitSc;
    public GameObject SeatPosition;

    public GameObject player;
    public CarMovement carMovementSc;
    private TextPopUp uiSc;

    private void OnEnable()
    {
        player.GetComponent<PlayerMovement2>().movementAllowed = false;
        player.GetComponent<PlayerMovement2>().cameraMovementAllowed = false;
        player.GetComponent<CharacterController>().enabled = false;
        uiSc = GameObject.FindFirstObjectByType<TextPopUp>();

        rb = gameObject.AddComponent<Rigidbody>();
        rb.freezeRotation = true;
        
    }

    private void FixedUpdate()
    {
        player.transform.position = Vector3.Lerp(player.transform.position, SeatPosition.transform.position, 0.2f);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, SeatPosition.transform.rotation, 0.05f);
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
        player.transform.rotation = Quaternion.identity;

        player.GetComponent<PlayerMovement2>().movementAllowed = true;
        player.GetComponent<PlayerMovement2>().cameraMovementAllowed = true;
        player.GetComponent<CharacterController>().enabled = true;

        carMovementSc.enabled = false;
        enabled = false;
    }
}
