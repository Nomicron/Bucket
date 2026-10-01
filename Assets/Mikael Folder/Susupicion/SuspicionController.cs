using UnityEngine;

public class SuspicionController : MonoBehaviour
{
    public static SuspicionController Instance;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void AddSuspicion(Character targetNPC, int amount) 
    {
        if(targetNPC != null) 
        {
            targetNPC.ModifySuspicion(amount);
        }
    }
}
