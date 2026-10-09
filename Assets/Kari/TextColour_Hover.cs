using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextColour_Hover : MonoBehaviour
{
    
    private Color defaultColor = Color.white;
    private TextMeshProUGUI ButtonText;
    
    Image Arrow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ButtonText = GetComponentInChildren<TextMeshProUGUI>();
        Arrow = transform.Find("Arrow").GetComponent<Image>();

        Arrow.enabled = false;
      
     
    }


    // Update is called once per frame
    void Update()
    {
       CheckAnimationState();
    }

    void CheckAnimationState()
    {
        Animator animator = GetComponent<Animator>();
        ColorUtility.TryParseHtmlString("#76789C", out Color hoverColor);

        AnimatorStateInfo currentState =
            animator.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName("Highlighted"))
        {
            ButtonText.color = hoverColor;
            Arrow.enabled = true;
        }
        else
        {
            ButtonText.color = defaultColor;
            Arrow.enabled = false;

        }
    }

}
