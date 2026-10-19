using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SlotMachinController : MonoBehaviour
{
    [SerializeField]
    Animator handleAnimator;
    [SerializeField]
    Transform[] wheels;

    [SerializeField]
    string triggerParameter = "Pull";
    [SerializeField]
    float spinSpeed = 600f;
    [SerializeField]
    float spinDuration = 2f;
    [SerializeField]
    float stopDelay = 0.8f;
    [SerializeField]
    int symbols = 6;

    bool isSpinning = false;


    private void OnMouseDown()
    {
        if (isSpinning)
        {
            return;
        }
        if (!isSpinning)
        {
            isSpinning = true;

            if (handleAnimator != null)
            {
                handleAnimator.SetTrigger(triggerParameter);
            }
            else
            {
                StartCoroutine(SpinWheels());
            }

        }
    }

    // Called by your Animation Event on the last frame
    public void OnHandleAnimationFinished()
    {
        StartCoroutine(SpinWheels());
    }

    private IEnumerator SpinWheels()
    {
        float stepAngle = 360f / symbols;


        for (int i = 0; i < wheels.Length; i++)
        {

            float totalSpinTime = spinDuration + (i * stopDelay);
            float timer = 0f;

            while (timer < totalSpinTime)
            {
                timer += Time.deltaTime;


                for (int j = i; j < wheels.Length; j++)
                {

                    wheels[j].Rotate(Vector3.forward * spinSpeed * Time.deltaTime, Space.Self);
                }

                yield return null;
            }


            int randomSymbol = Random.Range(0, symbols);
            float targetAngle = randomSymbol * stepAngle;


            wheels[i].localRotation = Quaternion.Euler(0, 0, targetAngle);
        }

        isSpinning = false;
    }
}