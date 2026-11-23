using System.Collections.Generic;
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
    public List<string> clues = new List<string>();

    public List<string> statements = new List<string>();
    public List<string> info = new List<string>();

    [SerializeField]
    int maxTrust = 100;

    int currentTrust = 50;

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

    public void AddClue(string clue) 
    {
        if (clues.Contains(clue)) return;

        clues.Add(clue);
        S_Notification.Instance.ShowNotification();
    }

    public void AddStatement(string a) 
    {
        if (!statements.Contains(a)) 
        {
            statements.Add(a);
        }
    }

    public void AddInfo(string text) 
    {
        if (info.Contains(text)) 
        {
            return;
        }
        info.Add(text);
        S_Notification.Instance.ShowNotification();
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

