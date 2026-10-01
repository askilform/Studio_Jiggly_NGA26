using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.AI.Navigation;

public class Weapon_Builder : MonoBehaviour
{
    public List<int> IdsPickedUp = new List<int>();
    public List<int> CollectedWeaponsId = new List<int>();
    public int CurrentBuildId;

    public bool autoPickup = false;

    public int GetGunIdOnStart;

    // public UiWeaponsParts uiWeaponPartSc;
    public List<UiWeaponsParts> weaponsPartsList = new List<UiWeaponsParts>();

    public UnityEvent ifHasEntireGun;

    private IEnumerator Start()
    {
        if (GameInstance.gunShowcase) GetGunIdOnStart = 123456;

        if (GetGunIdOnStart != 0)
        {
            foreach (char digit in GetGunIdOnStart.ToString())
            {
                int value = digit - '0';
                IdsPickedUp.Add(value);

                foreach (UiWeaponsParts partScs in weaponsPartsList)
                {

                    partScs.colorUiPart(value);
                }

                Debug.Log(value);
            }

            if (GetGunIdOnStart == 123456)
            {
                foreach (BreakableEndgame breakables in FindObjectsByType<BreakableEndgame>(sortMode: FindObjectsSortMode.None))
                {
                    breakables.OnBattleBegin();
                }

                FindFirstObjectByType<NavMeshSurface>().BuildNavMesh();

                ifHasEntireGun.Invoke();
            }
        }



        else foreach (int i in GameInstance.savedWeaponIds)
        {
                IdsPickedUp.Add((int)i);

                yield return null;

            foreach (UiWeaponsParts partScs in weaponsPartsList)
            {
                partScs.colorUiPart((int)i);
            }

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

        foreach (UiWeaponsParts partScs in weaponsPartsList)
        {

            partScs.colorUiPart(WeaponPartScript.id);
        }
        
        WeaponPartScript.OnPickup();

        IdsPickedUp.Sort();
        CurrentBuildId = int.Parse(string.Concat(IdsPickedUp));
        print(CurrentBuildId);

        GameInstance.savedWeaponIds = IdsPickedUp.ToArray();
        foreach (int i in GameInstance.savedWeaponIds) print ("Weapon saved: " + i);
    }
}
