using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_CleanLevelUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    public TextMeshProUGUI cleanLeveltext;
    //public roomManager roomManager;
    [SerializeField] private Image cleanBar;
    [SerializeField] private Gradient cleanGradient;
    [SerializeField] private float maxCleanAmount = 100f;

    public void ShowText(bool status)
    {
        cleanLeveltext.gameObject.SetActive(status);
    }

    // Update is called once per frame
   public void UpdateUI(float cleanLevel, roomScript room)
    {
        float normalizedCleanLevel = Mathf.Clamp01(cleanLevel / maxCleanAmount);

        cleanLeveltext.text = $"{room.roomName}, Clean Level: {cleanLevel:F2}%";
        cleanBar.fillAmount = normalizedCleanLevel;
        cleanBar.color = cleanGradient.Evaluate(normalizedCleanLevel);

        Debug.Log($"UI Updated: {room.roomName}" + $" | Clean Level: {cleanLevel}" + $" | Fill Amount: {normalizedCleanLevel}");
    }

}

