using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] pages;
    public int startTab = 0;
    public bool resetOnEnable = false;

    bool started = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActivateTab(startTab);
        started = true;
    }

    // Update is called once per frame
  public void ActivateTab(int tabNumber) 
    {
        for(int i = 0;  i < tabImages.Length; i++) 
        {
            bool selected = i == tabNumber;
            pages[i].SetActive(selected);
            tabImages[i].color = selected ? Color.white : Color.grey;
        }
    }
}
