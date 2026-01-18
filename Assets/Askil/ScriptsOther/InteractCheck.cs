using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InteractCheck : MonoBehaviour
{
    public GameObject UI;
    public AudioSource interactSfx;

    private bool InInteraction;
    [SerializeField] private Interactable CurrentInteractable;

    private void Start()
    {
        interactSfx = GetComponent<AudioSource>();
    }

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
        PlayInteractSFX();

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
