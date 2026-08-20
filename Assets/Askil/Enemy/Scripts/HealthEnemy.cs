using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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
    public GameObject CripplePrefab;
    public GameObject hitParticles;

    public UnityEvent onDeath;

    private Vector3 ogScale;

    private void Start()
    {
        print("EnemyStart");
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        ogScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8)) StartCoroutine(Death());
    }

    public void TakeDamage(int Damage, Vector3 hitLocation)
    {
        Health -= Damage;

        if (Health <= 0) StartCoroutine(Death());
        else
        {
            StartCoroutine(DamageVisuals(hitLocation));
            movementSc.SprintFollow();
        }
    }

    public IEnumerator Death()
    {
        onDeath.Invoke();
        print("Dead");
        movementSc.agent.enabled = false;

        /* Vector3 LauncDirection = movementSc.agent.transform.position - movementSc.player.transform.position;
        rb.isKinematic = false;
        rb.AddForce(LauncDirection.x, 5, LauncDirection.z, ForceMode.Impulse);
        */

        yield return null;
        Instantiate(DeathPrefab, transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
        
    }

    public void SplatOnlyDontDie()
    {
        Instantiate(CripplePrefab, transform.position, Quaternion.identity);
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

    private IEnumerator DamageVisuals(Vector3 hitLocation)
    {
        // transform.localScale = transform.localScale * 1.05f;

        Instantiate (hitParticles, hitLocation, Quaternion.identity);
        yield return new WaitForSeconds(0.05f);

        transform.localScale = ogScale;
    }
}
