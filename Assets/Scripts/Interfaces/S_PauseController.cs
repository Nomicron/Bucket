using UnityEngine;
using UnityEngine.Rendering;

public class PauseController : MonoBehaviour
{
    public static bool IsGamePaused { get; private set; } = false;

    private void Awake()
    {
        IsGamePaused = false;
    }
    public static void SetPause(bool pause) 
    {
        IsGamePaused = pause;
        Debug.Log($"Is paused = {pause}");
    }
}
