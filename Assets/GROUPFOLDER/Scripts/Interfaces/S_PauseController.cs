using UnityEngine;
using UnityEngine.Rendering;

public class PauseController : MonoBehaviour
{
    public static bool IsGamePaused { get; private set; } = false;
    //[SerializeField] static QuestUI questText;
    //[SerializeField] static S_CleanLevelUI cleanLevelText;

    private void Awake()
    {
        IsGamePaused = false;
    }
    public static void SetPause(bool pause) 
    {
        IsGamePaused = pause;
        Debug.Log($"Is paused = {pause}");
        //questText.ShowText(!pause);
        //cleanLevelText.ShowText(!pause);
    }
}
