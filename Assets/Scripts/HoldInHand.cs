using TMPro;
using UnityEngine;

public class HoldInHand : MonoBehaviour
{

    public GameObject currentHeldObject;

    public InteractCheck interactCheck;

    private LayerMask memoryLayerExclusion;
    public LayerMask excludeLayersWhenHeld;

    public TextMeshProUGUI heldText;

    void Start()
    {
        
    }

    void Update()
    {

        bool tryUseObject = Input.GetKeyDown(KeyCode.F);

        if (interactCheck != null)
        {
            if (interactCheck.CurrentInteractable != null && Input.GetKeyDown(KeyCode.E))

            {

                if (currentHeldObject != null)
                {
                    dropObject();
                }

                //Pick Up
                if (interactCheck.CurrentInteractable.canBeHeldInHand == false)
                {
                    return;
                }

                currentHeldObject = interactCheck.CurrentInteractable.gameObject;

                if (currentHeldObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {

                    rb.useGravity = false;
                    rb.isKinematic = true;
                    memoryLayerExclusion = rb.excludeLayers;
                    rb.excludeLayers = excludeLayersWhenHeld;
                }
                
            }

        }
        else
        {
            print("please connect HOLD IN HAND to the INTERACT CHECK");
        }




        if (currentHeldObject != null)
        {

            //HOLDING THINGS:
            currentHeldObject.transform.position = transform.position;
            currentHeldObject.transform.rotation = transform.rotation;
            currentHeldObject.transform.parent = gameObject.transform;


            if (tryUseObject) 
            { 
                if (currentHeldObject.TryGetComponent<UseWhileHeld>(out UseWhileHeld uwh))
                {
                    uwh.triggerUseWhileHeld();
                    
                }
            }

            if (currentHeldObject.TryGetComponent<WeaponPart>(out WeaponPart weaponPart))
            {
                interactCheck.heldWeaponPartRef = weaponPart;
            }

            

        }





        if (Input.GetKeyDown(KeyCode.G))
        {
            dropObject();
        }




        if (heldText != null)
        {
            if (currentHeldObject == null)
            {
                heldText.text = "";
            }
            else
            {
                heldText.text = " OBJECT:\r\n|-" + currentHeldObject.name + "\r\n    |\r\n    |- F: use\r\n    |- V: drop";
            }
            
        }


    }

        



    private void dropObject()
    {
        if (currentHeldObject != null)
        {

            currentHeldObject.transform.parent = null;

            if (currentHeldObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.useGravity = true;
                rb.isKinematic = false;

                rb.excludeLayers = memoryLayerExclusion;

                rb.linearVelocity = transform.forward * 3f;
            }

            currentHeldObject = null;

        }

    }
}
