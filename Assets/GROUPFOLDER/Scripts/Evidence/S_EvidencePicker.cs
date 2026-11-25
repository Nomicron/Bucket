using UnityEngine;
using UnityEngine.InputSystem;

public class S_EvidencePicker : MonoBehaviour
{
    public RayController rayController;

    private void Update()
    {
        if (PauseController.IsGamePaused) return;

        

        if (Mouse.current.leftButton.wasPressedThisFrame && rayController.TryGetHit(out RaycastHit hit) &&
            hit.collider.TryGetComponent(out Evidence evidence))
        {

            if (evidence.image == null) return;

            evidence.image.SetActive(true);
            evidence.gameObject.SetActive(false);

            if (S_ClosetManager.Instance != null)
            {
                S_ClosetManager.Instance.Unlock(evidence.evidenceName);
            }

            if (!string.IsNullOrEmpty(evidence.evidenceText) && S_Notification.Instance != null)
            {
                S_Notification.Instance.ShowNotification();
            }

        }
    }
}
