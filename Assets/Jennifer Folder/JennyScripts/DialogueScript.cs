using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject DialogueCanvas;
    public Camera playerCamera;
    bool startedDialogue = false;
    Character currentCharacter;

    public TextMeshProUGUI characterName;

    void Start()
    {
        DialogueCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastCheck();

        if (startedDialogue) 
        {
            DialogueCheck();
        } 
    }

    void DialogueCheck() 
    {
        if (DialogueCanvas == null) 
        { 
            return;
        }
        if (!DialogueCanvas.activeSelf && PauseController.IsGamePaused)
        {
            return;
        }

        bool isDialogueOpen = !DialogueCanvas.activeSelf;
        DialogueCanvas.SetActive(isDialogueOpen);
        PauseController.SetPause(DialogueCanvas.activeSelf);
    }

    void RaycastCheck() 
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 5) && Keyboard.current.eKey.wasPressedThisFrame) 
        {
            startedDialogue = true;
            currentCharacter = hit.collider.GetComponent<Character>();
            
            if (currentCharacter != null) 
            {
                ShowCharacterInfo(currentCharacter);    
            }
        }
        else
        {
            startedDialogue = false;
        }
    }

    void ShowCharacterInfo(Character character) 
    {
        //Change characterName TextMeshPro to the string character.Name
        characterName.text = character.name;
    }
}
