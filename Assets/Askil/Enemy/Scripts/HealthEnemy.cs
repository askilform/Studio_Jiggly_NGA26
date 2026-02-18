using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthEnemy : MonoBehaviour
{
    public int Health;
    public enemyMovement movementSc;

    // Materials
    public Material HitMat;
    Material originalMaterial;
    MeshRenderer enemyMesh;
    [SerializeField] SkinnedMeshRenderer[] renderers;
    [SerializeField] List<Material> OgMats = new List<Material>();

    public Rigidbody rb;
    public GameObject DeathPrefab;

    private void Start()
    {
        print("EnemyStart");
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8)) StartCoroutine(Death());
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;

        if (Health <= 0) StartCoroutine(Death());
        // else StartCoroutine(MatFlash());
    }

    public IEnumerator Death()
    {
        print("Dead");
        movementSc.agent.enabled = false;
        
        /* Vector3 LauncDirection = movementSc.agent.transform.position - movementSc.player.transform.position;
        rb.isKinematic = false;
        rb.AddForce(LauncDirection.x, 5, LauncDirection.z, ForceMode.Impulse);
        */

        yield return new WaitForSeconds(0.8f);
        Instantiate(DeathPrefab, transform.position, Quaternion.identity);
        Destroy(transform.root.gameObject);
    }

    private IEnumerator MatFlash()
    {
        OgMats.Clear();

        // Store original materials
        foreach (var renderer in renderers)
        {
            OgMats.AddRange(renderer.materials);

            Material[] hitMats = new Material[renderer.materials.Length];
            for (int i = 0; i < hitMats.Length; i++)
            {
                hitMats[i] = HitMat;
            }

            renderer.materials = hitMats;
        }

        yield return new WaitForSeconds(0.1f);

        // Restore materials
        int matIndex = 0;

        foreach (var renderer in renderers)
        {
            int matCount = renderer.materials.Length;
            Material[] originalMats = new Material[matCount];

            for (int i = 0; i < matCount; i++)
            {
                originalMats[i] = OgMats[matIndex++];
            }

            renderer.materials = originalMats;
        }
    }

}
