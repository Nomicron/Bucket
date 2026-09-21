using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI questListText;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (questListText == null) return;

        questListText.text = "<b>ACTIVE QUESTS</b>\n\n";

        if (QuestManager.Instance.activeQuests.Count == 0)
        {
            questListText.text += "<i>No active quests</i>";
            return;
        }

        foreach (Quest q in QuestManager.Instance.activeQuests)
        {
            string status = q.state == QuestState.InProgress
                ? "<color=yellow>[In Progress]</color>"
                : "<color=green>[Ready to Turn In]</color>";

            // Displays Title, Status, and Description
            questListText.text += $"<b>• {q.title}</b> {status}\n";
            if (!string.IsNullOrEmpty(q.description))
            {
                questListText.text += $"   <i>{q.description}</i>\n\n";
            }
        }
    }
}
