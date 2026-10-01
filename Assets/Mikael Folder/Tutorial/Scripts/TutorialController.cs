using UnityEngine;
using UnityEngine.InputSystem;


public class TutorialController : MonoBehaviour
{
    [SerializeField]
    GameObject tutorialGUI;

    bool hasShownTutorial = false;
    void Start()
    {
        tutorialGUI.SetActive(false);
    }
    void Update()
    {
        ActivateTutorial();
    }

    public void ActivateTutorial() 
    {
        if(!hasShownTutorial && tutorialGUI != null && CleaningModeController.InCleaningMode) 
        {
            hasShownTutorial = true;
            PauseController.SetPause(true);
            tutorialGUI.SetActive(true);
        }
        else if (Keyboard.current.hKey.wasPressedThisFrame) 
        {
            tutorialGUI.SetActive(true);
        }
    }

    public void CloseTutorial() 
    {
        tutorialGUI.SetActive(false);
    }
}
