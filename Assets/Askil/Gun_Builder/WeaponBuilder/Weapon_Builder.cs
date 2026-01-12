using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon_Builder : MonoBehaviour
{
    public List<string> partsPickedUp = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WeaponPart>() != null)
        {
            AddToInventory(other.gameObject);
            print(partsPickedUp.Count);
        }
    }

    private void AddToInventory(GameObject weaponPartObject)
    {
        WeaponPart WeaponPartScript = weaponPartObject.GetComponent<WeaponPart>();

        partsPickedUp.Add(WeaponPartScript.partName);
        WeaponPartScript.OnPickup();
    }
}
