using UnityEngine;

public class disableOrEnableBasedOnInventory : MonoBehaviour
{
    public Weapon_Builder weaponBuilderIdManager;

    public int[] whenYouHaveAllTheseID;
    public int[] butNotAllTheseID;

    public GameObject[] enableTheseObjects;
    public GameObject[] disableTheseObjects;

    void Update()
    {
        if (weaponBuilderIdManager == null)
        {
            print("no manager. Don't work lol");
            return;
        }


        //It starts of the way it should be:
        bool hasAllTheGoodOnes = true;
        //And then:
        //If any of the needed ID's miss, the conditions are invalid
        foreach(int needID in whenYouHaveAllTheseID)
        {
            if (!weaponBuilderIdManager.IdsPickedUp.Contains(needID))
            {
                hasAllTheGoodOnes = false;
            }
        }
        //If any of the wrong ID's are present, the conditions are invalid
        bool hasAllTheBadOnes = true;
        foreach(int notID in butNotAllTheseID)
        {
            //If any check of as not gotten, we are still unchanged
            if (!weaponBuilderIdManager.IdsPickedUp.Contains(notID))
            {
                hasAllTheBadOnes = false;
            }
        }

        //The effect goes off if you have all you need, and not have ALL the bad ones
        bool enableBool = hasAllTheGoodOnes && !hasAllTheBadOnes;

        //Apply to objects
        foreach(GameObject enableThis in enableTheseObjects)
        {
            enableThis.SetActive(enableBool);
        }
        foreach(GameObject disableThis in disableTheseObjects)
        {
            disableThis.SetActive(!enableBool);
        }
        
    }
}
