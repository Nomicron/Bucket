using UnityEngine;

public class roomManager : MonoBehaviour
{

    public static roomManager Instance;
    public roomScript currentRoom;

    private void Awake()
    {
        Instance = this;
    }

    public void EnterRoom(roomScript room)
    {
        if (currentRoom == room)
            return;

        currentRoom = room;

        Debug.Log("Player entered room: " + room.roomName);
    }

    public void ExitRoom(roomScript room)
    {
        if (currentRoom == room)
        {
            Debug.Log("Player left room: " + room.roomName);

            currentRoom = null;
        }
    }
}

