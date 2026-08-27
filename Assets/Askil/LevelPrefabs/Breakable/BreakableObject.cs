using System.Collections;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject BrokenPrefab;
    public GameObject PrefabRoot;

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
            PrefabRoot.transform.position,
            PrefabRoot.transform.rotation
        );
        broken.transform.localScale = PrefabRoot.transform.lossyScale;

        broken.transform.SetParent(null);

        yield return null;

        Destroy(transform.parent.gameObject);
    }

}
