using UnityEngine;
using UnityEngine.UIElements;

public class WeaponPart : MonoBehaviour
{
    public string partName;
    public Image uiImage;
    public string id;

    public void OnPickup()
    {
        Destroy(gameObject);
    }
}
