using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EvidenceConnection 
{
    public string evidenceA;
    public string evidenceB;
    public Character character; //Who gets new info
    public string newClue; //What gets added
}

public class S_EvidenceNotes : MonoBehaviour
{
    public static S_EvidenceNotes Instance;

    [SerializeField]
    Transform listParent;
    [SerializeField]
    S_EvidenceEntry entryPrefab;
    [SerializeField]
    List<EvidenceConnection> connections = new List<EvidenceConnection>();

    List<string> evidences = new List<string>();
    S_EvidenceEntry selected;

    void Awake()
    {
        Instance = this;
    }

    public void AddEvidence(string id, string displayText)
    {
        if (evidences.Contains(id)) 
        {
            return;
        }

        evidences.Add(id);

        S_EvidenceEntry entry = Instantiate(entryPrefab, listParent);
        entry.SetUp(id, displayText, OnEntryClicked);

        S_Notification.Instance.ShowNotification();
    }

    void OnEntryClicked(S_EvidenceEntry entry) 
    {
        // First click: select evidence
        if(selected == null) 
        {
            selected = entry;
            selected.SetSelected(true);
            return;
        }

        // Second click: deselect evidence
        if(selected == entry) 
        {
            selected.SetSelected(false);
            selected = null;
            return;
        }

        TryConnect(selected.Evidence, entry.Evidence);
        selected.SetSelected(false);
        selected = null;
    }

    void TryConnect(string a, string b) 
    {
        foreach(EvidenceConnection c in connections) 
        {
            bool match = (c.evidenceA == a && c.evidenceB == b) || (c.evidenceA == b && c.evidenceB == a);

            if (match) 
            {
                // Add Clue
                c.character.AddClue(c.newClue);
                return;
            }
        }
    }

    //void Refresh() 
    //{
    //    evidenceText.text = string.Join("\n", evidences);
    //}
}
