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
    [SerializeField] MeshRenderer[] renderers;
    [SerializeField] List<Material> OgMats = new List<Material>();

    private Rigidbody rb;

    private void Start()
    {
        // Materials
        enemyMesh = GetComponent<MeshRenderer>();
        originalMaterial = enemyMesh.material;
        renderers = GetComponentsInChildren<MeshRenderer>();

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8)) TakeDamage(20);
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;

        if (Health <= 0) Death();
        else StartCoroutine(MatFlash());
    }

    public void Death()
    {
        print("Dead");
        movementSc.agent.enabled = false;
        
        Vector3 LauncDirection = movementSc.agent.transform.position - movementSc.player.transform.position;
        rb.isKinematic = false;
        rb.AddForce(LauncDirection.x, 5, LauncDirection.z, ForceMode.Impulse);
    }

    private IEnumerator MatFlash()
    {
        foreach (MeshRenderer renderer in renderers)
        {
            OgMats.Add(renderer.material);
            renderer.material = HitMat;
        }

        enemyMesh.material = HitMat;

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = OgMats[i];
        }

        enemyMesh.material = originalMaterial;
    }
}
