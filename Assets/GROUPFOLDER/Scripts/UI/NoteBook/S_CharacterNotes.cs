using UnityEngine;
using TMPro;

public class S_CharacterNotes : MonoBehaviour
{
    public Character character;
    public TMP_Text characterDescription;
    public TMP_Text connectedText;

    void OnEnable()
    {
        Refresh();
    }

    public void Refresh() 
    {
        string clueList = "";

        foreach (string clue in character.clues) 
        {
            clueList += "- " + clue + "\n";
        }

        characterDescription.text = character.description + "\n\n" + clueList;

        if(connectedText != null) 
        {
            connectedText.text = string.Join("\n\n", character.info);
        }
    }
}
