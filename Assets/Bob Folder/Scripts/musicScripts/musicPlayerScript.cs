using UnityEngine;

public class musicPlayerScript : ObjectiveMechanics
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 3.0f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private musicDiskScript musicDisk;
    [SerializeField] private musicCrankScript musicCrank;

    void Update()
    {
        
    }

    private void OnMouseDown()
    {      
         ToggleMusic();      
    }

    void ToggleMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            musicDisk.StopPlaying();
        }
        else
        {
            Finish();
            audioSource.Play();
            musicDisk.StartPlaying();
            musicCrank.StartPlaying();
        }
    }
}
