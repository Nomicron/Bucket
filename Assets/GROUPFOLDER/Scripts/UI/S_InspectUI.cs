using UnityEngine;
using UnityEngine.UI;

public class InspectUI : MonoBehaviour
{
    public static InspectUI Instance;

    [Header("UI References")]
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        HideTooltip();
    }

    public void ShowTooltip(string title, string description)
    {
        titleText.text = title;
        descriptionText.text = description;

        // Access .gameObject first to call SetActive
        titleText.gameObject.SetActive(true);
        descriptionText.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        // Hide both elements when exiting hover
        titleText.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);
    }
}
