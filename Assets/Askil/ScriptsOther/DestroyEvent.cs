using UnityEngine;

public class DestroyEvent : MonoBehaviour
{
    public void DestroyInsertedFool(GameObject fool)
    {
        Destroy(fool.gameObject);
    }
}
