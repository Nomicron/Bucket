using UnityEngine;

public class S_UIManager : MonoBehaviour
{
    public QuestUI questUI;
    public S_CleanLevelUI cleanLevelUI;

    public static bool showUI { get; private set; } = true;
    public void ShowCleanLevelText(bool status)
    {
        cleanLevelUI.gameObject.SetActive(status);
    }

    public void ShowQuestText(bool status)
    {
        questUI.gameObject.SetActive(status);
    }
}
