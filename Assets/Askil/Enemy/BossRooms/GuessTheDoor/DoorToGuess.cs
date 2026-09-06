using FMODUnity;
using UnityEngine;

public class DoorToGuess : MonoBehaviour
{
    public GameObject enemyConnected;
    public GuessTheDoor MasterScript;
    public Material RedGlow;
    public Material WhiteGlow;
    public GameObject Light;
    public AudioSource TurnOnSound;
    public StudioEventEmitter turnOnSoundNew;
    public GameObject enemyProperDeath;
    public void ActivateEnemy()
    {
        enemyConnected.SetActive(true);
        MasterScript.sides.Remove(this);

        if (MasterScript.sides.Count == 1) // What happens to the last enemy attacking you?
        {
            HealthEnemy enemyHealthSc = enemyConnected.GetComponentInChildren<HealthEnemy>();
            enemyHealthSc.PostDeathPrefab = enemyProperDeath;
            enemyHealthSc.destroyDelayDeath = 0;
        }

        BecomeInactive();
    }

    public void BecomeActiveDoor()
    {
        foreach (MeshRenderer renderer in Light.GetComponentsInChildren<MeshRenderer>())
        {
           renderer.material = RedGlow;
        }
        print("DoorActivated");
        TurnOnSound.pitch = Random.Range(0.5f, 1.5f);
        TurnOnSound.Play();
        turnOnSoundNew.Play();
    }

    public void BecomeInactive()
    {
        foreach (MeshRenderer renderer in Light.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material = WhiteGlow;
        }
    }
}
