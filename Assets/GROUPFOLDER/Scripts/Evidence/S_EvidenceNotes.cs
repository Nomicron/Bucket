using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class S_EvidenceNotes : MonoBehaviour
{
    public static S_EvidenceNotes Instance;

    [SerializeField] TMP_Text evidenceText;
    List<string> evidences = new List<string>();

    private void Awake()
    {
        Instance = this; 
    }

    public void AddEvidence(string evidence) 
    {
        if (evidences.Contains(evidence)) return;

        evidences.Add(evidence);
        
        Refresh();

        S_Notification.Instance.ShowNotification();
    }

    void Refresh() 
    {
        evidenceText.text = string.Join("\n", evidences);
    }
}
