using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InteractCheck : MonoBehaviour
{
    public GameObject UI;
    public Animator animator;
    [SerializeField] private Interactable CurrentInteractable;
    bool isInteracting;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null && !isInteracting)
        {
            UI.SetActive(true);
            CurrentInteractable = other.GetComponent<Interactable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
            UI.SetActive(false);

            if (!isInteracting) CurrentInteractable = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CurrentInteractable != null && !isInteracting)
        {
            StartCoroutine(Interact());
        }
    }

    private IEnumerator Interact()
    {
        UI.SetActive(false);        
        isInteracting = true;
        animator.SetBool(CurrentInteractable.ArmAnimName, true);

        foreach (GameObject obj in CurrentInteractable.ToDisable)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in CurrentInteractable.ToEnable)
        {
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(CurrentInteractable.timeActive);

        foreach (GameObject obj in CurrentInteractable.ToDisable)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in CurrentInteractable.ToEnable)
        {
            obj.SetActive(false);
        }

        animator.SetBool(CurrentInteractable.ArmAnimName, false);
        isInteracting = false;
    }
}
