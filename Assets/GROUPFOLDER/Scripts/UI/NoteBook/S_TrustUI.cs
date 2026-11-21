using UnityEngine;
using UnityEngine.UI;

public class NPCTrustUI : MonoBehaviour
{
    [SerializeField]
    Character targetNPC;

    [SerializeField]
    Image fillImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(targetNPC != null) 
        {
            targetNPC.onTrustChanged.AddListener(UpdateSlider);

            UpdateSlider(targetNPC.CurrentTrust, targetNPC.MaxTrust);
        }
        else 
        {
            Debug.LogWarning("No target NPC assigned to this UI bar");
        }
    }

    // Update is called once per frame
    void UpdateSlider(int current, int max)
    {
        if(fillImage != null) 
        {
            fillImage.fillAmount = (float)current / max;
        }
    }

    private void OnDestroy()
    {
        // Clean up the listener when the Ui is destroyed
        if (targetNPC != null)
        {
            targetNPC.onTrustChanged.RemoveListener(UpdateSlider);
        }
    }
}
