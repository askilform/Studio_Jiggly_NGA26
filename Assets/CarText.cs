using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class CarText : MonoBehaviour
{
    TextMeshProUGUI text;
    public bool CanProceed = false;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeText(string newText)
    {
        text.text = newText;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanProceed) { GameObject.FindFirstObjectByType<LevelMaster>().ChanceScene("Tryings_AskilEdit"); }
    }
}
