using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public GameObject Enemy;
    public List<AudioSource> sfxs = new List<AudioSource> ();
    public enemyMovement enemyMovementSc;

    [SerializeField] private PlayerDetection playerdetectSc;
    private TextPopUp uiSc;
    private LevelMaster levelMaster;

    private void Start()
    {
        uiSc = GameObject.FindFirstObjectByType<TextPopUp>();
        levelMaster = FindFirstObjectByType<LevelMaster>();
        playerdetectSc = FindFirstObjectByType<PlayerDetection>();
        enemyMovementSc = FindFirstObjectByType<enemyMovement>();

        Enemy.SetActive (false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            uiSc.StartCoroutine(uiSc.FlashText("He Can Hear you! Don't Sprint....", 2f));
            sfxs[0].Play();
            levelMaster.playerInDangerArea = true;
            StartCoroutine(WaitAndActivate());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            uiSc.StartCoroutine(uiSc.FlashText("You Are Safe, FOR NOW", 2));
            sfxs[1].Play();
            levelMaster.playerInDangerArea = false;
            enemyMovementSc.investigating = false;

            if (enemyMovementSc.mainTarget.tag == "Player")
            {
                playerdetectSc.OnPlayerLost();
            }

            StartCoroutine(WaitAndDeactivate());
        }
    }
    private IEnumerator WaitAndActivate()
    {
        yield return new WaitForSeconds(2);
        if (levelMaster.playerInDangerArea) Enemy.SetActive(true);
    }

    private IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(5);
        if (!levelMaster.playerInDangerArea) Enemy.SetActive(false);
    }
}
