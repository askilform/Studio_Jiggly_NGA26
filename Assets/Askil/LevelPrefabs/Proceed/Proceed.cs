using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Proceed : MonoBehaviour
{
    TextMeshProUGUI text;
    public Color DynamicColor;

    public void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.color = DynamicColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (text.color.a < 1) 
        {
            DynamicColor.a += 0.2f * Time.deltaTime;
            text.color = DynamicColor;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject.FindFirstObjectByType<LevelMaster>().ChanceScene("Tryings_AskilEdit");
        }
    }
}
