using UnityEngine;
using UnityEngine.Events;

public class PuzzleSocket : MonoBehaviour
{
    public UnityEvent whenJustConnected;
    public UnityEvent whileConnected;

    public Transform lerpingCosmicMemoryOfATransform;

    public bool canTakeAnyID = true;
    public int lookForThisID;

    public GameObject currentMatch;
    private bool powered = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    void Update()
    {
        if (currentMatch != null)
        {
            currentMatch.transform.position = transform.position;
            currentMatch.transform.rotation = transform.rotation;
        }


        if (powered)
        {
            whileConnected.Invoke();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        


        if (other.TryGetComponent<PuzzlePlug>(out PuzzlePlug plug))
        {

            if (currentMatch != null)
            {
                print("Has a match already, rejected. Loser.");
                return;
            }

            currentMatch = other.gameObject;

            //Get powered when connected to the right powwered plug, or if any plug is connected.
            if (plug.plugPowered && (canTakeAnyID || plug.plugID == lookForThisID))
            {
                powered = true;
                whenJustConnected.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other == currentMatch)
        {
            currentMatch = null;
        }

    }

}
