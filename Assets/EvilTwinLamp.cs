using UnityEngine;

public class EvilTwinLamp : MonoBehaviour
{


    public float evolTwinMaxBrightness = 10f;
    public float thisMaxBrightness = 20f;

    public Light evilTwinLamp;
    public Light affectedLamp;

    public float minimumBrightness = 0f;

    void Update()
    {
        if (evilTwinLamp != null && affectedLamp != null)
        {
            affectedLamp.intensity = (1f - (evilTwinLamp.intensity / evolTwinMaxBrightness)) * thisMaxBrightness;

            if (affectedLamp.intensity < minimumBrightness) 
            { 
                affectedLamp.intensity = minimumBrightness; 
            }
        }
    }
}
