using UnityEngine;
using TMPro;
using System.Collections;

public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI questListText;

    public void UpdateUI()
    {

        if (questListText == null) return;


        questListText.text = "<b>Tasks: </b>\n\n";

        if (QuestManager.Instance.activeQuests.Count == 0 && QuestManager.Instance.activeTasks.Count == 0)
        {
            questListText.text += "<i>No active quests</i>";
            return;
        }

        foreach (Task t in QuestManager.Instance.activeTasks)
        {

            if (t.state == TaskState.ObjectiveCompleted)
            {
                questListText.text += $"<b>• <s>{t.title}</s></b> \n";
            }
            else
            {
                questListText.text += $"<b>• {t.title}\n";
            }

            if (!string.IsNullOrEmpty(t.description))
            {
                questListText.text += $"   <i>{t.description}</i>\n\n";
            }
        }

        foreach (Quest q in QuestManager.Instance.activeQuests)
        {

                //    string status = q.state == QuestState.InProgress 
                //? "<color=yellow>[In Progress]</color>"
                //: "<color=green>[Ready to Turn In]</color>";

            // Displays Title, Status, and Description
            // questListText.text += $"<b>• {q.title}</b> {status}\n";
            if (q.state == QuestState.TurnedIn)
            {
                questListText.text += $"<b>• <s>{q.title}</s></b> \n";
            }
            else
            {
                questListText.text += $"<b>• {q.title}\n";
            }

            if (!string.IsNullOrEmpty(q.description))
            {
                questListText.text += $"   <i>{q.description}</i>\n\n";
            }
        }

        

    }
}
