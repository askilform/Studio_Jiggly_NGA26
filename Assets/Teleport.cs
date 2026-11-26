using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 teleportToLocation;
    public void TeleportTo()
    {
        transform.position = teleportToLocation;
    }
}
