using UnityEngine;

[RequireComponent(typeof(Collider))]
public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    private void OnMouseDown()
    {
        switch (quest.state)
        {
            case QuestState.NotStarted:
                QuestManager.Instance.AddQuest(quest);
                break;

            case QuestState.InProgress:
                Debug.Log($"Quest '{quest.title}' is in progress! Go to the target destination.");
                break;

            case QuestState.ObjectiveCompleted:
                QuestManager.Instance.TurnInQuest(quest.id);
                break;

            case QuestState.TurnedIn:
                Debug.Log("Thank you for your help!");
                break;
        }
    }
}
