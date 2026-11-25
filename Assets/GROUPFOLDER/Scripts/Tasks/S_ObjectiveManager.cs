using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//handles Task and quest logics, controls tasklist and calls QuestUI.Update
public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    [SerializeField] public roomManager roomManager;

    [SerializeField] public Task collectSuppliesTask;
    [SerializeField] public Task disposeSuppliesTask;
    [SerializeField] public string nextSceneName;

    [SerializeField] public List<Quest> activeQuests = new List<Quest>();
    [SerializeField] public List<Task> allTasks = new List<Task>();

     public List<Task> activeTasks = new List<Task>();
    public List<Task> cleaningTasks = new List<Task>();

    public QuestUI questUI;

    private Task firstTask;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        firstTask = new Task();

        if (collectSuppliesTask != null && !string.IsNullOrEmpty(collectSuppliesTask.id))
        {
            collectSuppliesTask.state = TaskState.InProgress;
            activeTasks.Add(collectSuppliesTask);
            questUI.UpdateUI();
            return;
        }

        StartAllTasks();
    }

    private void StartAllTasks()
    {
        foreach (Task task in allTasks)
        {
            AddToLists(task);
            //roomManager.CheckRoomTasks();
            roomManager.AddTasks(task);

            if (task.state == TaskState.NotStarted)
                task.state = TaskState.InProgress;

        }
        //foreach (Task task in activeTasks)
        //{
        //    if (!task.isActiveTask)
        //        activeTasks.Remove(task);
        //}
        roomManager.CheckRoomTasks();
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
            q.state = QuestState.TurnedIn; // This should be set to ObjectiveCompleted first, then TurnedIn when the player turns it in.
            TrustController.Instance.AddTrust(q.character, 10); // Increase trust by 10 points for the character associated with the quest.
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
        // ADDED: Step 1 -> Remove collect task and activate main tasks
        if (collectSuppliesTask != null && collectSuppliesTask.id == taskId && collectSuppliesTask.state == TaskState.InProgress)
        {
            collectSuppliesTask.state = TaskState.ObjectiveCompleted;
            activeTasks.Remove(collectSuppliesTask);
            StartAllTasks();
            return;
        }

        // ADDED: Step 3 -> Dispose supplies task finished -> load next scene
        if (disposeSuppliesTask != null && disposeSuppliesTask.id == taskId && disposeSuppliesTask.state == TaskState.InProgress)
        {
            disposeSuppliesTask.state = TaskState.ObjectiveCompleted;
            activeTasks.Remove(disposeSuppliesTask);
            questUI.UpdateUI();

            if (!string.IsNullOrEmpty(nextSceneName))
                SceneManager.LoadScene(nextSceneName);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            return;
        }

        Task t = allTasks.Find(x => x.id == taskId);
        if (t != null && t.state == TaskState.InProgress)
        {
            t.state = TaskState.ObjectiveCompleted;
            roomManager.RemoveTask(t);

            // ADDED: Step 2 -> When all active tasks are done, clear them and show dispose task
            if (activeTasks.TrueForAll(x => x.state == TaskState.ObjectiveCompleted))
            {
                activeTasks.Clear();

                if (disposeSuppliesTask != null && !string.IsNullOrEmpty(disposeSuppliesTask.id))
                {
                    disposeSuppliesTask.state = TaskState.InProgress;
                    activeTasks.Add(disposeSuppliesTask);
                }
            }

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
