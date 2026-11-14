using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverEffects : MonoBehaviour
{
    
    private Color defaultColor = Color.white;
    private TextMeshProUGUI ButtonText;
    
    Image Arrow;
    Color hoverColor1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hoverColor1 = GetComponent<Image>().color;
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
        ColorUtility.TryParseHtmlString("#670000", out Color hoverColor);

        AnimatorStateInfo currentState =
            animator.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName("Highlighted"))
        {
            ButtonText.color = hoverColor1;
            ArrowFadeIn();
            Arrow.enabled = true;
        }
        else
        {
            ButtonText.color = defaultColor;
            ArrowFadeOut();
            //Arrow.enabled = false;

        }
    }

    void ArrowFadeIn()
    {
        //Fade effect in over time for the triangle beside the hovered text

        Color color = Arrow.color;
        color.a = 1f;
        Arrow.color = color;

        
        Arrow.CrossFadeAlpha(1f, 0.2f, false);
    }

    void ArrowFadeOut()
    {
        //Fade effect out over time for the triangle beside the hovered text

        Color color = Arrow.color;
        color.a = 1f;
        Arrow.color = color;


        Arrow.CrossFadeAlpha(0f, 0.015f, false);
    }

}
