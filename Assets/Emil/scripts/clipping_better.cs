using UnityEngine;
using UnityEngine.UIElements;

public class clipping_better : MonoBehaviour
{
    public Material monster;
    public Transform block;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        monster.SetVector("_cubeCenter", block.transform.position);
        monster.SetVector("_cubeSize", block.transform.lossyScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
