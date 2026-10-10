using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class DialogueScript : MonoBehaviour
{
    public RayController rayController;

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

    public OptionManagerScript optionManager;

    void Start()
    {
        DialogueCanvas.SetActive(false);
        
        if (optionManager != null) 
        {
            optionManager.gameObject.SetActive(false);
        }

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
        //Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (rayController.TryGetHit(out RaycastHit hit) && Keyboard.current.eKey.wasPressedThisFrame) 
        {
            currentCharacter = hit.collider.GetComponent<Character>();
            
            if (currentCharacter != null) 
            {
                //start dialogue
                startedDialogue = true;
                DialogueCanvas.SetActive(true);
                PauseController.SetPause(true);

                StartNewDialogue(currentCharacter.dialogueData[0]);
            }
        }
    }

    public void StartNewDialogue(DialogueData dialogue) 
    {
        if (optionManager != null) 
        {
            optionManager.gameObject.SetActive(false);
        }

        if (dialogue != null) 
        {
            ShowCharacterInfo(currentCharacter);

            StopAllCoroutines();

            StartCoroutine(SlowPrint(dialogue));
        }
        else 
        {
            EndDialogue();
        }
    }

    void ShowCharacterInfo(Character character) 
    {
        //Makes tmp and sprite to match character
        CharName.text = character.name;
        CharImage.sprite = character.sprite;
    }

    IEnumerator SlowPrint(DialogueData dialogue)
    {
        yield return new WaitUntil(() => !Mouse.current.leftButton.isPressed);

        //Prints every line in the scriptable dialogue data object attatched to the character
        foreach (string line in dialogue.lines) 
        {
            //Clears text after every line
            CharDialogue.text = "";
            bool skippedLine = false;

            //Prints letter by letter
            foreach (char c in line) 
            {
                //instantly finishes line if you press left click
                if (Mouse.current.leftButton.wasPressedThisFrame) 
                {
                    CharDialogue.text = line;
                    skippedLine = true;
                    break;
                }

                CharDialogue.text += c;

                float timer = 0f;

                while (timer < timeBetweenLetters) 
                {
                    if (Mouse.current.leftButton.wasPressedThisFrame) 
                    {
                        CharDialogue.text = line;
                        skippedLine = true;
                        break;
                    }

                    timer += Time.deltaTime;
                    yield return null;
                }

                if (skippedLine)
                {
                    break;
                }
            }

            CharDialogue.text = line;

            //checks if the mouse is pressed first to ensure no line is skipped
            yield return new WaitUntil(() => !Mouse.current.leftButton.isPressed);

            //then checks to make sure its pressed this frame
            yield return new WaitUntil(() => Mouse.current.leftButton.wasPressedThisFrame);

            yield return null;
        }

        if (optionManager != null) 
        {
            optionManager.gameObject.SetActive(true);

            optionManager.ShowOptions(dialogue.options);
        }

    }

    public void EndDialogue() 
    {
        //end the dialogue
        startedDialogue = false;
        DialogueCanvas.SetActive(false);

        if (optionManager != null)
        {
            optionManager.gameObject.SetActive(false);
        }

        PauseController.SetPause(false);

        CharDialogue.text = "";
    }
}
