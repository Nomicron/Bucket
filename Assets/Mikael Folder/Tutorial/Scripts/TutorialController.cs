using UnityEngine;
using UnityEngine.InputSystem;


public class TutorialController : MonoBehaviour
{
    [SerializeField]
    GameObject tutorialGUI;

    [SerializeField]
    Cleaning cleaning;

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
        if(!hasShownTutorial && cleaning != null && tutorialGUI != null && cleaning.cleaningMode) 
        {
            hasShownTutorial = true;
            PauseController.SetPause(true);
            tutorialGUI.SetActive(true);
        }
        else if (Keyboard.current.hKey.wasPressedThisFrame) 
        {
            PauseController.SetPause(true);
            tutorialGUI.SetActive(true);
        }
    }

    public void CloseTutorial() 
    {
        tutorialGUI.SetActive(false);
        PauseController.SetPause(false);
    }
}
