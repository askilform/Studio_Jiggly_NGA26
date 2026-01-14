using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class WorkBench : MonoBehaviour
{
    public int CorrectBuildId;
    public GameObject SpawnFrom;
    public GameObject WeaponToSpawn;
    public List <AudioSource> SFXs = new List <AudioSource>();

    private Weapon_Builder weapon_BuilderSC;

    void OnEnable()
    {
        weapon_BuilderSC = GameObject.Find("WeaponBuilder").GetComponent<Weapon_Builder>();

        bool MadeWeapon = weapon_BuilderSC.CollectedWeaponsId.Contains(CorrectBuildId);

        print(weapon_BuilderSC.CurrentBuildId);

        if (weapon_BuilderSC.CurrentBuildId == CorrectBuildId && !MadeWeapon)
        {
            SpawnWeapon();
        }

        else OnBuildFail();
    }

    private void SpawnWeapon()
    {
        Instantiate(WeaponToSpawn, SpawnFrom.transform.position, SpawnFrom.transform.rotation);
        SFXs[1].Play();
        weapon_BuilderSC.CollectedWeaponsId.Add(CorrectBuildId);
    }

    private void OnBuildFail()
    {
        SFXs[0].Play();
    }
}
