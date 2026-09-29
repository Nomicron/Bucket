using TMPro;
using UnityEngine;

public class cleaningUI : MonoBehaviour
{

    public TextMeshProUGUI cleaningText;
    public TextMeshProUGUI mopText;
    public Cleaning cleaning;
    public mopScript mop;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cleaning.cleaningMode && cleaning.currentMopPainter != null)
        {
            float cleaningPercentage = cleaning.currentMopPainter.GetCleanPercentage() * 100f;

            cleaningText.text = $"Clean: {cleaningPercentage:F0}%";

            if (cleaning.currentMopPainter.completed)
            {
                cleaningText.text = "Floor is clean!";
            }

            if (mop.isWet)
            {
                float wetPercentage = mop.wetnessLevel * 100f;

                mopText.text = $"Mop wetness: {wetPercentage:F0}%";
            }
            else
                mopText.text = "Mop is dry!";
        }
        else
        {
            cleaningText.text = "";
            mopText.text = "";
        }

        

    }
}
