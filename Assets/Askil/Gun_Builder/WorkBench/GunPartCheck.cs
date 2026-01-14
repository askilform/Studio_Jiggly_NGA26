using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class GunPartCheck : MonoBehaviour
{
    public int CorrentPartAmount;
    public GameObject SpawnFrom;
    public GameObject WeaponToSpawn;
    public List <AudioSource> SFXs = new List <AudioSource>();
    void OnEnable()
    {
        Weapon_Builder weapon_BuilderSC = GameObject.Find("WeaponBuilder").GetComponent<Weapon_Builder>();

        if (weapon_BuilderSC.partsPickedUp.Count == CorrentPartAmount)
        {
            SpawnWeapon();
        }

        else OnBuildFail();
    }

    private void SpawnWeapon()
    {
        Instantiate(WeaponToSpawn, SpawnFrom.transform.position, SpawnFrom.transform.rotation);
        SFXs[1].Play();
    }

    private void OnBuildFail()
    {
        SFXs[0].Play();
    }
}
