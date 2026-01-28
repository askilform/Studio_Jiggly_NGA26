using System;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;

public class EpicMusicSystem : MonoBehaviour
{

    public AudioSource audBase;
    public AudioSource audHit;
    public AudioSource audLead;

    public GameObject visBase;
    public GameObject visHit;
    public GameObject visLead;

    public float bpm;
    public float hitFreq;
    private float hitNow;
    private float leadNow;
    public float leadFreq;

    public bool ishit;
    public bool bufferHit;
    public bool isLead;
    public bool bufferLead;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float bpmInvert = 1f/(bpm * 60);

        if (Input.GetKeyDown(KeyCode.L))
        {
            bufferLead = true;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            bufferHit = true;
        }

        float delta = Time.deltaTime;

        leadNow += delta;
        hitNow += delta;



        if (hitNow > hitFreq)
        {
            hitNow -= hitFreq;
        }

        if (leadNow > leadFreq)
        {
            leadNow -= leadFreq;
        }


        visHit.transform.position = new Vector3(hitNow, 0, 0 );
        visLead.transform.position = new Vector3(leadNow, 1, 0);



    }

    private void onHit()
    {
        if (bufferHit) 
        {
            ishit = true;
            bufferHit = false;
        }
    }

    private void onLead()
    {
        if (bufferLead) 
        {
            isLead = true;
            bufferLead = false;
        }
    }
}
