using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public GameObject Enemy;
    public List<AudioSource> sfxs = new List<AudioSource> ();
    public enemyMovement enemyMovementSc;

    private PlayerDetection playerdetectSc;
    private TextPopUp uiSc;
    private LevelMaster levelMaster;

    private void Start()
    {
        uiSc = GameObject.FindFirstObjectByType<TextPopUp>();
        levelMaster = FindFirstObjectByType<LevelMaster>();
        playerdetectSc = FindFirstObjectByType<PlayerDetection>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            uiSc.StartCoroutine(uiSc.FlashText("You Are In Danger!", 0.5f));
            sfxs[0].Play();
            levelMaster.playerInDangerArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            uiSc.StartCoroutine(uiSc.FlashText("You Are Safe, FOR NOW", 2));
            sfxs[1].Play();
            levelMaster.playerInDangerArea = false;
            enemyMovementSc.Roam();
        }
    }
}
