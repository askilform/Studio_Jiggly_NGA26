using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using FMODUnity;

public class DarkMenu : MonoBehaviour
{

    public GameObject spinthis;
    public float spinspin = 0.5f;
    public float spintwist = 1f;

    public string[] sentences;
    public int whatSentence = 0;
    public string currentSentence = "";

    public float letterCooldown = 0.1f;
    float letterNow = 0.0f;

    public TextMeshProUGUI textmesh;

    public StudioEventEmitter tickSound;

    [SerializeField] private string playLevel;


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            print("JUMOP");
        }
        Debug.Log("DSFHDFG");
    }

    void Update()
    {
        float delta = Time.deltaTime;
        Vector3 newspin = new Vector3(spinthis.transform.eulerAngles.x, spinthis.transform.eulerAngles.y + delta * spinspin, spinthis.transform.eulerAngles.z + delta * spintwist);
        spinthis.transform.eulerAngles = newspin;



        letterNow += Time.deltaTime;
        
        if (letterNow > letterCooldown)
        {
            letterNow = 0f;

            if (currentSentence.Length < sentences[whatSentence].Length)
            {
                currentSentence = sentences[whatSentence].Substring(0, currentSentence.Length + 1);
                playTickSound();
            }
        }

        

        textmesh.text = currentSentence;

    }


    private void playTickSound()
    {
        tickSound.Play();
    }

    public void NextSentence()
    {
        whatSentence++;
        currentSentence = "";
    }

}
