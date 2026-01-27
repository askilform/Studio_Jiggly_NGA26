using UnityEngine;

public class MultipurposeDoor : MonoBehaviour
{

    public bool open = false;

    public Vector3 openOffset = new Vector3(0,0,0);
    public Vector3 openAngles = new Vector3(0, 90, 0);

    public Vector3 defaultOffst;
    public Vector3 defaultAngles;




    public float speed = 10f;


    void Start()
    {
        defaultOffst = transform.position;
        defaultAngles = transform.eulerAngles;
        
    }

    void Update()
    {

        Vector3 targetAngles = open ? defaultAngles + openAngles : defaultAngles;
        Vector3 targetPos = open ? defaultOffst + openOffset : defaultOffst;

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, Time.deltaTime * speed);


    }

    public void Open()
    {
        open = true;
    }

    public void Close()
    {
        open = false;
    }

    public void Toggle()
    {
        open = !open;
    }

}
