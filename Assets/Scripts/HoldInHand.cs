using TMPro;
using UnityEngine;

public class HoldInHand : MonoBehaviour
{

    public GameObject currentHeldObject;

    public InteractCheck interactCheck;

    private LayerMask memoryLayerExclusion;
    public LayerMask excludeLayersWhenHeld;

    public TextMeshProUGUI heldText;

    public Transform throwFromHere;

    public Animator RightHandAnims;

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
                RightHandAnims.SetTrigger("Pickup");

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
                string holdName = "Undefined Object";
                string holdToss = "Drop";
                string holdUse = "Use";
                bool showUseMessage = true;

                if (currentHeldObject.TryGetComponent<Interactable>(out Interactable interactScript))
                {
                    holdName = interactScript.heldInHandName;
                    holdToss = interactScript.heldInHandTossMessage;
                    holdUse = interactScript.heldInHandUseMessage;
                    showUseMessage = interactScript.showUseMessage;
                }


                //old
                //heldText.text = " OBJECT:\r\n|-" + holdName + "\r\n    |\r\n    |- G: " + holdToss;
                
                //new
                heldText.text = "[" + holdName + "]\r\n ---[G] " + holdToss;

                if (showUseMessage)
                {
                    heldText.text += "\r\n ---[F] " + holdUse;
                }

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

                //default throw force
                float throwForceOut = 3.0f;

                if (throwFromHere != null)
                {
                    if (currentHeldObject.TryGetComponent<Interactable>(out Interactable interactibleScript))
                    {
                        if (interactibleScript.throwFromCenterOfScreen)
                        {
                            //center on screen if thing says so
                            
                            rb.gameObject.transform.position = throwFromHere.position;
                            
                        }
                        //Override throw force in object
                        throwForceOut = interactibleScript.throwForce;
                    }
                    
                }
                //Toss the salad
                rb.linearVelocity = transform.forward * throwForceOut;


            }

            currentHeldObject = null;

        }

    }
}
