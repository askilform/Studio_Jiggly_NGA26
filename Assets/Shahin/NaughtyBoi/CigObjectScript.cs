using UnityEngine;

public class CigObjectScript : MonoBehaviour
{
    [Header ("Bullshit")]
    public string cigaretteBrand = "Brunstheim Browns";
    public string cigaretteSlogan = "Bring the city into your home";
    public bool toastedTobacco = true;

    [Header("Real Shit")]
    public bool isIgnited = true;
    
    public float maxLifetime = 45f;
    private float currentLifetimeAsRatio = 0.0f;
    public Gradient cigaretteGradient;

    [Header("Technical Shit")]
    public bool isHeldInHand = false;
    
    public float cigarettePhysicalLength;
    public GameObject cigaretteBurndownPart;
    public GameObject cigaretteTip;

    public Light[] cigaretteLights;


    void Start()
    {
        
    }


    void Update()
    {
        //Go from 0 to 1 over time
        currentLifetimeAsRatio += Time.deltaTime / Mathf.Max(maxLifetime, 0.1f);
        currentLifetimeAsRatio = Mathf.Clamp(currentLifetimeAsRatio, 0, 1f);



        foreach (Light ciglight in cigaretteLights) 
        {

            ciglight.enabled = isIgnited && currentLifetimeAsRatio < 1;

            ciglight.color = cigaretteGradient.Evaluate(currentLifetimeAsRatio);
        }

        //shrink middle part
        float middlePartScale = Mathf.Clamp((1 - currentLifetimeAsRatio), 0.001f, 1);

        cigaretteBurndownPart.transform.localScale = new Vector3(1, 1, middlePartScale);

        //move tip (tihi)
        cigaretteTip.transform.localPosition = new Vector3(0, 0, (1f - currentLifetimeAsRatio) * cigarettePhysicalLength);

    }
}
