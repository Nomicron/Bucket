using UnityEngine;
using UnityEngine.InputSystem;

public class MakeBed : ObjectiveMechanics
{
    [SerializeField] private GameObject unmadeBed;
    [SerializeField] private GameObject madeBed;

    [SerializeField] private RayController rayController;

    private bool isMade = false;

    private void Update()
    {
        if (isMade)
            return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryMakeBed();
        }
    }

    private void TryMakeBed()
    {
        if (rayController.TryGetHit(out RaycastHit hit))
        {
            MakeBed bed = hit.collider.GetComponentInParent<MakeBed>();

            if (bed == this)
            {
                MakeTheBed();
            }
        }
    }

    private void MakeTheBed()
    {
        unmadeBed.SetActive(false);
        madeBed.SetActive(true);
        Finish();
        isMade = true;
    }
}
