using UnityEngine;

//parent class for all scripts that handles finishing a task 
public class ObjectiveMechanics : MonoBehaviour
{
    public bool Finished { get; private set;}
    //public bool AllTasksFinished { get; private set; }

    public event System.Action<ObjectiveMechanics> OnFinished;
   
    protected void Finish() 
    {
        if (Finished)
            return;

        Finished = true;

        OnFinished?.Invoke(this);

    }
    //protected void FinishAll()
    //{
    //    if(AllTasksFinished)
    //        return;

    //    AllTasksFinished = true;

    //    OnFinished?.Invoke(this);
    //}
}
