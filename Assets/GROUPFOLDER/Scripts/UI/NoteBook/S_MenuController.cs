using UnityEngine;
using UnityEngine.InputSystem;



public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject UINotebookOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCanvas.SetActive(false);
        UINotebookOpen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame) 
        {
            ToggleNotePad();
        }
    }

    public void ToggleNotePad() 
    {
        if (menuCanvas == null) 
        {
            return;
        }
        if(!menuCanvas.activeSelf && PauseController.IsGamePaused) 
        {
            return;
        }

        bool isMenuOpen = !menuCanvas.activeSelf;
        menuCanvas.SetActive(isMenuOpen);

        bool isNotebookOpen = !UINotebookOpen.activeSelf;
        UINotebookOpen.SetActive(isNotebookOpen);
        PauseController.SetPause(menuCanvas.activeSelf);
    }
}
