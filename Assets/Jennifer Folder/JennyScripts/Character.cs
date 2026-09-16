using TMPro;
using UnityEngine;


public class Character : MonoBehaviour
{
    public Character character;
    SpriteRenderer spriteRenderer;

    public string name;
    public string description;
    [SerializeField] Sprite sprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

