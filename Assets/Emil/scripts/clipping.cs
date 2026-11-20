
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CubeShaderController : MonoBehaviour
{
    public Renderer targetMesh;

    private MaterialPropertyBlock propertyBlock;
    private BoxCollider cubeCollider;

    void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
        cubeCollider = GetComponent<BoxCollider>();
    }

    void LateUpdate()
    {
        if (targetMesh == null) return;

        Bounds bounds = cubeCollider.bounds;

        targetMesh.GetPropertyBlock(propertyBlock);

        propertyBlock.SetVector(
            "_cubeCenter",
            bounds.center
        );

        propertyBlock.SetVector(
            "_cubeSize",
            bounds.size
        );

        targetMesh.SetPropertyBlock(propertyBlock);
    }
}