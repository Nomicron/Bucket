using System.Collections.Generic;
using UnityEngine;

//handles Task and quest logics, controls tasklist and calls QuestUI.Update 
public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [SerializeField] public roomManager roomManager;

    [SerializeField] public List<Quest> activeQuests = new List<Quest>();
    [SerializeField] public List<Task> allTasks = new List<Task>();

     public List<Task> activeTasks = new List<Task>();
    public List<Task> cleaningTasks = new List<Task>();

    public QuestUI questUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {

        foreach (Task task in allTasks)
        {
            AddToLists(task);
            roomManager.CheckRoomTasks();
            roomManager.AddTasks(task);

            if (task.state == TaskState.NotStarted)
                task.state = TaskState.InProgress;

        }
        //foreach (Task task in activeTasks)
        //{
        //    if (!task.isActiveTask)
        //        activeTasks.Remove(task);
        //}

        questUI.UpdateUI();

    }

    public void AddQuest(Quest quest)
    {
        if (quest.state == QuestState.NotStarted)
        {
            quest.state = QuestState.InProgress;
            activeQuests.Add(quest);
            questUI.UpdateUI();
           // Debug.Log($"Accepted Quest: {quest.title}");
        }
    }

    public void CompleteQuest(string questId)
    {
        Quest q = activeQuests.Find(x => x.id == questId);
        if (q != null && q.state == QuestState.InProgress)
        {
            q.state = QuestState.ObjectiveCompleted;
            questUI.UpdateUI();
           // Debug.Log($"Quest completed for: {q.title}. Return to the quest giver!");
        }
    }

    public void TurnInQuest(string questId)
    {
        Quest q = activeQuests.Find(x => x.id == questId);
        if (q != null && q.state == QuestState.ObjectiveCompleted)
        {
            q.state = QuestState.TurnedIn;
            // activeQuests.Remove(q);
            questUI.UpdateUI();
            // Debug.Log($"Quest Turned In: {q.title}! Reward Received.");
        }
    }

    public void CompleteTask(string taskId)
    {
        Task t = allTasks.Find(x => x.id == taskId);
        if (t != null && t.state == TaskState.InProgress)
        {
            t.state = TaskState.ObjectiveCompleted;
            roomManager.RemoveTask(t);
            questUI.UpdateUI();
            // Debug.Log($"Task completed for: {t.title}. Return to the quest giver!");
        }
    }

    private void AddToLists(Task task) 
    {
        if(task.isActiveTask && !activeTasks.Contains(task))
        {
            activeTasks.Add(task);
        }

        if(task.isCleaningTask && !cleaningTasks.Contains(task))
        {
            cleaningTasks.Add(task);
        }

    }

    //private void AddRoomTask(string roomName)
    //{
    //    questsInRoom.Clear();
    //    foreach (Task task in activeTasks)
    //    {
    //        if (task.room != null && task.room.roomName == roomName && task.state != TaskState.ObjectiveCompleted)
    //        {
    //            questsInRoom.Add(task);
    //        }
    //    }
    //}
    //private void CompleteRoom(string roomName) 
    //{ 
    //    for(int i = 0; i < activeTasks.Count; i++)
    //    {
    //        Task task = activeTasks[i];
    //        if (task.room != null && task.room.roomName == roomName)
    //        {
    //            if (task.state != TaskState.ObjectiveCompleted)
    //            {
    //                Debug.Log($"Completing task: {task.title} for room: {roomName}");
    //                return;
    //            }
    //        }
    //    }

    //}
}
