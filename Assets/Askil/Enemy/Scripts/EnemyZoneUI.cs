using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyZoneUI : MonoBehaviour
{
    private float lerpValue;
    private TextMeshProUGUI txt;

    private void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
        txt.color = new Color(0, 0, 0, 0);
    }

    public IEnumerator OnZoneChange(string message, float duration)
    {
        txt.text = message;
        txt.color = Color.white;

        yield return new WaitForSeconds(duration);

        txt.color = new Color (0, 0, 0, 0);
    }

    private void Update()
    {
        
    }
}
