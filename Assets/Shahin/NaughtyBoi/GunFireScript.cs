using TMPro;
using UnityEngine;

public class GunFireScript : MonoBehaviour
{
    
    public GameObject bulletTraceObjectToSpawn;
    
    public AudioSource audioSource;

    public AudioClip[] gunfireSounds;

    public bool burstShot = false;

    //GUN MECHANICS
    [Header("for fire rate")]
    public float shotCooldownNow = 0f;
    public float ShotCooldown = 1.5f;

    [Header("for charge and burst")]
    public float chargeupTime = 1.3f;
    private float chargeNow = 0f;
    public float burstDuration = 1f;
    private bool ambattaBurst = false;
    public float postBurstClarity = 1.5f;
    //public AudioClip chargeSound;
    public AudioClip cooldownSound;
    public AudioSource chargeSoundSource;

    [Header("ammostuff")]
    [HideInInspector] public int batteryLeft = 10;
    public int batteryMax = 10;


    [Header("some unused now")]
    public float gunModelKnockback = 0.1f;
    public float aimKnockback = 0.1f;

    public float raycastRange = 2f;
    public LayerMask layerMask;

    [Header("dmg = dmg - (armor - pen)")]
    public float damage = 1;
    public float armorPiercing = 5;

    [Header("LightGradient")]
    public Gradient lightGradient;
    public Light muzzleLight;
    public float lightFadeDuration= 1.0f;
    private float lightFadeNow = 1f;

    [Header("UI")]
    public TextMeshPro burstingTextDev;
    public TextMeshPro cdTextDev;
    public TextMeshPro batteryDev;






    void Start()
    {
        batteryLeft = batteryMax;

        lightFadeNow = 1f;
        //print("Press F to fire, for now");
    }

    public void RefillBatteries()
    {
        batteryLeft = batteryMax;

    }


    void Update()
    {
        bool holdingFire = Input.GetMouseButton(0);
        bool pressFire = Input.GetMouseButtonDown(0);
        bool releaseFire = Input.GetMouseButtonUp(0);

        lightFadeNow = Mathf.Min(lightFadeNow + Time.deltaTime / Mathf.Max(0.01f, lightFadeDuration), 1f);

        shotCooldownNow -= Time.deltaTime;



        if (burstShot)
        {
            //chargeBurst
            if ((ambattaBurst == false && holdingFire && batteryLeft > 0) || chargeNow < 0)
            {
                chargeNow += Time.deltaTime;

            }

            else if (chargeNow > 0)
            {
                chargeNow -= Time.deltaTime;
            }

            //Trigger burst
            if (ambattaBurst == false && chargeNow > chargeupTime)
            {
                ambattaBurst = true;
                chargeNow = burstDuration;
            }

            if (ambattaBurst && chargeNow < 0)
            {
                if (cooldownSound != null)
                {
                    audioSource.PlayOneShot(cooldownSound);
                }

                ambattaBurst = false;
                chargeNow = -postBurstClarity;
            }

            if (burstingTextDev != null && cdTextDev != null)
            {
                burstingTextDev.text = ambattaBurst ? "AMBATTA BURST" : "Not Bursting";

                cdTextDev.text = Mathf.Round(chargeNow*100).ToString() + " / " + Mathf.Round(chargeupTime*100).ToString();

            }


            //Sounds
            float basePitch = 0.5f;

            if (!ambattaBurst && chargeNow > 0 && holdingFire)
            {
                chargeSoundSource.volume = 1f;
                chargeSoundSource.pitch = basePitch + chargeNow;
            }
            else
            {
                chargeSoundSource.volume -= Time.deltaTime * 5f;
                chargeSoundSource.pitch = Mathf.Lerp(chargeSoundSource.pitch , basePitch, Time.deltaTime * 40f);
            }


        }

        if ((holdingFire && !burstShot && batteryLeft > 0) || (burstShot && ambattaBurst))
        {
            if (shotCooldownNow <= 0f)
            {
                shotCooldownNow = ShotCooldown;
                GunFireEffects();
                GunFire();

            }
        }
        


        //Pick out a color from a gradient
        if (lightGradient != null && muzzleLight != null)
        {
            muzzleLight.color = lightGradient.Evaluate(lightFadeNow);
        }


        if (batteryDev != null)
        {
            batteryDev.text = batteryLeft.ToString() + " / " + batteryMax.ToString() + " battery"; 
        }

    }


    public void GunFireEffects()
    {
        lightFadeNow = 0f;
    }


    public void GunFire()
    {

        //Reduce Battery
        batteryLeft = Mathf.Max(batteryLeft - 1, 0);


        audioSource.pitch = Random.Range(0.9f, 1.1f);

        if (gunfireSounds.Length > 0)
        {
            audioSource.PlayOneShot(  gunfireSounds[  Random.Range(0, gunfireSounds.Length)  ]  );
        }

        else
        {
            audioSource.PlayOneShot(audioSource.clip);
        }


        RaycastHit actualHit;

        //default point of hit, if nothing hits
        Vector3 actualHitPos = transform.position + transform.forward * raycastRange;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out actualHit, raycastRange, layerMask))

        {
            Debug.Log("Hit");
            //override that to the hit point if hit
            actualHitPos = actualHit.point;

            //check if target can SUFFER!!!! WRARARARARARA
            if (actualHit.transform.gameObject.TryGetComponent<GetShotObject>(out GetShotObject getShotScript))
            {
                print("Hit A THing");
                getShotScript.GetShot(damage, armorPiercing, actualHit.transform.position);
                print("try deal damage " + damage.ToString() + " dmg, " + armorPiercing.ToString() + " penetration");
            }
            else
            {
                print("No Hit");
            }
        }


        //Trace
        GameObject traceInstatiated = Instantiate(bulletTraceObjectToSpawn, actualHitPos, transform.rotation);
        //scale trace
        traceInstatiated.transform.localScale = new Vector3(1, 1, (traceInstatiated.transform.position - transform.position).magnitude);


        


    }

}
