using System;
using System.Collections.Generic;
using UnityEngine;

public class GunPartsScript : MonoBehaviour

{
    
    public bool autoUpdateEachFrame = true;

    public List<GameObject> partList;
    public Weapon_Builder weaponBuilderIdManager;

    void Start()
    {
        print("Press G to update parts");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CheckParts();
            WritePartsString();
        }

        if (autoUpdateEachFrame)
        {
            CheckParts();
        }
        
    }

    public void WritePartsString()
    {
        string allPartsString = "PARTS: ";
        foreach(GameObject part in partList)
        {
            if (part.TryGetComponent<SingleGunPartScript>(out SingleGunPartScript partScript))
            {
                allPartsString += partScript.partName + ", ";
            }
        }
        print(allPartsString);
    }


    public void CheckParts()
    {
        foreach(GameObject part in partList)
        {
            if (part.TryGetComponent<SingleGunPartScript>(out SingleGunPartScript partScript))
            {
                partScript.gameObject.SetActive(weaponBuilderIdManager.IdsPickedUp.Contains(partScript.partID));
            }
        }
    }
}
