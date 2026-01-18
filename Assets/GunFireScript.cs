using UnityEngine;

public class GunFireScript : MonoBehaviour
{
    
    public GameObject bulletTraceObjectToSpawn;

    public AudioSource audioSource;

    public AudioClip[] gunfireSounds;

    //GUN MECHANICS
    public float shotCooldownNow = 0f;
    public float ShotCooldown = 1.5f;
    public float gunModelKnockback = 0.1f;
    public float aimKnockback = 0.1f;

    public float raycastRange = 2f;
    public LayerMask layerMask;

    [Header("dmg = dmg - (armor - pen)")]
    public float damage = 1;
    public float armorPiercing = 5;


    void Start()
    {
        print("Press F to fire, for now");
    }


    void Update()
    {

        shotCooldownNow -= Time.deltaTime;

        if (Input.GetKey(KeyCode.F))
        {
            if (shotCooldownNow <= 0f)
            {
                shotCooldownNow = ShotCooldown;
                GunFire();
            }
        }
    }

    public void GunFire()
    {
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
        }


        //Trace
        GameObject traceInstatiated = Instantiate(bulletTraceObjectToSpawn, actualHitPos, transform.rotation);
        //scale trace
        traceInstatiated.transform.localScale = new Vector3(1, 1, (traceInstatiated.transform.position - transform.position).magnitude);


        //check if target can SUFFER!!!! WRARARARARARA
        if (actualHit.transform.gameObject.TryGetComponent<GetShotObject>(out GetShotObject getShotScript))
        {
            print("Hit A THing");
            getShotScript.GetShot(damage, armorPiercing);
            print("try deal damage " + damage.ToString() + " dmg, " + armorPiercing.ToString() + " penetration");
        }


    }

}
