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
    public void penis()
    {
        StartCoroutine (DestroyAndSpawn());
    }
    public IEnumerator DestroyAndSpawn()
    {
        Debug.Log("[] Player hit breakable");

        Transform source = transform.root;

        GameObject broken = Instantiate(
            BrokenPrefab,
            source.position,
            source.rotation
        );
        broken.transform.localScale = source.lossyScale;

        broken.transform.SetParent(null);

        yield return null;

        Destroy(transform.parent.gameObject);
    }

}
