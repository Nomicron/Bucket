using UnityEngine;

public class SuspicionController : MonoBehaviour
{
    public static SuspicionController Instance;

    [SerializeField]
    Character[] targetNPCs;

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

    public void AddSuspicionAll(int amount) 
    {
        foreach(Character character in targetNPCs) 
        {
            if(character != null) 
            {
                character.ModifySuspicion(amount);
            }
        }
    }
}
