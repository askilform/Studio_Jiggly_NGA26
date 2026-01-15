using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InteractCheck : MonoBehaviour
{
    public GameObject UI;
    private bool InInteraction;
    [SerializeField] private Interactable CurrentInteractable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null)
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
            CurrentInteractable = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CurrentInteractable != null && !InInteraction)
        {
            StartCoroutine(Interact());
        }
    }

    private IEnumerator Interact()
    {
        InInteraction = true;
        UI.SetActive(false);

        {
            foreach (GameObject obj in CurrentInteractable.ToDisable)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in CurrentInteractable.ToEnable)
            {
                obj.SetActive(true);
            }
        }

        yield return new WaitForSeconds(CurrentInteractable.TimeBeforeReset);
        InInteraction = false;

        {
            foreach (GameObject obj in CurrentInteractable.ToDisable)
            {
                obj.SetActive(true);
            }

            foreach (GameObject obj in CurrentInteractable.ToEnable)
            {
                obj.SetActive(false);
            }
        }
    }
}
