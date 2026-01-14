using UnityEngine;
using UnityEngine.Events;

public class GetShotObject : MonoBehaviour
{
    public float health = 4;
    public float armor = 1;


    public UnityEvent onDieEvent;


    public void GetShot(float damageIn, float armorPierce)
    {
        print("Getting Hurt");
        
        float reducedArmor = Mathf.Max(armor - armorPierce, 0);
        float damageInAfterArmor = Mathf.Max(damageIn - reducedArmor, 0);


        health -= damageInAfterArmor;

        print("dmg " + damageIn.ToString() + "pen " + armorPierce.ToString() + " | reducedarmor:" + reducedArmor.ToString() + ", damageinafterarmor: " + damageInAfterArmor.ToString() + " | health: " + health.ToString());

        
        if (health <= 0)
        {
            OnDie();
        }
    }


    public void OnDie()
    {
        print("On Die Event");
        onDieEvent.Invoke();
    }

    public void JustDestroy()
    {
        print("Destroy");
        Destroy(gameObject);
    }

}
