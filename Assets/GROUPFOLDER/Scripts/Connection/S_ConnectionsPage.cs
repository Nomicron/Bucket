using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterConnection 
{
    public Character characterA;
    public string statementA;
    public Character characterB;
    public string statementB;
    [TextArea]
    public string infoForA;
    [TextArea] 
    public string infoForB;
}

public class S_ConnectionsPage : MonoBehaviour
{
    [SerializeField]
    GameObject popup;
    [SerializeField]
    Button confirmButton;
    [SerializeField]
    Button reopenButton;
    [SerializeField]
    S_ConnectionController[] choices;
    [SerializeField]
    S_StatementPanel panelA;
    [SerializeField]
    S_StatementPanel panelB;
    [SerializeField]
    List<CharacterConnection> connections = new List<CharacterConnection>();

    readonly List<S_ConnectionController> picked = new List<S_ConnectionController>();

    private void Awake()
    {
        foreach(var c in choices) 
        {
            c.onClicked = OnChoiceClicked;
        }
        confirmButton.onClick.AddListener(Confirm);
        reopenButton.onClick.AddListener(() => popup.SetActive(true));
        panelA.onStatementPicked = TryConnect;
        panelB.onStatementPicked = TryConnect;
    }

    private void OnEnable()
    {
        popup.SetActive(true);
        confirmButton.interactable = picked.Count == 2;
    }

    void OnChoiceClicked(S_ConnectionController c) 
    {
        if (picked.Contains(c)) 
        {
            picked.Remove(c);
            c.SetSelected(false);
        }
        else 
        {
            if(picked.Count == 2) 
            {
                picked[0].SetSelected(false);
                picked.RemoveAt(0);
            }
            picked.Add(c);
            c.SetSelected(true);
        }
        confirmButton.interactable = picked.Count == 2;
    }

    void Confirm() 
    {
        if(picked.Count != 2) 
        {
            return;
        }
        popup.SetActive(false);
        panelA.Show(picked[0]); // first pick = first page
        panelB.Show(picked[1]);
    }

    void TryConnect() 
    {
        Debug.Log($"TryConnect called. A has selection: {panelA.HasSelection}, B has selection: {panelB.HasSelection}");
        if (!panelA.HasSelection || !panelB.HasSelection) 
        {
            return;
        }
        Debug.Log($"A: [{panelA.Character}] [{panelA.SelectedStatement}] | B: [{panelB.Character}] [{panelB.SelectedStatement}]");
        foreach (var c in connections) 
        {
            bool direct = c.characterA == panelA.Character && c.statementA == panelA.SelectedStatement && c.characterB == panelB.Character && c.statementB == panelB.SelectedStatement;

            bool swapped = c.characterA == panelB.Character && c.statementA == panelB.SelectedStatement && c.characterB == panelA.Character && c.statementB == panelA.SelectedStatement;
            Debug.Log($"Entry: [{c.characterA}] [{c.statementA}] | [{c.characterB}] [{c.statementB}]");
            if (direct || swapped) 
            {
                if (!string.IsNullOrEmpty(c.infoForA) && c.characterA != null) 
                {
                    c.characterA.AddInfo(c.infoForA);
                }
                if (!string.IsNullOrEmpty(c.infoForB) && c.characterB != null) 
                {
                    c.characterB.AddInfo(c.infoForB);
                }
                break;
            }
        }

        panelA.ClearSelection();
        panelB.ClearSelection();

      
    }
}
