using UnityEngine;
using UnityEngine.Events;
// How to use: Add to NPC GameObjects to keep track of their suspicion
public class NPCSuspicionProfile : MonoBehaviour
{
    [SerializeField]
    string npcName = "";
    [SerializeField]
    int maxSuspicion = 100;

    int currentSuspicion = 0;

    
    public string NPCName => npcName;
    public int CurrentSuspicion => currentSuspicion;
    public int MaxSuspicion => maxSuspicion;

    // Event that broadcasts updates to specific NPC UI Bar
    public UnityEvent<int, int> onSuspicionChanged;

    public void ModifySuspicion(int amount) 
    {
        currentSuspicion = Mathf.Clamp(currentSuspicion + amount, 0 , maxSuspicion);

        onSuspicionChanged?.Invoke(currentSuspicion, maxSuspicion);
    }
}
