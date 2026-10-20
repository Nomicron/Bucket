using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class S_ComicController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start the countdown timer when the comic starts
        StartCoroutine(WaitAndSwitchScene());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitAndSwitchScene()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Switch to the Day1 scene
        SceneManager.LoadScene("Day1");
    }
}
