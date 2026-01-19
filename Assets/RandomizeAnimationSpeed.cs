using Unity.Mathematics;
using UnityEngine;

public class RandomizeAnimationSpeed : MonoBehaviour
{
    public Animator theAnimatorInQuestion;

    private float targetSpeed = 1f;

    public float minSpeed = 0.5f;
    public float maxSpeed = 1.5f;
    
    public float howOftenToChange = 0.5f;
    public float lerpSpeed = 10f;
    private float cooldown = 0f;
    

    void Update()
    {
        if (theAnimatorInQuestion == null) return; 
        
        cooldown += Time.deltaTime;

        if (cooldown >= howOftenToChange)
        {
            cooldown = 0f;
            targetSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        }

        theAnimatorInQuestion.speed = math.lerp(theAnimatorInQuestion.speed, targetSpeed, Time.deltaTime * lerpSpeed);

    }
}
