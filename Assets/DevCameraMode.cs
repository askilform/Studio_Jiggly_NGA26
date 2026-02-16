using UnityEngine;

public class DevCameraMode : MonoBehaviour
{


    public bool usePixelCanvasInsteadOfCamera;

    [Header ("don't touch unless needed")]
    public Camera ccccamera;
    public Canvas cccccanvas;


    void Start()
    {
        if (usePixelCanvasInsteadOfCamera == false)
        {
            cccccanvas.enabled = false;
            ccccamera.targetTexture = null;
        }
    }
}
