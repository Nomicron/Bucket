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
    public string groupId; // Tasks with the same groupId + room are displayed together.
    public string id;
    public string title;
    [TextArea] public string description;
    public TaskState state = TaskState.NotStarted;
    public roomScript room;
    public bool isCleaningTask;
    public bool isActiveTask;
    //public TaskType taskType = TaskType.None;
}
//public enum TaskType
//{
//    None, Dust, Stain, Trash, Floor, PickUp, Other
//}