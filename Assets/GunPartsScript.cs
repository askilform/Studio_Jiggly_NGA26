using System;
using System.Collections.Generic;
using UnityEngine;

public class GunPartsScript : MonoBehaviour

{
    
    public List<GameObject> partList;
    public Weapon_Builder weaponBuilderIdManager;

    void Start()
    {
        print("Press G to update parts");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            checkParts();
        }
        
    }



    public void checkParts()
    {
        string allPartsString = "PARTS: ";

        foreach(GameObject part in partList)
        {
            if (part.TryGetComponent<SingleGunPartScript>(out SingleGunPartScript partScript))
            {

                allPartsString += partScript.partName + ", ";

                if (weaponBuilderIdManager.IdsPickedUp.Contains(partScript.partID))
                {
                    partScript.gameObject.SetActive(true);
                }
                else
                {
                    partScript.gameObject.SetActive(false);
                }


            }


        }
        print(allPartsString);
    }
}
