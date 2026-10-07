using UnityEngine;

public class anim_kys : MonoBehaviour
{

    public Animator disable_this;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disableTheThing()
    {
        if (disable_this != null)
        {
            disable_this.enabled = false;
        }
    }
}
