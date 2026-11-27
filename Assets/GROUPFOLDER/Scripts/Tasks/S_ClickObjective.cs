using UnityEngine;

public class ClickObjective : ObjectiveMechanics
{
    [Tooltip("Optional: Drag the Player here so they must walk close before clicking.")]
    public Transform player;
    public float maxDistance = 3f;

    private ObjectiveConnector connector;

    private void Awake()
    {
        connector = GetComponent<ObjectiveConnector>();
    }

    private void OnMouseDown()
    {
        if (Finished) return;

        // Only allow clicking when this task is currently active
        if (connector != null && !ObjectiveManager.Instance.activeTasks.Exists(t => t.id == connector.id && t.state == TaskState.InProgress))
        {
            return;
        }

        // Optional distance check
        if (player != null && Vector3.Distance(transform.position, player.position) > maxDistance)
        {
            return;
        }

        Finish();
    }
}
