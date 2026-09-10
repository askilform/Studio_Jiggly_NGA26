using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Proceed : MonoBehaviour
{
    TextMeshProUGUI text;
    bool CanProceed = false;
    public Color DynamicColor;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.color = DynamicColor;
    }

    public void StartThatShit()
    {
        print("AAAAAAAA");
        CanProceed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanProceed)
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
}
