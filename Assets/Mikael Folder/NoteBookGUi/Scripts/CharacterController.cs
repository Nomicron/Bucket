using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    private GameObject active_page;
    private int page;
    public int pageNumber 
    {
        get { return page; }
        set
        {
            if(pages == null || pages.Length == 0) 
            {
                return;
            }
            page = value;
            if(page < 0) 
            {
                page = pages.Length - 1;
            }
            else if(page > pages.Length - 1) 
            {
                page = 0;
            }
            SetActivePage();
        }
    }

    public GameObject[] pages;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(pages.Length == 0) 
        {
            return;
        }
        active_page = pages[page];
        SetActivePage();
    }

    // Update is called once per frame

    public void Next() 
    {
        pageNumber++;
    }
    public void Prev() 
    {
        pageNumber--;
    }

    private void SetActivePage() 
    {
        if(active_page != null)
        {
            active_page.SetActive(false);
        }
        active_page = pages[page];
        active_page.SetActive(true);
    }


}
