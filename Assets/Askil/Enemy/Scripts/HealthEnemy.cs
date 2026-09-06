using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class HealthEnemy : MonoBehaviour
{
    private bool dead;

    private Vector3 ogScale;




    public int Health;
    public enemyMovement movementSc;

    public GameObject PostDeathPrefab;
    public GameObject CripplePrefab;
    public GameObject hitParticles;
    public UnityEvent onDeath;
    public bool DieOnDeath;

    public float destroyDelayDeath;



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
        if (!dead)
        {
            dead = true;
            print("DEAD BOI");

            yield return null;

            if (PostDeathPrefab != null)
            {
                GameObject newDeathPrefab = Instantiate(PostDeathPrefab, transform.position, Quaternion.identity);
            }

            if (destroyDelayDeath != 0)
            {
                movementSc.agent.enabled = false;
                yield return new WaitForSeconds(destroyDelayDeath);
            }

            onDeath.Invoke();
            if (DieOnDeath) Destroy(transform.parent.gameObject);
        }
    }

    public void SplatOnlyDontDie()
    {
        Instantiate(CripplePrefab, transform.position, Quaternion.identity);
    }

    private IEnumerator DamageVisuals(Vector3 hitLocation)
    {
        transform.localScale = transform.localScale * 1.05f;

        Instantiate (hitParticles, hitLocation, Quaternion.identity);
        yield return new WaitForSeconds(0.05f);

        transform.localScale = ogScale;
    }
}
