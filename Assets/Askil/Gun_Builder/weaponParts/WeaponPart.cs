using UnityEngine;
using UnityEngine.UIElements;

public class WeaponPart : MonoBehaviour
{
    public string partName;
    public Image uiImage;
    public int id;

    public void OnPickup()
    {
        Destroy(gameObject);
    }
}
