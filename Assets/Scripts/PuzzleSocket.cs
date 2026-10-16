using FMODUnity;
using System.Collections;
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
    private bool allowLerp;


    void Update()
    {
        if (powered)
        {
            whileConnected.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (allowLerp)
        {
            currentMatch.transform.position = Vector3.Lerp(
                currentMatch.transform.position,
                transform.position,
                0.1f);

            currentMatch.transform.rotation = Quaternion.Lerp(
            currentMatch.transform.rotation,
            transform.rotation,
            0.1f);
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

                StartCoroutine(lerpMovement());

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

    IEnumerator lerpMovement()
    {
        FindFirstObjectByType<HoldInHand>().dropObject();
        currentMatch.GetComponentInChildren<Rigidbody>().isKinematic = true;

        allowLerp = true;
        GetComponent<StudioEventEmitter>().Play();

        yield return new WaitForSeconds(1);

        allowLerp = false;
        currentMatch.transform.position = transform.position;
    }
}
