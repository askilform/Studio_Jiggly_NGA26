using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public GameObject Enemy;
    public List<AudioSource> sfxs = new List<AudioSource> ();
    public enemyMovement enemyMovementSc;
    public CanvasGroup uiGroup;

    [SerializeField] private PlayerDetection playerdetectSc;
    private TextPopUp uiSc;
    private LevelMaster levelMaster;
    public bool enemyActive;

    private void Start()
    {
        uiSc = GameObject.FindFirstObjectByType<TextPopUp>();
        levelMaster = FindFirstObjectByType<LevelMaster>();
        playerdetectSc = FindFirstObjectByType<PlayerDetection>();
        enemyMovementSc = FindFirstObjectByType<enemyMovement>();

        Enemy.SetActive (false);
        enemyActive = false;
        uiGroup.alpha = 0;
    }

    private void FixedUpdate()
    {
        if (enemyActive && uiGroup.alpha < 1) uiGroup.alpha += 0.005f;

        if (!enemyActive && uiGroup.alpha > 0) uiGroup.alpha -= 0.01f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            uiSc.StartCoroutine(uiSc.FlashText("He will hear you running...", 2f, true));
            sfxs[0].Play();
            levelMaster.playerInDangerArea = true;
            StartCoroutine(WaitAndActivate());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            uiSc.StartCoroutine(uiSc.FlashText("You Are Safe, FOR NOW", 2, false));
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
        if (levelMaster.playerInDangerArea)
        {
            Enemy.SetActive(true);
            enemyActive = true;
        }    
    }

    private IEnumerator WaitAndDeactivate()
    {
        enemyActive = false;
        yield return new WaitForSeconds(5);
        if (!levelMaster.playerInDangerArea)
        {
            Enemy.SetActive(false);
        }    
    }
}
