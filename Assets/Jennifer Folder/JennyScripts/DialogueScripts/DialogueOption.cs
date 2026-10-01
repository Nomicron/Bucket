using UnityEngine;

[CreateAssetMenu(fileName = "DialogueOption", menuName = "Scriptable Objects/DialogueOption")]
public class DialogueOption : ScriptableObject
{
    public string optionText;
    public DialogueData nextDialogue;
}
