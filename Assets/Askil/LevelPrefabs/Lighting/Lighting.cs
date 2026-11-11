using FMODUnity;
using System.Collections;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public Material ThunderMat;
    public StudioEventEmitter sfx;

    public Vector2 LightningIntervals;
    void Start()
    {
        ThunderShot();
    }

    public void ThunderShot()
    {
        StartCoroutine(ThunderShotroutine());
    }

    IEnumerator ThunderShotroutine()
    {
        yield return new WaitForSeconds(Random.Range (LightningIntervals.x, LightningIntervals.y));

        Material oGskybox = RenderSettings.skybox;
        RenderSettings.skybox = ThunderMat;
        if (sfx != null) sfx.Play();

        yield return new WaitForSeconds(0.1f);

        RenderSettings.skybox = oGskybox;

        yield return new WaitForSeconds(0.1f);

        RenderSettings.skybox = ThunderMat;

        yield return new WaitForSeconds(0.1f);

        RenderSettings.skybox = oGskybox;

        ThunderShot();
    }

}
