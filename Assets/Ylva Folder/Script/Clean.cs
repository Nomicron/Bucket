using UnityEngine;
using UnityEngine.InputSystem;

public class Cleaning : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Look lookScript;
    [SerializeField] float cleaningRange = 2f;
    [SerializeField] float scrubThreshold = 1000f;
    Ray ray;

    float scrubAmount;
    public bool cleaningMode = false;
    Stain currentStain;
    public bool hasMop = false;


    private void Start()
    {
        ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward
        );
    }
    void Update()
    {

        if (cleaningMode && currentStain != null)
        {
            HandleCleaning(currentStain.stainType);
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

      Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, cleaningRange))
        {
            Stain stain = hit.collider.GetComponent<Stain>();

            if (stain != null)
            {
                currentStain = stain;

                scrubAmount = 0;

                cleaningMode = true;


                PauseController.SetPause(true);
            }
        }
    }

    void ExitCleaningMode()
    {
        cleaningMode = false;
        currentStain = null;

        lookScript.canLook = true;
        hasMop = false;

        PauseController.SetPause(false);
    }
    void HandleCleaning(StainType stainType)
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        switch (stainType) 
        {
            case StainType.Blood:

                lookScript.canLook = false;

                if (!Mouse.current.leftButton.isPressed)
                {
                    scrubAmount = 0;
                    return;
                }

                if (Physics.Raycast(ray, out RaycastHit hit, cleaningRange))
                {
        
                    Stain stain = hit.collider.GetComponent<Stain>();

                   
                    if (stain == currentStain)
                    {
               
                        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

                            
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
                break;

            case StainType.Dirt:

                lookScript.canLook = false;

                if (Physics.Raycast(ray, out RaycastHit hit2, cleaningRange))
                {

                    Stain stain = hit2.collider.GetComponent<Stain>();


                    if (stain == currentStain)
                    {

                        hasMop = true;
                    }
                }
                break;
   
        }
    }
}
public enum StainType
{
    Blood,
    Dirt,
    Paint
}