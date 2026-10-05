using UnityEngine;

public class Stain : ObjectiveMechanics
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
            Finish();
            Destroy(gameObject);
            return;
            
        }

        spriteRenderer.sprite = cleaningStages[currentStage];
    }
}