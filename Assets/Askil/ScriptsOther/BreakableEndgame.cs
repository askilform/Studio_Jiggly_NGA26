using UnityEngine;
using System.Collections;

public class BreakableEndgame : MonoBehaviour
{
    [SerializeField] private GameObject ParticlePrefab;

    bool battleBegun;
    public void OnBattleBegin()
    {
        battleBegun = true;

        transform.gameObject.layer = 3;

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.layer = 3;
        }

        // gameObject.AddComponent(typeof(BoxCollider));

        print ("Has Prefab =" + ParticlePrefab != null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            OnEnemyOverlap(transform.position);
        }
    }

    public void OnEnemyOverlap(Vector3 breakArea)
    {
        if (battleBegun)
        {
            Instantiate(ParticlePrefab, breakArea, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
