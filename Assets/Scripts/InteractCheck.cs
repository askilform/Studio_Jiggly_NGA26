using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InteractCheck : MonoBehaviour
{
    public GameObject UI;
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
        if (Input.GetKeyDown(KeyCode.E) && CurrentInteractable != null)
        {
            StartCoroutine(Interact());
        }
    }

    private IEnumerator Interact()
    {
        foreach (GameObject obj in CurrentInteractable.ToDisable)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in CurrentInteractable.ToEnable)
        {
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(3);

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
