using TMPro;
using UnityEngine;
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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

