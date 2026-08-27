using UnityEngine;

public class GuessTheDoor : MonoBehaviour
{
    public GameObject[] sides;

    private void OnEnable()
    {
        sides[Random.Range(0,sides.Length)].SetActive(true);
    }
}
