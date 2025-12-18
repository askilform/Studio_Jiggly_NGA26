using UnityEngine;

public class StartRandomTime : MonoBehaviour
{
    void Start()
    {
        AudioSource AS = GetComponent<AudioSource>();
        AS.time = Random.Range(0.0f, AS.clip.length);
    }
}
