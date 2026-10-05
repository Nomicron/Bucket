using UnityEngine;

public enum QuestState
{
    NotStarted,
    InProgress,
    ObjectiveCompleted,
    TurnedIn
}

public enum TaskState 
{
    NotStarted,
    InProgress,
    ObjectiveCompleted
}


[System.Serializable]
public class Quest
{
    public string id;
    public string title;
    [TextArea] public string description;
    public QuestState state = QuestState.NotStarted;
}
[System.Serializable]

public class Task 
{
    public string id;
    public string title;
    [TextArea] public string description;
    public TaskState state = TaskState.NotStarted;
}