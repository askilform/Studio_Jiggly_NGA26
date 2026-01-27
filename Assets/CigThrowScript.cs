using UnityEngine;

public class CigThrowScript : MonoBehaviour
{

    public GameObject cigaretteObject;

    public bool mustBeHeldToUse = false;

    void Start()
    {
        
    }


    void Update()
    {
        if (!mustBeHeldToUse && Input.GetKeyDown(KeyCode.G))
        {
            ThrowCig();
        
        }
    }

    public void ThrowCig()
    {
        GameObject cigInstance = Instantiate(cigaretteObject, transform.position, transform.rotation);

        if (cigInstance.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.linearVelocity = cigInstance.transform.forward * 10 + Vector3.up * 2;
            rb.angularVelocity = new Vector3(0, 5, 0);
        }
    }

}
