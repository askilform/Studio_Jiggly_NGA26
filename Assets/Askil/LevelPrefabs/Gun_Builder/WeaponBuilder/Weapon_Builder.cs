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

    public bool autoPickup = false;


    public bool instantlyBecomeInsane = false;

    private void Start()
    {
        if (instantlyBecomeInsane)
        {
            IdsPickedUp.Add(1);
            IdsPickedUp.Add(2);
            IdsPickedUp.Add(3);
            IdsPickedUp.Add(4);
            IdsPickedUp.Add(5);
            IdsPickedUp.Add(6);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (autoPickup && other.GetComponent<WeaponPart>() != null)
        {
            AddToInventory(other.gameObject);
        }
    }

    public void AddToInventory(GameObject weaponPartObject)
    {
        WeaponPart WeaponPartScript = weaponPartObject.GetComponent<WeaponPart>();

        IdsPickedUp.Add(WeaponPartScript.id);
        WeaponPartScript.OnPickup();

        IdsPickedUp.Sort();
        CurrentBuildId = int.Parse(string.Concat(IdsPickedUp));
        print(CurrentBuildId);
    }
}
