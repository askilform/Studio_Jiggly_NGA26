using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class GunPartCheck : MonoBehaviour
{
    public int CorrentPartAmount;
    public GameObject SpawnFrom;
    public GameObject WeaponToSpawn;
    void OnEnable()
    {
        Weapon_Builder weapon_BuilderSC = GameObject.Find("WeaponBuilder").GetComponent<Weapon_Builder>();

        if (weapon_BuilderSC.partsPickedUp.Count == CorrentPartAmount)
        {
            SpawnWeapon();
        }
    }

    private void SpawnWeapon()
    {
        Instantiate(WeaponToSpawn, SpawnFrom.transform.position, SpawnFrom.transform.rotation);
    }

}
