using UnityEngine;

public class SizeIncreaseOnHover : MonoBehaviour
{ 
    public void OnHover(GameObject ObjectHovered)
    {
        ObjectHovered.transform.localScale = new Vector3(
            ObjectHovered.transform.localScale.x,
            ObjectHovered.transform.localScale.y,
            ObjectHovered.transform.localScale.z) * 1.2f;
    }
}
