using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Cleaning : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Look lookScript;
    [SerializeField] float cleaningRange = 2f;
    [SerializeField] float scrubThreshold = 1000f;
    [SerializeField] RayController rayController;

    public MopPainter currentMopPainter;

    Ray ray;
    private Vector3 cleaningStartPosition;
    float scrubAmount;
    //public bool cleaningMode = false;
    Stain currentStain;
    public bool hasMop = false;
    private bool mopRestarted = false;
    //private bool isClean = false; 

    // [SerializeField] GameObject madeBed;
    private void Start()
    {
        ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward
        );
    }
    void Update()
    {

        //if (CleaningModeController.InCleaningMode && currentStain != null)
        //{
        //    HandleCleaning(currentStain.stainType);
        //}
        if (!CleaningModeController.InCleaningMode)
            return;

        // The cleaning object may have been destroyed when completed.
        if (currentStain == null)
        {
            ExitCleaningMode();
            return;
        }

        CheckCleaningDistance();

        if (!CleaningModeController.InCleaningMode)
            return;

        HandleCleaning(currentStain.stainType);

    }
    private void OnInteract(InputValue input)
    {
        if (!input.isPressed)
            return;

        if (CleaningModeController.InCleaningMode)
        {
            ExitCleaningMode();
        }
        else
        {
            TryEnterCleaningMode();
        }
    }
    private void OnEscape(InputValue input)
    {
        if (!input.isPressed)
            return;

        if (CleaningModeController.InCleaningMode)
        {
            ExitCleaningMode();
        }
    }

    private void CleaningObjectiveFinished(ObjectiveMechanics mechanic)
    {
        ExitCleaningMode();
    }

    void TryEnterCleaningMode()
    {

        if (rayController.TryGetHit(out RaycastHit hit))
        {
            Stain stain = hit.collider.GetComponent<Stain>();

            if (stain != null)
            {
                currentStain = stain;
                currentMopPainter = stain.GetComponent<MopPainter>();


                // Remember where cleaning started.
                cleaningStartPosition = hit.point;

                if (currentMopPainter != null)
                {
                    if (currentMopPainter.completed)
                        return;

                    currentMopPainter.OnFinished += CleaningObjectiveFinished;
                    currentMopPainter.SpawnBucket();
                }

                scrubAmount = 0;

                CleaningModeController.SetCleaningMode(true);
            }
        }
    }

    void ExitCleaningMode()
    {
        if (currentMopPainter != null)
        {
            currentMopPainter.OnFinished -= CleaningObjectiveFinished;
            currentMopPainter.RemoveBucket();
        }
        // cleaningMode = false;
        CleaningModeController.SetCleaningMode(false);
        currentStain = null;
        currentMopPainter = null;

        lookScript.canLook = true;
        mopRestarted = false;
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
                //PauseController.SetPause(true);

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
                  //  Finish(); 
                }

                break;

            case StainType.Dirt:

                //hasMop = true;
                lookScript.canLook = false;

                if (!mopRestarted)
                {
                    mopRestarted = true;
                    StartCoroutine(RestartMop());
                }

                break;
            case StainType.Bed:

                //stain.gameObject.SetActive(false);
                //madeBed.SetActive(true);

                //ExitCleaningMode();


                break;

        }
    }
    private void CheckCleaningDistance()
    {
        Vector3 playerPosition = playerCamera.transform.position;

        // Ignore vertical height.
        playerPosition.y = cleaningStartPosition.y;

        float distance = Vector3.Distance(
            playerPosition,
            cleaningStartPosition
        );

        if (distance > cleaningRange)
        {
            ExitCleaningMode();
        }
    }
    private IEnumerator RestartMop()
    {
        // First activation.
        hasMop = true;

        // Let Unity process one frame.
        yield return null;

        // Disable mop again.
        hasMop = false;

        yield return null;

        // Enable it a second time.
        hasMop = true;
    }
}

public enum StainType
{
    Blood,
    Dirt,
    Paint,
    Bed
}