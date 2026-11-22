using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_EvidenceEntry : MonoBehaviour
{
    [SerializeField]
    TMP_Text label;
    [SerializeField]
    Image background;
    [SerializeField]
    Button button;
    [SerializeField]
    Color normalColor = new Color(1, 1, 1, 0.05f);
    [SerializeField]
    Color selectedColor = new Color(1, 0.85f, 0.3f, 0.5f);


    public string Evidence { get; private set; }

    public void SetUp(string evidence, string displayText, Action<S_EvidenceEntry> onClicked) 
    {
        Evidence = evidence;
        label.text = displayText;
        SetSelected(false);
        button.onClick.AddListener(() => onClicked(this));
    }

    public void SetSelected(bool selected) 
    {
        background.color = selected ? selectedColor : normalColor;
    }
}
