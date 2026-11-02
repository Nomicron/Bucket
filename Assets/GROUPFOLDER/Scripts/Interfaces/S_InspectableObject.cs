using UnityEngine;

public class Inspectable : MonoBehaviour
{
    [Header("Inspect Details")]
    [SerializeField] private string inspectTitle = "Item Name";

    [TextArea(3, 6)]
    [SerializeField] private string inspectDescription = "Enter item description here...";

    private void OnMouseEnter()
    {
        if (InspectUI.Instance != null)
        {
            InspectUI.Instance.ShowTooltip(inspectTitle, inspectDescription);
        }
    }

    private void OnMouseExit()
    {
        if (InspectUI.Instance != null)
        {
            InspectUI.Instance.HideTooltip();
        }
    }
}
