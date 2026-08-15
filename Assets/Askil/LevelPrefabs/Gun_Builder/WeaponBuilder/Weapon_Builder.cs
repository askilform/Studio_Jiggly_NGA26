using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon_Builder : MonoBehaviour
{
    public List<int> IdsPickedUp = new List<int>();
    public List<int> CollectedWeaponsId = new List<int>();
    public int CurrentBuildId;

    public bool autoPickup = false;


    public bool instantlyBecomeInsane = false;
    public int GetGunIdOnStart;

    private void Start()
    {
        // Recieve entire weapon if checked
        if (GameInstance.gunShowcase || instantlyBecomeInsane)
        {
            IdsPickedUp.Add(1);
            IdsPickedUp.Add(2);
            IdsPickedUp.Add(3);
            IdsPickedUp.Add(4);
            IdsPickedUp.Add(5);
            IdsPickedUp.Add(6);
        }


        if (GetGunIdOnStart != 0)
        {
            print("Null startID");
            foreach (char digit in GetGunIdOnStart.ToString())
            {
                int value = digit - '0';
                IdsPickedUp.Add(value);
                Debug.Log(value);
            }
        }


        else foreach (int i in GameInstance.savedWeaponIds) IdsPickedUp.Add((int)i);
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

        GameInstance.savedWeaponIds = IdsPickedUp.ToArray();
        foreach (int i in GameInstance.savedWeaponIds) print ("Weapon saved: " + i);
    }
}
