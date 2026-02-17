using System;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EpicMusicSystem : MonoBehaviour
{   

    public TextMeshPro devMusicUi;
    public bool disableDebugLog = true;

    public AudioSource audBase;
    public AudioSource audHit;
    public AudioSource audLead;
    public AudioSource audBuild;


    public float maxVol = 0.2f;

    public float bpm;
    public float hitFreq;
    private float hitNow;
    private float leadNow;
    public float leadFreq;

    private bool isHit = false;
    private bool isBuild = false;
    private bool bufferHit = false;
    private bool isLead = false;
    private bool bufferLead = false;


    public float musicFadeLerp = 80f;

    public bool playMusic = true;


    //for dps over triggering
    public float heatToTriggerLead = 10f;
    public float heatMax = 30f;
    public float heatReductionMult = 0.3f;
    private float heatNow = 0f;

    public bool allowDevKeys = false;



    void Start()
    {
        audBase.volume = maxVol;
        audHit.volume = 0;
        audLead.volume = 0;
        audBuild.volume = 0;
    }


    public void startPlayingMusic()
    {
        playMusic = true;
    }

    public void stopPlayingMusic()
    {
        playMusic = false;
    }


    void Update()
    {
        //try no loop this instead for detect end and reset bpm-------------------------------------
        if (!audBase.isPlaying && playMusic)
        {
            audBase.Play();
            audHit.Play();
            audLead.Play();
            audBuild.Play();
            leadNow = 0;
            hitNow = 0;
        }


        //BPM things------------------------------------------------------------------------------
        float bpmInvert = bpm / 60f;

        float delta = Time.deltaTime;

        leadNow += delta * bpmInvert;
        hitNow += delta * bpmInvert;

        //Gain heat On Hit-----------
        heatNow -= delta * heatReductionMult;
        heatNow = Mathf.Clamp(heatNow, 0, heatMax);

        if (heatNow > heatToTriggerLead) bufferLead = true;


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

        //build if lead buffers-----------------------------------------------------------------
        isBuild = bufferLead && !isLead;

        //APPLY---------------------------------------------------------------------------------
        lerpVolume(audLead, isLead);
        lerpVolume(audHit, isHit);
        lerpVolume(audBuild, isBuild);




        //DEV----------------------------------------------------------------------------------
        if (allowDevKeys)
        {
         
            if (Input.GetKeyDown(KeyCode.L))
            {
                bufferLeadSegment();
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                bufferHitSegment();
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                simulateGettingHit();
            }
            
        }


        if (devMusicUi != null) devMusicUi.text = "MUSIC SYSTEM:\n\n";

        int devtextLength = 20;
        devText("BASE: ", audBase.time / audBase.clip.length, devtextLength );
        devText("HITS: ", hitNow / hitFreq, devtextLength );
        devText("LEAD: ", leadNow / leadFreq, devtextLength );


        

        if (devMusicUi != null)
        {
            devMusicUi.text += "\nCURRENT HEAT: " + Mathf.RoundToInt(heatNow).ToString() + " / " + Mathf.RoundToInt(heatToTriggerLead).ToString() + "\n";

            if (isBuild) devMusicUi.text += "\nPlaying Buildup";

            if (bufferLead) devMusicUi.text += "\nBuffering Lead";
            if (isLead) devMusicUi.text += "\nPlaying Lead";

            if (bufferHit) devMusicUi.text += "\n---Buffering Hit";
            if (isHit) devMusicUi.text += "\n---Playing Hit";

            
        }

        
    }




    private void lerpVolume(AudioSource whatSource, bool whatBool)
    {
        float whatVolume = whatBool ? maxVol : 0f;
        whatSource.volume = Mathf.Lerp(whatSource.volume, whatVolume, Time.deltaTime * musicFadeLerp);
    }




    private void devText(string categoryName, float percentage, int amount)
    {   

        int lenghtOut = Mathf.CeilToInt(percentage*amount);
        string devString = categoryName;
        
        for(int i = 0; i < lenghtOut; i++) devString += "-";

        if (!disableDebugLog) print(devString);
        if (devMusicUi != null) devMusicUi.text += devString + "\n";

    }

    //Call these to use with other scripts
    public void bufferHitSegment()
    {
        bufferHit = true;
    }

    public void bufferLeadSegment()
    {
        bufferLead = true;
    }


    public void simulateGettingHit()
    {
        bufferHit = true;
        heatNow += 1f;
    }


    //these are just here, leave them be, they do things
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
