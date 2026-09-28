using UnityEngine;
using UnityEngine.InputSystem;

public class Cleaning : MonoBehaviour
{
   // [SerializeField] Camera playerCamera;
    [SerializeField] Look lookScript;
    //[SerializeField] float cleaningRange = 2f;
    [SerializeField] float scrubThreshold = 1000f;
    [SerializeField] RayController rayController;
    [SerializeField] GameObject madeBed;

    float scrubAmount;
    public bool cleaningMode = false;
    Stain currentStain;
    public bool hasMop = false;
    public bool bedMade = false; 

    private void Start()
    {
       
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

      //Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (rayController.TryGetComponent<Stain>(out Stain stain))
        {
            if (stain != null)
            {
                currentStain = stain;

                scrubAmount = 0;

                cleaningMode = true;


                //PauseController.SetPause(true); 
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
        if (!rayController.TryGetComponent<Stain>(out Stain stain) || stain != currentStain) 
        {
            return;
        }

        switch (stainType) 
        {
            case StainType.Blood:

                lookScript.canLook = false;

                if (!Mouse.current.leftButton.isPressed)
                {
                    scrubAmount = 0;
                    return;
                }

                Vector2 mouseDelta = Mouse.current.delta.ReadValue();


                if (mouseDelta.magnitude < 1f)

                    return;


                scrubAmount += mouseDelta.magnitude;


                if (scrubAmount >= scrubThreshold)
                {
                    currentStain.Clean();
                    scrubAmount = 0;
                }
                break;

            case StainType.Dirt:

                hasMop = true;
                lookScript.canLook = false;

                break;
            case StainType.Bed:

                if (!bedMade)
                {
                    madeBed.SetActive(true);

                    stain.gameObject.SetActive(false);

                    bedMade = true;

                    ExitCleaningMode();
                }
                break;
   
        }
    }
}
public enum StainType
{
    Blood,
    Dirt,
    Paint,
    Bed
}