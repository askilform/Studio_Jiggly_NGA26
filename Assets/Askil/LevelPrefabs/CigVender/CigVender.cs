using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CigVender : MonoBehaviour
{
    public GameObject CigPrefab;
    public GameObject CigSpawnPosition;
    public List<Material> CigMaterials = new List<Material>();
    public float coolDownTime;
    public TextMeshPro coolDownText;

    float coolDown;

    public void VendCig()
    {
        if (coolDown < 0f)
        {
            GameObject cig = Instantiate(CigPrefab, CigSpawnPosition.transform.position, Random.rotation);
            cig.GetComponentInChildren<CigThrowScript>().ChangeMeshMaterial(CigMaterials[Random.Range(0, CigMaterials.Count)]);
            coolDown = coolDownTime;
        }
    }

    private void Update()
    {
        if (coolDown >  -0.1) coolDown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        coolDownText.text = (coolDown > 0f) ? coolDown.ToString("F2") : "Ready To Vend!";
    }
}
