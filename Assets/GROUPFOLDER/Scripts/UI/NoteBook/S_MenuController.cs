using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject UINotebookOpen;

    [SerializeField]
    GameObject tutorialGUI;

    bool notebookOpen = false;
    bool tutorialOpen = false;
    bool hasShownTutorial = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        menuCanvas.SetActive(false);
        UINotebookOpen.SetActive(false);
        tutorialGUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        EscapeKey();
        ActivateTutorial();
    }

    void EscapeKey()
    {
        if (!Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            return;
        }

        if (tutorialOpen) 
        {
            CloseTutorial();
        }
        else if (notebookOpen) 
        {
            CloseNoteBook();
        }
        else 
        {
            OpenNoteBook();
        }
    }

    void ActivateTutorial()
    {
        if (notebookOpen) 
        {
            return;
        }

        if (Keyboard.current.hKey.wasPressedThisFrame || (!hasShownTutorial && tutorialGUI != null && CleaningModeController.InCleaningMode))
        {
            if (!tutorialOpen)
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

    void OpenNoteBook()
    {
        notebookOpen = true;

        menuCanvas.SetActive(true);
        UINotebookOpen.SetActive(true);

        PauseController.SetPause(true);
        UpdatePause();
    }

    void CloseNoteBook()
    {
        notebookOpen = false;

        menuCanvas.SetActive(false);
        UINotebookOpen.SetActive(false);

        PauseController.SetPause(false);
        UpdatePause();
    }

    void OpenTutorial()
    {
        tutorialOpen = true;

        tutorialGUI.SetActive(true);

        PauseController.SetPause(true);
        UpdatePause();
    }

    void CloseTutorial()
    {
        tutorialOpen = false;

        tutorialGUI.SetActive(false);

        PauseController.SetPause(false);
        UpdatePause();
    }

    void UpdatePause()
    {
        if (notebookOpen == true || tutorialOpen == true) 
        {
            PauseController.SetPause(true);
        }
        else 
        {
            PauseController.SetPause(false);
        }
    }
}
