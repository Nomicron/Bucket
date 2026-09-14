using UnityEngine;
using UnityEngine.InputSystem;

public class Cleaning : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Look lookScript;
    [SerializeField] float cleaningRange = 2f;
    [SerializeField] float scrubThreshold = 1000f;

    float scrubAmount;
    bool cleaningMode = false;
    Stain currentStain;

    void Update()
    {

        if (cleaningMode)
        {
            HandleCleaning();
        }

    }
    private void OnInteract(InputValue input)
    {
        if (!input.isPressed)
            return;

        if (cleaningMode)
        {
            ExitCleaningMode();
        }
        else
        {
            TryEnterCleaningMode();
        }
    }

    void TryEnterCleaningMode()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, cleaningRange))
        {
            Stain stain = hit.collider.GetComponent<Stain>();

            if (stain != null)
            {
                currentStain = stain;

                scrubAmount = 0;

                cleaningMode = true;

                lookScript.canLook = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    void ExitCleaningMode()
    {
        cleaningMode = false;
        currentStain = null;

        lookScript.canLook = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void HandleCleaning()
    {

        // Must hold left mouse button to scrub
        if (!Mouse.current.leftButton.isPressed)
        {
            scrubAmount = 0;
            return;
        }

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, cleaningRange))
        {
            Stain stain = hit.collider.GetComponent<Stain>();

            // Only clean the stain we entered cleaning mode on
            if (stain == currentStain)
            {
                Vector2 mouseDelta = Mouse.current.delta.ReadValue();

                // Ignore tiny mouse movement / input noise
                if (mouseDelta.magnitude < 1f)
                    return;

                scrubAmount += mouseDelta.magnitude;

                if (scrubAmount >= scrubThreshold)
                {
                    currentStain.Clean();
                    scrubAmount = 0;
                }


            }
        }

    }
}
public enum StainType
{
    Blood,
    Dirt,
    Paint
}