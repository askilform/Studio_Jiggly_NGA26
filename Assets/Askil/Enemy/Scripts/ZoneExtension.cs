using UnityEngine;

public class ZoneExtension : MonoBehaviour
{
    private EnemyZone enemyZone;

    public TypeOfZone typeOfZone;
    public enum TypeOfZone {None, DangerzoneExtension, SafezoneExtension}

    private void Start()
    {
        enemyZone = FindFirstObjectByType<EnemyZone>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (typeOfZone)
            {
                case TypeOfZone.None:
                    enemyZone.currentZoneExtention = EnemyZone.CurrentZoneExtention.None;
                    break;
                case TypeOfZone.DangerzoneExtension:
                    enemyZone.currentZoneExtention = EnemyZone.CurrentZoneExtention.DangerZone;
                    break;
                case TypeOfZone.SafezoneExtension:
                    enemyZone.currentZoneExtention = EnemyZone.CurrentZoneExtention.SafeZone;
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyZone.currentZoneExtention = EnemyZone.CurrentZoneExtention.None;
        }
    }
}

