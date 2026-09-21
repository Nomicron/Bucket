using UnityEngine;

[RequireComponent(typeof(Collider))]
public class QuestDestination : MonoBehaviour
{
    public string questId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestManager.Instance.CompleteObjective(questId);
        }
    }
}
