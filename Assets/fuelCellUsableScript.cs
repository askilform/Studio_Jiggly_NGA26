using UnityEngine;

public class fuelCellUsableScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void reloadWithThis()
    {
        GameObject reloadThis = GameObject.FindWithTag("Reloadme");
        if (reloadThis.TryGetComponent<FuelHolderScript>(out FuelHolderScript reloadthisscript))
        {
            reloadthisscript.ReloadFuel();
        }

        GameObject holdScriptObject = GameObject.FindWithTag("GrabScriptThing");
        if (holdScriptObject.TryGetComponent<HoldInHand>(out HoldInHand hand))
        {
            Destroy(hand.currentHeldObject.gameObject);
            hand.currentHeldObject = null;
        }
    }

}
