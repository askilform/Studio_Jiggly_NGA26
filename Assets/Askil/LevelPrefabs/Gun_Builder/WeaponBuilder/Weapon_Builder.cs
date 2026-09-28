using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class Weapon_Builder : MonoBehaviour
{
    public List<int> IdsPickedUp = new List<int>();
    public List<int> CollectedWeaponsId = new List<int>();
    public int CurrentBuildId;

    public bool autoPickup = false;

    public int GetGunIdOnStart;

    public UiWeaponsParts uiWeaponPartSc;

    public UnityEvent ifHasEntireGun;

    private IEnumerator Start()
    {

        if (GetGunIdOnStart != 0)
        {
            foreach (char digit in GetGunIdOnStart.ToString())
            {
                int value = digit - '0';
                IdsPickedUp.Add(value);
                uiWeaponPartSc.colorUiPart(value);
                Debug.Log(value);
            }

            if (GetGunIdOnStart == 123456) ifHasEntireGun.Invoke();
        }



        else foreach (int i in GameInstance.savedWeaponIds)
        {
                IdsPickedUp.Add((int)i);

                yield return null;
                uiWeaponPartSc.colorUiPart((int)i);
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
        uiWeaponPartSc.colorUiPart(WeaponPartScript.id);
        WeaponPartScript.OnPickup();

        IdsPickedUp.Sort();
        CurrentBuildId = int.Parse(string.Concat(IdsPickedUp));
        print(CurrentBuildId);

        GameInstance.savedWeaponIds = IdsPickedUp.ToArray();
        foreach (int i in GameInstance.savedWeaponIds) print ("Weapon saved: " + i);
    }
}
