using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class DialogueScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject DialogueCanvas;
    public Camera playerCamera;
    bool startedDialogue = false;
    Character currentCharacter;

    public TextMeshProUGUI CharName;
    public Image CharImage;
    public TextMeshProUGUI CharDialogue;

    public float timeBetweenLetters;
    public float timeBetweenLines;

    void Start()
    {
        DialogueCanvas.SetActive(false);
        PauseController.SetPause(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (!startedDialogue) 
        {
            RaycastCheck();
        }
    }

    //checks if player sees a character and presses e on the keyboard to indicate youve started dialogue with them
    void RaycastCheck() 
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 5) && Keyboard.current.eKey.wasPressedThisFrame) 
        {
            currentCharacter = hit.collider.GetComponent<Character>();
            
            if (currentCharacter != null) 
            {
                //start dialogue
                startedDialogue = true;
                DialogueCanvas.SetActive(true);
                PauseController.SetPause(true);

                ShowCharacterInfo(currentCharacter);    
            }
        }
    }

    void ShowCharacterInfo(Character character) 
    {
        //Makes tmp and sprite to match character
        CharName.text = character.name;
        CharImage.sprite = character.sprite;

        StartCoroutine(SlowPrint(character));
    }

    IEnumerator SlowPrint(Character character)
    { 
        //Needs to be changed to work with days when that is implemented.
        //for example (if there is a daymanager implemented: Dialogue dialogue = character.DialogueData[DayManager.currentDay - 1];
        DialogueData dialogue = character.dialogueData[0];

        //Prints every line in the scriptable dialogue data object attatched to the character
        foreach (string line in dialogue.lines) 
        {
            //Clears text after every line
            CharDialogue.text = "";

            //Prints letter by letter
            foreach (char c in line) 
            {
                //instantly finishes line if you press left click
                if (Mouse.current.leftButton.wasPressedThisFrame) 
                {
                    CharDialogue.text = line;
                    break;
                }

                CharDialogue.text += c;

                //adds delay per letter
                yield return new WaitForSeconds(timeBetweenLetters);
            }

            CharDialogue.text = line;

            //checks if the mouse is pressed first to ensure no line is skipped
            yield return new WaitUntil(() => Mouse.current.leftButton.isPressed);

            //then checks to make sure its pressed this frame
            yield return new WaitUntil(() => Mouse.current.leftButton.wasPressedThisFrame);
        }

        //end the dialogue
        startedDialogue = false;
        DialogueCanvas.SetActive(false);
        PauseController.SetPause(false);
        CharDialogue.text = "";
    }
}
