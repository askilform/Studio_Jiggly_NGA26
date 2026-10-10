
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float pushForce = 3f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        // Ignore objects without a movable Rigidbody
        if (rb == null || rb.isKinematic)
            return;

        // Only push horizontally
        Vector3 pushDirection = new Vector3(
            hit.moveDirection.x,
            0f,
            hit.moveDirection.z
        ).normalized;

        // Apply force at the point of contact
        rb.AddForceAtPosition(
            pushDirection * pushForce,
            hit.point,
            ForceMode.Impulse
        );
    }
}
