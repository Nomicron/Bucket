using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ClosetUIScript : MonoBehaviour
{
    public GameObject closet;
    public GameObject UIOpenCloset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closet.SetActive(false);
        UIOpenCloset.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        OpenCloset();
    }

    void OpenCloset() 
    {
        if (Keyboard.current.cKey.wasPressedThisFrame) 
        {
            if (!PauseController.IsGamePaused) 
            {
                PauseController.SetPause(true);
                closet.SetActive(true);
                UIOpenCloset.SetActive(true);
            }
            else if (PauseController.IsGamePaused) 
            {
                PauseController.SetPause(false);
                closet.SetActive(false);
                UIOpenCloset.SetActive(false);
            }
        }
    }
}
