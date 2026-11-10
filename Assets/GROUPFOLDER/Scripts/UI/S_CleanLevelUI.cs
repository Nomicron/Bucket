using TMPro;
using UnityEngine;

public class S_CleanLevelUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    public TextMeshProUGUI cleanLeveltext;
    //public roomManager roomManager;
    void Start()
    {
        cleanLeveltext.text = "Clean Level: " + 0;
    }

    // Update is called once per frame
   public void UpdateUI(float cleanLevel, roomScript room)
    {
        cleanLeveltext.text = $"{room.roomName}" + ", Clean Level: " + $"{cleanLevel:F2}" + "%";
        //Debug.Log("Clean Level updated to: " + cleanLevel.ToString() + " in room: " + room.roomName);
    }
}
