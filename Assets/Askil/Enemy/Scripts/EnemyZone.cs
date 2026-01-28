using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public GameObject Enemy;
    public List<AudioSource> sfxs = new List<AudioSource> ();

    private TextPopUp uiSc;

    private void Start()
    {
        uiSc = GameObject.FindFirstObjectByType<TextPopUp>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") uiSc.StartCoroutine(uiSc.FlashText("You Are In Danger!", 0.5f));
        sfxs[0].Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") uiSc.StartCoroutine(uiSc.FlashText("You Are Safe, FOR NOW", 2));
        sfxs[1].Play();
    }
}
