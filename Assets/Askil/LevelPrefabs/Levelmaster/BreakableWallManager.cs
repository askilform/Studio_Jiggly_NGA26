using UnityEngine;
using Unity.AI.Navigation;

public class BreakableWallManager : MonoBehaviour
{
    public void MakeWallsBreakable()
    {
        foreach (BreakableEndgame breakables in FindObjectsByType<BreakableEndgame>(sortMode: FindObjectsSortMode.None))
        {
            breakables.OnBattleBegin();
            print("I can break now lol");
        }

        FindFirstObjectByType<NavMeshSurface>().BuildNavMesh();

        print("Walls Now Breakable!");
    }
}
