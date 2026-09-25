using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TeleportEvent : UnityEvent <GameObject, Vector3>
{
    public void Teleport(GameObject objectToTeleport, Vector3 teleportTo)
    {
        objectToTeleport.transform.position = teleportTo;
    }
}
