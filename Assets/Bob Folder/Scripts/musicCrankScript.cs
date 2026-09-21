using UnityEngine;

public class musicCrankScript : MonoBehaviour
{
     [SerializeField] private float rotationSpeed = 50.0f;
     [SerializeField] private float crankTimer = 5.0f;
    public bool isPlaying = false;
    private float timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartPlaying();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;

            transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);

            if (timer >= crankTimer)
            {
                isPlaying = false;
            }
        }
    }

    public void StartPlaying()
    {
        timer = 0f;
        isPlaying = true;
    }

}
