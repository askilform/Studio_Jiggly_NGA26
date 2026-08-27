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

    public GameObject DeathPrefab;
    public GameObject CripplePrefab;
    public GameObject hitParticles;

    public bool StandardDeath = true;
    public UnityEvent onDeath;

    private Vector3 ogScale;

    private void Start()
    {
        print("EnemyStart");
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
        if (StandardDeath)
        {
            onDeath.Invoke();
            print("Dead");
            movementSc.agent.enabled = false;

            yield return null;
            GameObject newDeathPrefab = Instantiate(DeathPrefab, transform.position, Quaternion.identity);
            newDeathPrefab.GetComponent<OnDeath>().DeathWithEnding();
            Destroy(transform.parent.gameObject);
        }
    }

    public void SplatOnlyDontDie()
    {
        Instantiate(CripplePrefab, transform.position, Quaternion.identity);
    }

    private IEnumerator DamageVisuals(Vector3 hitLocation)
    {
        // transform.localScale = transform.localScale * 1.05f;

        Instantiate (hitParticles, hitLocation, Quaternion.identity);
        yield return new WaitForSeconds(0.05f);

        transform.localScale = ogScale;
    }
}
