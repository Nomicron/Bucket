using UnityEngine;

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
