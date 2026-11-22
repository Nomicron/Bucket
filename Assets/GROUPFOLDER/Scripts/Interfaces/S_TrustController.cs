using UnityEngine;

public class TrustController : MonoBehaviour
{
    public static TrustController Instance;

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

    public void AddTrust(Character targetNPC, int amount) 
    {
        if(targetNPC != null) 
        {
            targetNPC.ModifyTrust(amount);
        }
        Debug.Log($"Added {amount} trust to {targetNPC.NPCName}. Current Trust: {targetNPC.CurrentTrust}/{targetNPC.MaxTrust}");
    }

    public void AddTrustAll(int amount) 
    {
        foreach(Character character in targetNPCs) 
        {
            if(character != null) 
            {
                character.ModifyTrust(amount);
            }
        }
    }
}
