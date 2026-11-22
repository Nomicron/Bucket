using TMPro;
using UnityEngine;

public class S_Notification : MonoBehaviour
{
    public static S_Notification Instance;

    [SerializeField] TMP_Text notificationText;
    [SerializeField] float duration = 4f;
    float timer;

    void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        notificationText.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) 
        {
            timer -= Time.deltaTime;

            notificationText.alpha = Mathf.Clamp01(Mathf.Min(timer, duration - timer));
        }
    }

    public void ShowNotification() 
    {
        timer = duration;
    }
}
