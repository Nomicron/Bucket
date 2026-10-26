using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S_Menu : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button playButton;
    public Button quitButton;

    void Start()
    {
        // Automatically link the buttons to their functions when the game starts
        if (playButton != null)
        {
            playButton.onClick.AddListener(PlayGame);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    // Call this method when the Play button is clicked
    public void PlayGame()
    {
        SceneManager.LoadScene("Comic1");
    }

    // Call this method when the Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

        // Stops play mode inside the Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
