using UnityEngine;

public class PlaneClipper : MonoBehaviour
{
    [SerializeField] private Transform clipPlaneObject;
    [SerializeField] private Renderer targetRenderer;

    private static readonly int PlanePosId = Shader.PropertyToID("_PlanePos");
    private static readonly int PlaneNormalId = Shader.PropertyToID("_PlaneNormal");

    private void Update()
    {
        if (!clipPlaneObject || !targetRenderer) return;

        // Use TransformDirection so scaling/rotation on the plane object works cleanly
        Vector3 normal = clipPlaneObject.TransformDirection(Vector3.up).normalized;
        Vector3 pos = clipPlaneObject.position;

        targetRenderer.material.SetVector(PlanePosId, pos);
        targetRenderer.material.SetVector(PlaneNormalId, normal);
    }
}
