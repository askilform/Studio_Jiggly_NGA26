using UnityEngine;

public class StartRandomTime : MonoBehaviour
{
    AudioSource AS;

    private void OnEnable()
    {
        if (AS == null) AS = GetComponent<AudioSource>();
        AS.time = Random.Range(0.0f, AS.clip.length);
    }
}
