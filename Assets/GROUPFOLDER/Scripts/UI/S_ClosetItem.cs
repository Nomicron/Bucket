using UnityEngine;
using UnityEngine.EventSystems;
using JetBrains.Annotations;
using TMPro;

public class S_ClosetItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject description;

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
}
