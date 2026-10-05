using UnityEngine;

public class MopPainter : ObjectiveMechanics
{
    [Header("Cleaning")]
    [Range(0f, 1f)]
    [SerializeField] float requiredCleanAmount = 0.9f;

    [SerializeField] int textureResolution = 256;
    [SerializeField] int brushSize = 10;
    [SerializeField] mopScript mop;

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

    public void CleanAtUV(Vector2 uv)
    {
        if (!mop.isWet)
            return;

        if (completed)
            return;

        if (mop.isWet)
            mop.MopUseWater();

        int centerX = Mathf.RoundToInt(uv.x * textureResolution);
        int centerY = Mathf.RoundToInt(uv.y * textureResolution);

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
                }
            }
        }

        cleaningMask.Apply();

        float cleanPercentage = (float)cleanedPixels / totalPixels;

        if (cleanPercentage >= requiredCleanAmount)
        {
            CompleteCleaning();
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
        Finish();
        Debug.Log("Floor cleaned!");


    }

    public float GetCleanPercentage()
    {
        return (float)cleanedPixels / totalPixels;
    }
}
