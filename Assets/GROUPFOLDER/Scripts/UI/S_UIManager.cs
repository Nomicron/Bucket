using UnityEngine;

public class S_UIManager : MonoBehaviour
{
    public static S_UIManager Instance { get; private set; }

    [Header("Gameplay UI")]
    [SerializeField] private GameObject questUI;
    [SerializeField] private GameObject cleanLevelUI;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ShowUI(bool show)
    {
        questUI.gameObject.SetActive(show);
        cleanLevelUI.gameObject.SetActive(show);
    }

}
