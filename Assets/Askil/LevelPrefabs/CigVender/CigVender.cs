using System.Collections.Generic;
using UnityEngine;

public class CigVender : MonoBehaviour
{
    public GameObject CigPrefab;
    public GameObject CigSpawnPosition;
    public List<Material> CigMaterials = new List<Material>();
    public void VendCig()
    {
        GameObject cig = Instantiate(CigPrefab, CigSpawnPosition.transform.position, Random.rotation);
        cig.GetComponentInChildren<CigThrowScript>().ChangeMeshMaterial(CigMaterials[Random.Range(0, CigMaterials.Count)]);
    }
}
