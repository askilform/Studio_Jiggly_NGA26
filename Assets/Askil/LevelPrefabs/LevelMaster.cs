using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    [Header("Enemy")]




    [Header("Music")]
    public AudioSource ChillMusic;
    public AudioSource DangerMusic;
    public AudioSource ChaseMusic;

    public float playerWalkMultiplier;

    public void FadeBetweenMusic(AudioSource currentMusic, AudioSource nextMusic)
    {

    }
}
