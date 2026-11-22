using UnityEngine;
using TMPro;

public class S_CharacterNotes : MonoBehaviour
{
    public Character character;
    public TMP_Text characterDescription;

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
    }
}
