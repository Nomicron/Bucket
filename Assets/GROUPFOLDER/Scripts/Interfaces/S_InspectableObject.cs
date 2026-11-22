using UnityEngine;

public class Inspectable : MonoBehaviour
{
    [Header("Inspect Details")]
  // [SerializeField] private string inspectTitle = "Item Name";

    //[TextArea(3, 6)]
    //[SerializeField] private string inspectDescription = "Enter item description here...";

   [SerializeField] RayController rayController;

    [SerializeField] Evidence evidence;
    private void LateUpdate()
    {
        if (rayController.TryGetHit(out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Evidence"))
            {
                Evidence evidence = hit.collider.GetComponent<Evidence>();

                if (evidence != null)
                {
                    InspectUI.Instance.ShowTooltip(
                        evidence.evidenceName,
                        evidence.description
                    );
                }
                else
                {
                    InspectUI.Instance.HideTooltip();
                }
            }
            else
            {
                InspectUI.Instance.HideTooltip();
            }
        }
        else
        {
            InspectUI.Instance.HideTooltip();
        }

    }

    //private void OnMouseExit()
    //{
    //}
}
