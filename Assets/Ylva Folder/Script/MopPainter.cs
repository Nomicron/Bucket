using UnityEngine;
using UnityEngine.VFX;

public class MopPainter : ObjectiveMechanics
{
    [Header("Cleaning")]
    [Range(0f, 1f)]
    [SerializeField] float requiredCleanAmount = 0.9f;

    [SerializeField] int textureResolution = 256;
    [SerializeField] int brushSize = 10;
    [SerializeField] mopScript mop;

    //Added by Bob
    [SerializeField] bool useBubbles = false;
    [SerializeField] private GameObject[] bubbleVFXPrefabs;
    [SerializeField] private float bubbleInterval = 0.2f;
    [SerializeField] private float bubbleLifetime = 1.2f;
    private float bubbleTimer = 0f;

    Texture2D cleaningMask;

    int cleanedPixels;
    int totalPixels;
    public bool completed;

    void Start()
    {
        cleaningMask = new Texture2D(
            textureResolution,
            textureResolution,
            TextureFormat.R8,
            false
        );

        // Start completely dirty.
        Color[] pixels = new Color[textureResolution * textureResolution];

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.black;
        }

        cleaningMask.SetPixels(pixels);
        cleaningMask.Apply();

        totalPixels = textureResolution * textureResolution;

        // Give the material our runtime cleaning mask.
        GetComponent<Renderer>().material.SetTexture(
            "CleaningMask", cleaningMask
        );


        Renderer rend = GetComponent<Renderer>();

        rend.material.SetTexture("_CleaningMask", cleaningMask);

        //Debug.Log("Mask property exists: " +
        //          rend.material.HasProperty("_CleaningMask"));

        //Debug.Log("Assigned mask: " +
        //          rend.material.GetTexture("_CleaningMask"));
    }

    public void CleanAtUV(Vector2 uv, Vector3 hitPoint) // added vector3 for bubbles
    {
        if (!mop.isWet)
            return;

        if (completed)
            return;



        int centerX = Mathf.RoundToInt(uv.x * textureResolution);
        int centerY = Mathf.RoundToInt(uv.y * textureResolution);

        bool cleanedSomething = false; // Added by Bob

        for (int x = -brushSize; x <= brushSize; x++)
        {
            for (int y = -brushSize; y <= brushSize; y++)
            {
                // Make the brush circular.
                if (x * x + y * y > brushSize * brushSize)
                    continue;

                int pixelX = centerX + x;
                int pixelY = centerY + y;

                if (pixelX < 0 || pixelX >= textureResolution ||
                    pixelY < 0 || pixelY >= textureResolution)
                    continue;

                // Don't count pixels we've already cleaned.
                if (cleaningMask.GetPixel(pixelX, pixelY).r < 0.5f)
                {
                    cleaningMask.SetPixel(pixelX, pixelY, Color.white);
                    cleanedPixels++;
                    // Debug.Log($"Total cleaned: {cleanedPixels}/{totalPixels}");
                    cleanedSomething = true;
                }
            }
        }

        //cleaningMask.Apply();

        //float cleanPercentage = (float)cleanedPixels / totalPixels;

        //if (cleanPercentage >= requiredCleanAmount)
        //{
        //    CompleteCleaning();
        //}

        // Added by Bob
        if (cleanedSomething)
        {
            cleaningMask.Apply();

            mop.MopUseWater();


            if (bubbleTimer <= 0f && useBubbles)
            {
                SpawnBubbles(hitPoint);
                bubbleTimer = bubbleInterval;
            }

            float cleanPercentage =
                (float)cleanedPixels / totalPixels;

            if (cleanPercentage >= requiredCleanAmount)
            {
                CompleteCleaning();
            }

        }
    }


        void CompleteCleaning() // Added by Bob
        {
            completed = true;

            Color[] pixels = new Color[textureResolution * textureResolution];

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color.white;
            }

            cleaningMask.SetPixels(pixels);
            cleaningMask.Apply();

            cleanedPixels = totalPixels;

            Debug.Log("Floor cleaned!");


        }

        cleaningMask.SetPixels(pixels);
        cleaningMask.Apply();

        cleanedPixels = totalPixels;
        Finish();
        Debug.Log("Floor cleaned!");


    }

    public float GetCleanPercentage()
    {
        return (float)cleanedPixels / totalPixels;
    }

     void Update()
     {
        if (bubbleTimer > 0f)
        {
            bubbleTimer -= Time.deltaTime;
        }
     }
    private void SpawnBubbles(Vector3 position)
    {
        if (bubbleVFXPrefabs == null || bubbleVFXPrefabs.Length == 0)
            return;

        int index = Random.Range(0, bubbleVFXPrefabs.Length);

        GameObject bubble = Instantiate(
            bubbleVFXPrefabs[index],
            position + Vector3.up * 0.02f,
            Quaternion.identity
        );

        Destroy(bubble, bubbleLifetime);
    }
}

