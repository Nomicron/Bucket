using JetBrains.Annotations;
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
        //DialogueScript dialogueScript = FindFirstObjectByType<DialogueScript>();

        //dialogueScript.StartNewDialogue(option.nextDialogue);
        DialogueScript dialogueScript =  FindFirstObjectByType<DialogueScript>();

        if (option.acceptQuest)
        {
            QuestGiver questGiver =
                dialogueScript.CurrentCharacter.GetComponent<QuestGiver>();

            if (questGiver != null)
            {
                questGiver.AcceptQuest();
            }
            else
            {
                Debug.LogWarning(
                    "Character has no QuestGiver component!"
                );
            }
        }

        if (option.givesClue) 
        {
            dialogueScript.CurrentCharacter.AddClue(option.clueText);
        }

        if (option.givesEvidence) 
        {
            S_EvidenceNotes.Instance.AddEvidence(option.evidenceId, option.evidenceDisplayText);
        }

        dialogueScript.StartNewDialogue(option.nextDialogue);
    }
}
