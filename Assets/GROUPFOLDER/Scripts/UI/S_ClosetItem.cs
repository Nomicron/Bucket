using UnityEngine;
using UnityEngine.EventSystems;
using JetBrains.Annotations;
using TMPro;
using System;

public class S_ClosetItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject description;
    public string evidenceId;
    [SerializeField]
    GameObject selectedFrame;

    public Action<S_ClosetItem> onClicked;

    void OnEnable()
    {
        description.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        description.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) 
    {
        Debug.Log($"Clicked {evidenceId}, onClicked is {(onClicked == null ? "NULL" : "set")}");
        onClicked?.Invoke(this); 
    }

    public void SetSelected(bool selected) 
    {
        if(selectedFrame != null) 
        {
            selectedFrame.SetActive(selected);
        }
    }
}
