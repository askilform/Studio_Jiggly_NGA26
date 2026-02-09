using System.Collections;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject BrokenPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            StartCoroutine(DestroyAndSpawn());
        }
    }

    IEnumerator DestroyAndSpawn()
    {
        Debug.Log("[] Player hit breakable");

        GameObject broken = Instantiate(BrokenPrefab, transform);
        broken.transform.SetParent(null);

        yield return null;

        Destroy(transform.root.gameObject);
    }

}
