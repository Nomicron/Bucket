using UnityEngine;

public enum QuestState
{
    NotStarted,
    InProgress,
    ObjectiveCompleted,
    TurnedIn
}

[System.Serializable]
public class Quest
{
    public string id;
    public string title;
    [TextArea] public string description;
    public QuestState state = QuestState.NotStarted;
}
