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



    //For Aiming
    [HideInInspector] public bool aiming = false;

    public Vector3 aimOffset = new Vector3(0.3f, 0, -0.2f);

    public float aimTilt = 105f;
    private float appliedTilt = 0f;


    private Vector3 startPos;

    public Camera gameCamera;
    private float startFOV = 75f;
    public float aimFovAdd = 20f;

    public float allThisAimShitLerpIn = 8f;
    public float allThisAimShitLerpOut = 2f;
    public float fovLerpIn = 1.5f;
    public float fovLerpOut = 8f;

    private void Start()
    {
        if(gameCamera != null)
        {
            startFOV = gameCamera.fieldOfView;
        }
        

        startPos = ObjectToAim.transform.localPosition;
        
    }

    void Update()
    {

        if (AimReferenceObject == null || ObjectToAim == null)
        {
            print("need to set aim ref at the player eyes, and hand ref at the hand");
            return;
        }



        //For aiming gun cool style
        Vector3 targetPos = startPos;
        float targetTilt = 0f;
        float targetFOV = startFOV;
        float fovLerpNow = fovLerpOut;
        float allThisAimShitLerpNow = allThisAimShitLerpOut;

        if (aiming)
        {
            targetFOV = startFOV + aimFovAdd;
            targetPos = startPos + aimOffset;
            targetTilt = aimTilt;
            fovLerpNow = fovLerpIn;
            allThisAimShitLerpNow = allThisAimShitLerpIn;
        }

        ObjectToAim.transform.localPosition = Vector3.Lerp(ObjectToAim.transform.localPosition, targetPos, Time.deltaTime * allThisAimShitLerpNow);
        appliedTilt = Mathf.Lerp(appliedTilt, targetTilt, Time.deltaTime * allThisAimShitLerpNow);

        //Zoom in or out on lerp

        if(gameCamera != null)
        {
            gameCamera.fieldOfView = Mathf.Lerp(gameCamera.fieldOfView, targetFOV, Time.deltaTime * fovLerpNow);

        }




        //_________
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

        Vector3 tiltedUpDirection = Quaternion.AngleAxis(-appliedTilt, gameObject.transform.forward) * gameObject.transform.up;

        Quaternion targetHeldGunAim  = Quaternion.LookRotation(HitPos - ObjectToAim.transform.position, tiltedUpDirection);
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
