using UnityEngine;

public class roomScript : MonoBehaviour
{

   
    public string roomName;

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
