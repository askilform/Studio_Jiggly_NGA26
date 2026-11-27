using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InteractCheck : MonoBehaviour
{
    public GameObject UI;
    public GameObject playerCam;
    public LayerMask layerMask;
    public UnityEvent InteractAudioEvent;

    private bool InInteraction;
    public Interactable CurrentInteractable;

    public FuelHolderScript fuelHolderScriptRef;

    [HideInInspector] public WeaponPart heldWeaponPartRef;
    public HoldInHand holdInHandScriptRef;
    public Weapon_Builder weaponBuilderRef;
    public TextMeshProUGUI interactHoverText;

    public bool canPickupFuel = false;

    public Animator RightArmAnims;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null && other.gameObject.GetComponent<Interactable>().ActiveInteractable)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 6, layerMask))
            {
                CurrentInteractable = other.GetComponent<Interactable>();

                interactHoverText.text = CurrentInteractable.hoverMessage;
            }

        }
    }
  

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
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

                    RightArmAnims.SetBool("Holding", false);
                }

            }
        }
    }

    private void FixedUpdate()
    {
        UI.SetActive(CurrentInteractable != null);
    }

    private IEnumerator Interact()
    {
        InInteraction = true;

        PlayInteractSFX();
        CurrentInteractable.onEnable.Invoke();

        // Changes layer so player doesnt try to interact with held object
        // New interactCheck Position overlaps with held objects

        if (CurrentInteractable.canBeHeldInHand) CurrentInteractable.gameObject.layer = 12;

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
        InteractAudioEvent.Invoke();
    }

}
