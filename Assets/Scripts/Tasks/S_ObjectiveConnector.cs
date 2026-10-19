using UnityEngine;

//listens to objetiveMechanics raised events and calls questmanager CompleteTask() 
public class ObjectiveConnector : MonoBehaviour
{
    public ObjectiveMechanics objectiveMechanics;
    public string id;
    public ObjectiveType type;

    private void Awake()
    {
        if (objectiveMechanics == null)
            objectiveMechanics = GetComponent<ObjectiveMechanics>();
    }

    private void OnEnable()
    {

        if (objectiveMechanics == null)
        {
            return;
        }

        objectiveMechanics.OnFinished += CleaningFinished;
    }

    private void OnDisable()
    {
        if (objectiveMechanics != null)

            objectiveMechanics.OnFinished -= CleaningFinished;
    }

    private void CleaningFinished(ObjectiveMechanics mechanic)
    {
        if(type == ObjectiveType.Task)
             QuestManager.Instance.CompleteTask(id);
        else 
            QuestManager.Instance.CompleteQuest(id);
    }
}

public enum ObjectiveType
{ 
    Task, 
    Quest
}