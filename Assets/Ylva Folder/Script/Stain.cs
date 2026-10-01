using UnityEngine;

public class Stain : MonoBehaviour
{
    public StainType stainType;

    [SerializeField] Sprite[] cleaningStages;

    SpriteRenderer spriteRenderer;
    public int currentStage = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Clean()
    {
        currentStage++;

        if (currentStage >= cleaningStages.Length)
        {
            Destroy(gameObject);
            return;
        }

        spriteRenderer.sprite = cleaningStages[currentStage];
    }
}