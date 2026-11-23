using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_StatementPanel : MonoBehaviour
{
    [SerializeField] 
    Transform listParent;
    [SerializeField] 
    S_EvidenceEntry entryPrefab;
    [SerializeField] 
    GameObject lockerView;

    [SerializeField]
    TMP_Text nameCharacter;
    [SerializeField]
    Image portrait;

    public Action onStatementPicked;
    public Character Character { get; private set; }
    public string SelectedStatement => selected != null ? selected.Evidence : null;
    public bool HasSelection => selected != null;

    S_EvidenceEntry selected;

    public void Show(S_ConnectionController choice)
    {
        foreach (Transform child in listParent) 
        { 
            Destroy(child.gameObject); 
        }
        selected = null;
        Character = choice.character;

        bool locker = choice.isLocker;
        if ((nameCharacter != null))
        {
            nameCharacter.text = locker ? "Locker" : Character.name;
        }

        if (portrait = null) 
        {
            portrait.gameObject.SetActive(!locker && Character.sprite != null);
            if (!locker) 
            {
                portrait.sprite = Character.sprite;
            }
        }
        if (lockerView != null) 
        {
            lockerView.SetActive(locker); 
        }
        listParent.gameObject.SetActive(!locker);
        if (locker) 
        {
            return;
        }

        foreach (string s in Character.statements)
        {
            S_EvidenceEntry entry = Instantiate(entryPrefab, listParent);
            entry.SetUp(s, s, OnEntryClicked);
        }
    }

    void OnEntryClicked(S_EvidenceEntry entry)
    {
        if (selected != null) 
        {
            selected.SetSelected(false); 
        }
        selected = entry;
        entry.SetSelected(true);
        onStatementPicked?.Invoke();
    }

    public void ClearSelection()
    {
        if (selected != null) 
        {
            selected.SetSelected(false); 
        }
        selected = null;
    }
}