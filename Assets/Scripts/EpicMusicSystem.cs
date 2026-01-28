using System;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;
using UnityEngine.Rendering;

public class EpicMusicSystem : MonoBehaviour
{

    public AudioSource audBase;
    public AudioSource audHit;
    public AudioSource audLead;

    public GameObject visBase;
    public GameObject visHit;
    public GameObject visLead;

    public float maxVol = 0.2f;

    public float bpm;
    public float hitFreq;
    private float hitNow;
    private float leadNow;
    public float leadFreq;

    public bool isHit = false;
    public bool bufferHit = false;
    public bool isLead = false;
    public bool bufferLead = false;

    float musicFadeLerp = 60f;


    void Start()
    {
        audBase.volume = maxVol;
        audHit.volume = 0;
        audLead.volume = 0;

        audBase.Play();
        audHit.Play();
        audLead.Play();
    }

    void Update()
    {
        //try no loop this instead for detect end and reset bpm
        if (!audBase.isPlaying)
        {
            audBase.Play();
            audHit.Play();
            audLead.Play();
            leadNow = 0;
            hitNow = 0;
        }

        float bpmInvert = bpm / 60f;

        if (Input.GetKeyDown(KeyCode.L))
        {
            bufferLead = true;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            bufferHit = true;
        }

        float delta = Time.deltaTime;

        leadNow += delta * bpmInvert;
        hitNow += delta * bpmInvert;



        if (hitNow > hitFreq)
        {
            hitNow -= hitFreq;
            onHit();
        }

        if (leadNow > leadFreq)
        {
            leadNow -= leadFreq;
            onLead();
        }


        visBase.transform.position = new Vector3(audBase.time / audBase.clip.length, 2, 0 );
        visHit.transform.position = new Vector3(hitNow / hitFreq, 0, 0 );
        visLead.transform.position = new Vector3(leadNow / leadFreq, 1, 0);

        
    
        float leadTargetVol = isLead ? maxVol : 0f;

        float hitTargetVol = isHit ? maxVol : 0f;

        audLead.volume = Mathf.Lerp(audLead.volume, leadTargetVol, delta * musicFadeLerp);

        audHit.volume = Mathf.Lerp(audHit.volume, hitTargetVol, delta * musicFadeLerp);


    }

    private void onHit()
    {

        isHit = bufferHit;
        bufferHit = false;

    }

    private void onLead()
    {

        isLead = bufferLead;
        bufferLead = false;

    }
}
