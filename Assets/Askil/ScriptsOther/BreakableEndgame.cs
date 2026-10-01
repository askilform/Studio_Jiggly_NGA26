using UnityEngine;
using System.Collections;

public class BreakableEndgame : MonoBehaviour
{
    [SerializeField] private GameObject ParticlePrefab;
    public void OnBattleBegin()
    {
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
        Instantiate(ParticlePrefab, breakArea, Quaternion.identity);
        Destroy(gameObject);
    }
}
