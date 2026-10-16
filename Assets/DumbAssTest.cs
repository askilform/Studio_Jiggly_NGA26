using FMOD.Studio;
using FMODUnity;
using UnityEngine;
public class DumbAssTest : MonoBehaviour
{


    [Header("AudioFmod")]
    public EventReference fmodBase;
    public EventInstance fmodBaseInst;
    void Start()
    {
        
        fmodBaseInst = RuntimeManager.CreateInstance(fmodBase);
        fmodBaseInst.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
