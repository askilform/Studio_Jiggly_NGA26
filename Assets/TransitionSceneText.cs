using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using FMODUnity;
using UnityEngine.Events;

public class DarkMenu : MonoBehaviour
{

    public string[] sentences;
    public int whatSentence = 0;
    public string currentSentence = "";

    public float letterCooldown = 0.05f;
    float letterNow = 0.0f;

    public TextMeshProUGUI textmesh;

    public StudioEventEmitter tickSound;

    bool readyForNext = false;

    [SerializeField] private string playLevel;

    public UnityEvent triggerWhenDone;
    bool finishLock = false;

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

        bool nextClick = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Return);

        


        letterNow += Time.deltaTime;
        
        if (letterNow > letterCooldown)
        {
            letterNow = 0f;

            if (currentSentence.Length < sentences[whatSentence].Length)
            {
                currentSentence = sentences[whatSentence].Substring(0, currentSentence.Length + 1);
                playTickSound();
            }

            else
            {
                readyForNext = true;
            }

        }

        

        textmesh.text = currentSentence;


        if (nextClick && readyForNext)
        {
            NextSentence();
        };


        print(readyForNext);

    }


    private void playTickSound()
    {
        tickSound.Play();
    }



    public void NextSentence()
    {
        whatSentence++;
        currentSentence = "";

        readyForNext = false;

        if (whatSentence > sentences.Length -1 && finishLock == false)
        {
            triggerWhenDone.Invoke();
            finishLock = true;
        }

        print("sentence: " + whatSentence.ToString());

    }

}
