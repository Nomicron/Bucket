using UnityEngine;

public class musicDiskScript : MonoBehaviour
{
     [SerializeField] private float rotationSpeed = 50.0f;
    private bool isPlaying = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying)
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    public void StartPlaying()
    {
        isPlaying = true;
    }
    public void StopPlaying()
    { 
        isPlaying = false; 
    }
}
