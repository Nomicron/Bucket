using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionManagerScript : MonoBehaviour
{
    public GameObject optionButton;
    public Transform optionParent;

    public void ShowOptions(DialogueOption[] options) 
    {
        foreach (Transform child in optionParent) 
        {
            Destroy(child.gameObject);
        }

        foreach (DialogueOption option in options) 
        {
            GameObject buttonObject = Instantiate(optionButton, optionParent);

            TextMeshProUGUI buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = option.optionText;

            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(() => SelectOption(option));
        }
    }

    void SelectOption(DialogueOption option) 
    {
        DialogueScript dialogueScript = FindFirstObjectByType<DialogueScript>();

        dialogueScript.StartNewDialogue(option.nextDialogue);
    }
}
