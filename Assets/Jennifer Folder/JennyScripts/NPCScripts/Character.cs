using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    public Character character;
    SpriteRenderer spriteRenderer;

    public string name;
    public string description;
    public Sprite sprite;
    public DialogueData[] dialogueData;

    [SerializeField]
    int maxSuspicion = 100;

    int currentSuspicion = 0;
    public string NPCName => name;
    public int CurrentSuspicion => currentSuspicion;
    public int MaxSuspicion => maxSuspicion;

    // Event that broadcasts updates to specific NPC UI Bar
    public UnityEvent<int, int> onSuspicionChanged;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ModifySuspicion(int amount)
    {
        currentSuspicion = Mathf.Clamp(currentSuspicion + amount, 0, maxSuspicion);

        onSuspicionChanged?.Invoke(currentSuspicion, maxSuspicion);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

