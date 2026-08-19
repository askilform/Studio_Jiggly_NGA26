using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractCheck : MonoBehaviour
{
    public GameObject UI;
    public AudioSource interactSfx;
    public GameObject playerCam;
    public LayerMask layerMask;

    private bool InInteraction;
    [HideInInspector] public Interactable CurrentInteractable;

    public FuelHolderScript fuelHolderScriptRef;

    [HideInInspector] public WeaponPart heldWeaponPartRef;
    public HoldInHand holdInHandScriptRef;
    public Weapon_Builder weaponBuilderRef;
    public TextMeshProUGUI interactHoverText;

    public bool canPickupFuel = false;

    private void Start()
    {
        interactSfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 6, layerMask))
            {
                UI.SetActive(true);
                CurrentInteractable = other.GetComponent<Interactable>();

                interactHoverText.text = CurrentInteractable.hoverMessage;
            }

        }
    }
  

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
            UI.SetActive(false);
            CurrentInteractable = null;
        }
    }
      

    private void Update()
    {
        if (weaponBuilderRef == null) print("please connect INTERACT CHECK to the WEAPON BUILDER");
        if (holdInHandScriptRef == null) print("please connect INTERACT CHECK to the HOLD IN HAND script");

        if (Input.GetKeyDown(KeyCode.E) && CurrentInteractable != null && !InInteraction)
        {
            StartCoroutine(Interact());


            //We now use the fuel cell to reload while held. We don't need this anymore. But dont delete please
            /*
            //------------------------------------------------------------------------------------------------ fuel
            if (CurrentInteractable.CompareTag("AmmoPickup") && fuelHolderScriptRef != null)
            {
                print("pick up and reload fuel");

                if (canPickupFuel)
                {

                    fuelHolderScriptRef.ReloadFuel();

                    Destroy(CurrentInteractable.gameObject);
                }
            }
            */
            

            //BUILD PART WHILE AT WORKBENCH
            // --------------------------------------------------------------------------------------------------------------------- !!!!!!!!!!
            if (CurrentInteractable.CompareTag("WorkBench"))
            {
                print("WORKBENCH");

                if (heldWeaponPartRef != null && weaponBuilderRef != null && holdInHandScriptRef != null)
                {
                    weaponBuilderRef.AddToInventory(heldWeaponPartRef.gameObject);

                    heldWeaponPartRef = null;
                    holdInHandScriptRef.currentHeldObject = null;
                }

            }


        }

        /*                  ----- WORK IN PROGESS (RAYCAST TO INTERACTCHECK) ------
        RaycastHit hit;
        // --------------------------------------------------------------------------------------------------------------------- !!!!!!!!!!!!
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 4, layerMask))
        {
            if (hit.transform.GetComponentInChildren<Interactable>() != null)
            {
                UI.SetActive(true);
                CurrentInteractable = hit.transform.GetComponentInChildren<Interactable>();

                interactHoverText.text = CurrentInteractable.hoverMessage;
            }

            else
            {
                if (hit.transform.GetComponentInChildren<Interactable>() != null)
                {
                    UI.SetActive(false);
                    CurrentInteractable = null;
                }
            }
        }

        else
        {
            UI.SetActive(false);
            CurrentInteractable = null;
        }

        */
    }

    private IEnumerator Interact()
    {
        InInteraction = true;
        UI.SetActive(false);
        PlayInteractSFX();
        CurrentInteractable.onEnable.Invoke();

        {
            if (CurrentInteractable.ToDisable != null) foreach (GameObject obj in CurrentInteractable.ToDisable)
            {
                obj.SetActive(false);
            }

            if (CurrentInteractable.ToEnable != null)  foreach (GameObject obj in CurrentInteractable.ToEnable)
            {
                obj.SetActive(true);
            }
        }

        yield return new WaitForSeconds(CurrentInteractable.TimeBeforeReset);
        InInteraction = false;

        if (CurrentInteractable.resetAfterTime)
        {
            {
                if (CurrentInteractable.ToDisable != null) foreach (GameObject obj in CurrentInteractable.ToDisable)
                    {
                        obj.SetActive(true);
                    }

                if (CurrentInteractable.ToEnable != null) foreach (GameObject obj in CurrentInteractable.ToEnable)
                    {
                        obj.SetActive(false);
                    }
            }
        }
    }

    public void PlayInteractSFX()
    {
        interactSfx.Play();
    }

}
