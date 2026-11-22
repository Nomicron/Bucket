using UnityEngine;

[CreateAssetMenu(fileName = "DialogueOption", menuName = "Scriptable Objects/DialogueOption")]
public class DialogueOption : ScriptableObject
{
    public string optionText;
    public DialogueData nextDialogue;

    [Header("Quest")]
    public bool acceptQuest;

    [Header("Clue")]
    public bool givesClue;
    public string clueText;

    [Header("Evidence")]
    public bool givesEvidence;
    public string evidenceId;
    [TextArea]
    public string evidenceDisplayText;
}