using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
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


    float musicFadeLerp = 60f;

    public bool playMusic = true;


    void Start()
    {
        audBase.volume = maxVol;
        audHit.volume = 0;
        audLead.volume = 0;
        audBuild.volume = 0;
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
        if (Input.GetKeyDown(KeyCode.L))
        {
            bufferLead = true;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            bufferHit = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            bufferHit = true;
            bufferLead = true;
        }


        if (devMusicUi != null) devMusicUi.text = "MUSIC SYSTEM:\n";

        int devtextLength = 20;
        devText("BASE: ", audBase.time / audBase.clip.length, devtextLength );
        devText("HITS: ", hitNow / hitFreq, devtextLength );
        devText("LEAD: ", leadNow / leadFreq, devtextLength );



        if (devMusicUi != null && isBuild) devMusicUi.text += "\nPlaying Buildup";

        if (devMusicUi != null && bufferLead) devMusicUi.text += "\nBuffering Lead";
        if (devMusicUi != null && isLead) devMusicUi.text += "\nPlaying Lead";

        if (devMusicUi != null && bufferHit) devMusicUi.text += "\n---Buffering Hit";
        if (devMusicUi != null && isHit) devMusicUi.text += "\n---Playing Hit";
        
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
