using UnityEngine;

public class CigThrowScript : MonoBehaviour
{

    public GameObject cigaretteObject;

    public bool mustBeHeldToUse = false;

    public GameObject[] cigs;

    private int currentCig = 0;

    private float cigHeightActive = 0f;
    private float cigHeightPassive = 0f;

    void Start()
    {
        print(currentCig.ToString() + "current cig amount");
        
        cigHeightActive = cigs[currentCig-1].transform.localPosition.y;
        cigHeightPassive = cigs[0].transform.localPosition.y;

    }


    void Update()
    {
        if (!mustBeHeldToUse && Input.GetKeyDown(KeyCode.G))
        {
            ThrowCig();
        }


       Vector3 targetPos = new Vector3(cigs[currentCig].transform.localPosition.x, cigHeightActive, cigs[currentCig].transform.localPosition.z);
       cigs[currentCig].transform.localPosition = Vector3.Lerp(cigs[currentCig].transform.localPosition, targetPos, Time.deltaTime * 10);

    }



    public void ThrowCig()
    {

        if (currentCig < cigs.Length)
        {
            cigs[currentCig].gameObject.SetActive(false);
            currentCig += 1;

            GameObject cigInstance = Instantiate(cigaretteObject, transform.position, transform.rotation);

            if (cigInstance.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.linearVelocity = cigInstance.transform.forward * 10 + Vector3.up * 2;
                rb.angularVelocity = new Vector3(0, 5, 0);
            }

        }
    }

}
