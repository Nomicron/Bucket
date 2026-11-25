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
    GameObject closetView;

    [SerializeField]
    TMP_Text nameCharacter;
    [SerializeField]
    Image portrait;

    public Action onStatementPicked;
    public Character Character { get; private set; }
    public string SelectedStatement => selected != null ? selected.Evidence : selectedItem != null ? selectedItem.evidenceId : null;
    public bool HasSelection => selected != null || selectedItem != null;

    S_EvidenceEntry selected;

    S_ClosetItem selectedItem;

    public bool IsCloset { get; private set; }

 

    public void Show(S_ConnectionController choice)
    {
        foreach (Transform child in listParent) 
        { 
            Destroy(child.gameObject); 
        }
        selected = null;
        selectedItem = null;
        Character = choice.character;

        IsCloset = choice.isLocker;
        if ((nameCharacter != null))
        {
            nameCharacter.text = IsCloset ? "Closet" : Character.name;
        }

        if (portrait != null) 
        {
            portrait.gameObject.SetActive(!IsCloset && Character.sprite != null);
            if (!IsCloset) 
            {
                portrait.sprite = Character.sprite;
            }
        }
        if (closetView != null) 
        {
            closetView.SetActive(IsCloset); 
        }

        listParent.gameObject.SetActive(!IsCloset);

        if (IsCloset) 
        {
            foreach(S_ClosetItem item in closetView.GetComponentsInChildren<S_ClosetItem>(true)) 
            {
                item.onClicked = OnClosetItemClicked;
                item.SetSelected(false);
                bool unlocked = S_ClosetManager.Instance != null && S_ClosetManager.Instance.IsUnlocked(item.evidenceId);
                item.gameObject.SetActive(unlocked);
                Debug.Log($"Closet panel assigning to {closetView.GetComponentsInChildren<S_ClosetItem>(true).Length} items");
            }
            return;
        }

        foreach (string s in Character.statements)
        {
            S_EvidenceEntry entry = Instantiate(entryPrefab, listParent);
            entry.SetUp(s, s, OnEntryClicked);
        }
    }

    void OnClosetItemClicked(S_ClosetItem item) 
    {
        if (selectedItem != null) 
        {
            selectedItem.SetSelected(false);
        }
        selectedItem = (selectedItem == item) ? null : item;

        if(selectedItem != null) 
        {
            selectedItem.SetSelected(true);
        }
        onStatementPicked?.Invoke();
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
        if(selectedItem != null) 
        {
            selectedItem.SetSelected(false);
        }
        selected = null;
        selectedItem = null;
    }
}