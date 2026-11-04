using UnityEngine;

public class S_ToiletController : MonoBehaviour
{
    [SerializeField]
    Animator handleAnimator;

    [SerializeField]
    string triggerParameter = "Flush";
    [SerializeField]
    float animationDuration = 5.0f;


    bool isFlushing = false;
    float flushTimer = 0f;

    private void Start()
    {
        handleAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isFlushing) 
        {
            flushTimer -= Time.deltaTime;

            if(flushTimer <= 0f) 
            {
                isFlushing = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isFlushing) 
        {
            isFlushing = true;
            flushTimer = animationDuration;

            if(handleAnimator != null) 
            {
                handleAnimator.SetTrigger(triggerParameter);
            }
        }
     
    }

 
}
