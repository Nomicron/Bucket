using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Xml.Schema;

public class roomManager : MonoBehaviour
{

    public static roomManager Instance;
    public roomScript currentRoom;
    public List<roomScript> roomList;

    public S_CleanLevelUI cleanLevelUI;

    
    private void Awake()
    {
        Instance = this;
        roomList = new List<roomScript>(FindObjectsByType<roomScript>());
    }

    public void EnterRoom(roomScript room)
    {
        if (currentRoom == room)
            return;

        currentRoom = room;

        Debug.Log("Player entered room: " + room.roomName);
        cleanLevelUI.UpdateUI(room.cleanLevel, room);
    }

    public void ExitRoom(roomScript room)
    {
        if (currentRoom == room)
        {
            Debug.Log("Player left room: " + room.roomName);

            currentRoom = null;
        }
    }

    public void AddTasks(Task task)
    {
       //if (task.room == currentRoom ) 
       //{ 
       //     Debug.Log("Task added to current room: " + task.title);
       //}

        foreach (var room in roomList)
        {
            if (task.room == room && task.isCleaningTask)
            {
                Debug.Log("Task added to room: " + task.title + " in room: " + room.roomName);
                room.questsInRoom.Add(task);
                return;
            }
        }

    }

    public void RemoveTask(Task task)
    {
        foreach (var room in roomList)
        {
            if (task.room == room && task.isCleaningTask)
            {
                Debug.Log("Task removed from room: " + task.title + " in room: " + room.roomName);
               // room.questsInRoom.Remove(task);
                float number = 100f / room.questsInRoom.Count;
                room.cleanLevel += number; 
                Debug.Log("Room clean level increased by: " + number + " New clean level: " + room.cleanLevel);
                cleanLevelUI.UpdateUI(room.cleanLevel, room);
                return;
            }
        }
    }

    public void CheckRoomTasks()
    {
        foreach (var room in roomList)
        {
            if (room.questsInRoom.Count == 0)
                room.cleanLevel = 100f;
            else
                room.cleanLevel = 0f;
        }
    }
}

