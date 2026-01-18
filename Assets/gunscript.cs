using UnityEngine;

public class gunscript : MonoBehaviour
{

    public LayerMask layerMask;

    public GameObject hitPointIndicatorObject;

    public GameObject AimReferenceObject;
    public GameObject ObjectToAim;

    public GameObject gunRayIndicatorObject;

    //This is for wall checks and such, can keep high!!
    public float raycastRange = 200f;

    public float heldGunAimLerp = 20f;




    void Update()
    {

        if (AimReferenceObject == null || ObjectToAim == null)
        {
            print("need to set aim ref at the player eyes, and hand ref at the hand");
            return;
        }


        //We make a "default" target point for the raycast, if nothing is hit.
        Vector3 HitPos = AimReferenceObject.transform.position + AimReferenceObject.transform.forward * raycastRange;
        RaycastHit hit;

        //If hit, override this
        if (Physics.Raycast(AimReferenceObject.transform.position, AimReferenceObject.transform.forward, out hit, raycastRange, layerMask))
        { 
            HitPos = hit.point;
        }

        //show where we aim
        hitPointIndicatorObject.transform.position = HitPos;

        //Rotate Hand towards the aim point with some lerp. (SHould be capped to a certain amount of degrees)
        Quaternion targetHeldGunAim  = Quaternion.LookRotation(HitPos - ObjectToAim.transform.position, Vector3.up);
        ObjectToAim.transform.rotation = Quaternion.Lerp(ObjectToAim.transform.rotation, targetHeldGunAim, Time.deltaTime * heldGunAimLerp);


        //For showing where it will ACTUALLY hit, aimed from the hand
        RaycastHit actualHit;
        Vector3 actualHitPos = ObjectToAim.transform.position + ObjectToAim.transform.forward * raycastRange;

        if (Physics.Raycast(ObjectToAim.transform.position, ObjectToAim.transform.TransformDirection(Vector3.forward), out actualHit, raycastRange, layerMask))
        {
            actualHitPos = actualHit.point;
        }
        gunRayIndicatorObject.transform.position = actualHitPos;
    }
}
