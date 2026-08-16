using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float SteerDebugStartLocation;
    private float SteeringAdd;
    float x;
    float z;

    public TextMesh debugText;
    public GameObject wheelDebug;

    [Header("Tweaks")]
    public float Speed;
    public float turnSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        SteerDebugStartLocation = wheelDebug.transform.localPosition.x;
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        debugText.text = new Vector2(x, z).ToString();
        rb.AddForce(transform.forward * z * Speed, ForceMode.Acceleration);

        if (x != 0)
        { 
            wheelDebug.transform.localPosition = new Vector3 
                (x, 
                wheelDebug.transform.localPosition.y
                , wheelDebug.transform.localPosition.z);

            rb.MoveRotation(
            rb.rotation * Quaternion.Euler(0, x * Time.deltaTime * turnSpeed, 0));
        }
    }
}
