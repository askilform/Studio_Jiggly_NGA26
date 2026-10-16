using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class blinkingHitmarker : MonoBehaviour
{
    public Image suggamaliggamaballazahhhhhh_sack_is_hanging_low_rider;

    public void Start()
    {
        suggamaliggamaballazahhhhhh_sack_is_hanging_low_rider.enabled = false;
    }


    public void BlinkThatSHit()
    {
        BlinkIt();
    }

    public IEnumerator BlinkIt()
    {
        suggamaliggamaballazahhhhhh_sack_is_hanging_low_rider.enabled = true;
        yield return new WaitForSeconds(0.05f);
        suggamaliggamaballazahhhhhh_sack_is_hanging_low_rider.enabled = false;
    }
}
