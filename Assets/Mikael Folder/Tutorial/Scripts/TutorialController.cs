using UnityEngine;
using UnityEngine.InputSystem;


public class TutorialController : MonoBehaviour
{
    [SerializeField]
    GameObject tutorialGUI;

    bool hasShownTutorial = false;
    bool isOpen = false;
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
            OpenTutorial();
        }
        if (Keyboard.current.hKey.wasPressedThisFrame) 
        {
            if (!isOpen) 
            {
                hasShownTutorial = true;
                OpenTutorial();
            }
            else 
            {
                CloseTutorial();
            }
        }
    }

    public void OpenTutorial() 
    {
        isOpen = true;

        tutorialGUI.SetActive(true);

        Time.timeScale = 0f;

        PauseController.SetPause(true);
    }

    public void CloseTutorial() 
    {
        isOpen = false;

        tutorialGUI.SetActive(false);

        Time.timeScale = 1f;

        PauseController.SetPause(false);
    }
}
