using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<Quest> activeQuests = new List<Quest>();
    public QuestUI questUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddQuest(Quest quest)
    {
        if (quest.state == QuestState.NotStarted)
        {
            quest.state = QuestState.InProgress;
            activeQuests.Add(quest);
            questUI.UpdateUI();
            Debug.Log($"Accepted Quest: {quest.title}");
        }
    }

    public void CompleteObjective(string questId)
    {
        Quest q = activeQuests.Find(x => x.id == questId);
        if (q != null && q.state == QuestState.InProgress)
        {
            q.state = QuestState.ObjectiveCompleted;
            questUI.UpdateUI();
            Debug.Log($"Objective completed for: {q.title}. Return to the quest giver!");
        }
    }

    public void TurnInQuest(string questId)
    {
        Quest q = activeQuests.Find(x => x.id == questId);
        if (q != null && q.state == QuestState.ObjectiveCompleted)
        {
            q.state = QuestState.TurnedIn;
            activeQuests.Remove(q);
            questUI.UpdateUI();
            Debug.Log($"Quest Turned In: {q.title}! Reward Received.");
        }
        }
}
