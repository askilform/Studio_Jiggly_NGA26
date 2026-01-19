using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    public int Health;

    public void TakeDamage(int Damage)
    {
        Health -= Damage;

        if (Health <= 0) Death();
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
