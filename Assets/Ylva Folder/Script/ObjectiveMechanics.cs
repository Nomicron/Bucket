using UnityEngine;

//parent class for all scripts that handles finishing a task 
public class ObjectiveMechanics : MonoBehaviour
{
    public bool Finished { get; private set;}

    public event System.Action<ObjectiveMechanics> OnFinished;
   
    protected void Finish() 
    {
        if (Finished)
            return;

        Finished = true;

        OnFinished?.Invoke(this);

    }
}
