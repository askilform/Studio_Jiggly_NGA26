using UnityEngine;

public class FuelHolderScript : MonoBehaviour
{

    public GameObject fuelObject;
    public GameObject emptyFuelPrefabProjectile;
    public GameObject depletingFuel;

    public GunFireScript gunFireScript;

    public Animator fuelAnimator;

    public bool hasDroppedRod = false;
    
    public AudioClip dropRodSound;
    public AudioClip restockRodSound;
    public AudioSource audioSource;


    void Start()
    {
        
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.R))
        {
            gunFireScript.batteryLeft = gunFireScript.batteryMax;
        }



        float batteryRatio = Mathf.Clamp((float)gunFireScript.batteryLeft / (float)gunFireScript.batteryMax, 0.001f, 1f);

        depletingFuel.transform.localScale = new Vector3(1f, 1f, batteryRatio);


        //when has rod, then run out of ammo
        if (gunFireScript.batteryLeft <= 0)
        {
            
            if (hasDroppedRod == false)
            {
                print("RUN DROP ROD SCRIPT");
                fuelAnimator.SetTrigger("dropFuelRod");
                GameObject fuelRodInstance = Instantiate(emptyFuelPrefabProjectile, fuelObject.transform.position, fuelObject.transform.rotation);
                if (fuelRodInstance.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.linearVelocity = fuelRodInstance.transform.up * 1 + fuelRodInstance.transform.forward * -1;
                    rb.angularVelocity = fuelRodInstance.transform.right * 8;
                }
                audioSource.PlayOneShot(dropRodSound);

                
            }
            hasDroppedRod = true;
        }

        //when has dropped rod, then ammo get
        else
        {
            if (hasDroppedRod == true)
            {
                print("RUN RESTOCK ROD SCRIPT");
                fuelAnimator.SetTrigger("restockFuelRod");
                audioSource.PlayOneShot(restockRodSound);
            }
            hasDroppedRod = false;
        }
    }
}
