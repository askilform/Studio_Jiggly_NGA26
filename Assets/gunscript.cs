using UnityEngine;

public class gunscript : MonoBehaviour
{

    public LayerMask layerMask;

    public GameObject hitPointIndicatorObject;

    public GameObject AimReferenceObject;
    public GameObject ObjectToAim;

    public GameObject gunRayIndicatorObject;

    public float raycastRange = 200f;

    public float heldGunAimLerp = 40f;


    public GameObject bulletTraceObjectToSpawn;

    void Start()
    {
        print("Gun Script");
    }

    void Update()
    {
        

        if (AimReferenceObject == null || ObjectToAim == null)
        {
            print("need to set aim ref at the player eyes, and hand ref at the hand");
            return;
        }

        //Where to point if not hitting wall
        Vector3 HitPos = AimReferenceObject.transform.position + AimReferenceObject.transform.forward * raycastRange;


        RaycastHit hit;


        if (Physics.Raycast(AimReferenceObject.transform.position, AimReferenceObject.transform.forward, out hit, raycastRange, layerMask))

        { 
            Debug.Log("Hit"); 
            HitPos = hit.point;
        }

        else
        { 
            Debug.Log("No Hit");
        }

        hitPointIndicatorObject.transform.position = HitPos;


        Quaternion targetHeldGunAim  = Quaternion.LookRotation(HitPos - ObjectToAim.transform.position, Vector3.up);

        ObjectToAim.transform.rotation = Quaternion.Lerp(ObjectToAim.transform.rotation, targetHeldGunAim, Time.deltaTime * heldGunAimLerp);



        RaycastHit actualHit;

        if (Physics.Raycast(ObjectToAim.transform.position, ObjectToAim.transform.TransformDirection(Vector3.forward), out actualHit, raycastRange, layerMask))

        {
            Debug.Log("Hit");
            gunRayIndicatorObject.transform.position = actualHit.point;
        }

        else
        {
            Debug.Log("No Hit");
            gunRayIndicatorObject.transform.position = hitPointIndicatorObject.transform.position;
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject traceInstatiated = Instantiate(bulletTraceObjectToSpawn, HitPos, ObjectToAim.transform.rotation);
            
            traceInstatiated.transform.localScale = new Vector3(1, 1, (traceInstatiated.transform.position - ObjectToAim.transform.position).magnitude);
        }

    }
}
