using UnityEngine;
using System.Collections.Generic;
public class roomScript : MonoBehaviour
{
    public string roomName;
    public float cleanLevel = 0f;
    public bool  isCleaned = false;
    public bool givesTrust = false;
    public Character mainCharacter;

    [SerializeField] public List<Task> questsInRoom = new List<Task>();

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            roomManager.Instance.EnterRoom(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            roomManager.Instance.ExitRoom(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player") && roomManager.Instance.currentRoom == roomManager.Instance.outside)
        {
            roomManager.Instance.EnterRoom(this);
        }

    }
}
