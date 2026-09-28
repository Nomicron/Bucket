using TMPro;
using UnityEngine;

public class cleaningUI : MonoBehaviour
{

    public TextMeshProUGUI cleaningText;
    public Cleaning cleaning;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cleaning.cleaningMode && cleaning.currentMopPainter != null)
        {
            float percentage = cleaning.currentMopPainter.GetCleanPercentage() * 100f;

            cleaningText.text = $"Clean: {percentage:F0}%";

            if (cleaning.currentMopPainter.completed)
            {
                cleaningText.text = "Floor is clean!";
            }
        }
        else
        {
            cleaningText.text = "";
        }

    }
}
