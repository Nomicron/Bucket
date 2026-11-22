using UnityEngine;
using System.Collections.Generic;
public class roomScript : MonoBehaviour
{
    public string roomName;
    public float cleanLevel = 0f;


    [SerializeField] public List<Task> questsInRoom = new List<Task>();


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
}
