using UnityEngine;

[RequireComponent(typeof(Collider))]
public class QuestDestination : ObjectiveMechanics
{
    //public string questId;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // QuestManager.Instance.CompleteQuest(questId);
            Finish();
        }

    }
}
