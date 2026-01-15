using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class WorkBench : MonoBehaviour
{
    [Header("Assign in prefab")]
    public List<AudioSource> SFXs = new List<AudioSource>();
    public GameObject CinematicCamPrefab;
    public float CutSceneDuration;

    [Header("Assign in scene")]
    public int CorrectBuildId;
    public GameObject SpawnWeaponTransform;
    public GameObject WeaponToSpawn;
    public GameObject CinematicTargetPosition;

    private Weapon_Builder weapon_BuilderSC;

    void OnEnable()
    {
        weapon_BuilderSC = GameObject.Find("WeaponBuilder").GetComponent<Weapon_Builder>();

        bool MadeWeapon = weapon_BuilderSC.CollectedWeaponsId.Contains(CorrectBuildId);

        print(weapon_BuilderSC.CurrentBuildId);

        if (weapon_BuilderSC.CurrentBuildId == CorrectBuildId && !MadeWeapon)
        {
            SpawnCinematicCam();
            StartCoroutine(SpawnWeapon());
        }

        else OnBuildFail();
    }

    private IEnumerator SpawnWeapon()
    {
        Instantiate(WeaponToSpawn, SpawnWeaponTransform.transform.position, SpawnWeaponTransform.transform.rotation);
        SFXs[1].Play();
        weapon_BuilderSC.CollectedWeaponsId.Add(CorrectBuildId);

        yield return null;
    }

    private void OnBuildFail()
    {
        SFXs[0].Play();
    }

    private void SpawnCinematicCam()
    {
        GameObject CamPrefab = Instantiate(CinematicCamPrefab, Vector3.zero, Quaternion.identity);
        CinematicCam CineCamScript = CamPrefab.GetComponent<CinematicCam>();

        CineCamScript.SetReferences(CinematicTargetPosition, CutSceneDuration);
    }
}
