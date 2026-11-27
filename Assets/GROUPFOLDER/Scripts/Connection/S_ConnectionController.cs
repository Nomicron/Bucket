using System;
using UnityEngine;
using UnityEngine.UI;

public class S_ConnectionController : MonoBehaviour
{
    [SerializeField]
    Image selectedFrame;
    [SerializeField]
    Button button;

    public Character character;
    public bool isLocker;

    public bool IsSelected { get; private set; }
    public Action<S_ConnectionController> onClicked;

    private void Awake()
    {
        button.onClick.AddListener(() => onClicked?.Invoke(this));
        SetSelected(false);
    }


    public void SetSelected(bool selected)
    {
        IsSelected = selected;
        selectedFrame.gameObject.SetActive(selected);
    }
}
