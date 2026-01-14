using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon_Builder : MonoBehaviour
{
    public List<int> IdsPickedUp = new List<int>();
    public List<int> CollectedWeaponsId = new List<int>();
    public int CurrentBuildId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WeaponPart>() != null)
        {
            AddToInventory(other.gameObject);
        }
    }

    private void AddToInventory(GameObject weaponPartObject)
    {
        WeaponPart WeaponPartScript = weaponPartObject.GetComponent<WeaponPart>();

        IdsPickedUp.Add(WeaponPartScript.id);
        WeaponPartScript.OnPickup();

        IdsPickedUp.Sort();
        CurrentBuildId = int.Parse(string.Concat(IdsPickedUp));
        print(CurrentBuildId);
    }
}
