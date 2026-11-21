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
    int maxTrust = 100;

    int currentTrust = 0;

    public string NPCName => name;
    public int CurrentTrust => currentTrust;
    public int MaxTrust => maxTrust;

    // Event that broadcasts updates to specific NPC UI Bar
    public UnityEvent<int, int> onTrustChanged;

    public void ModifyTrust(int amount)
    {
        currentTrust = Mathf.Clamp(currentTrust + amount, 0, maxTrust);

        onTrustChanged?.Invoke(currentTrust, maxTrust);
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

