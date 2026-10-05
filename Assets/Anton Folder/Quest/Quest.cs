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
    //public string doneTitle;
    [TextArea] public string description;
    public QuestState state = QuestState.NotStarted;
    //public ObjectiveType questType;
}
[System.Serializable]

public class Task 
{
    public string id;
    public string title;
   // public string doneTitle;
    [TextArea] public string description;
    public TaskState state = TaskState.NotStarted;
    //public ObjectiveType taskType;
}