using UnityEngine;
using Unity.AI.Navigation;
using System.Collections;

public class BreakableWallManager : MonoBehaviour
{
    public void MakeWallsBreakable()
    {
        foreach (BreakableEndgame breakables in FindObjectsByType<BreakableEndgame>(sortMode: FindObjectsSortMode.None))
        {
            breakables.OnBattleBegin();
            print("I can break now lol");
        }

        StartCoroutine(WaitAndBuildNm());

        print("Walls Now Breakable!");
    }

    IEnumerator WaitAndBuildNm()
    {
        yield return new WaitForSeconds(1);
        FindFirstObjectByType<NavMeshSurface>().BuildNavMesh();
    }
 
}
