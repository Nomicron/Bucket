using System.Collections.Generic;
using UnityEngine;

//handles Task and quest logics, controls tasklist and calls QuestUI.Update 
public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [SerializeField] public List<Quest> activeQuests = new List<Quest>();
    [SerializeField] public List<Task> activeTasks = new List<Task>();

    public QuestUI questUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {

        foreach (Task task in activeTasks)
        {
            if (task.state == TaskState.NotStarted)
                task.state = TaskState.InProgress;
        }

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
        Task t = activeTasks.Find(x => x.id == taskId);
        if (t != null && t.state == TaskState.InProgress)
        {
            t.state = TaskState.ObjectiveCompleted;
            questUI.UpdateUI();
            // Debug.Log($"Task completed for: {t.title}. Return to the quest giver!");
        }
    }
}
