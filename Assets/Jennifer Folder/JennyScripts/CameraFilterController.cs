using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Runtime.CompilerServices;

public class CameraFilterController : MonoBehaviour
{
    public static bool InCleaningMode { get; private set; } = false;

    private static Vignette vignetteEffect;
    private static ColorAdjustments colorAdjustments;
    private static DepthOfField depthOfField;
    private static FilmGrain filmGrain;

    private void Awake()
    {
        InCleaningMode = false;

        Volume volume = GetComponent<Volume>();

        volume.profile.TryGet(out vignetteEffect);
        volume.profile.TryGet(out colorAdjustments);
        volume.profile.TryGet(out depthOfField);
        volume.profile.TryGet(out filmGrain);


        SetCleaningMode(false);
    }

    public static void SetCleaningMode(bool inCleaningMode) 
    {
        InCleaningMode = inCleaningMode;

        if (vignetteEffect != null) 
        {
            vignetteEffect.active = inCleaningMode;
        }
        
        if (colorAdjustments != null) 
        {
            colorAdjustments.active = inCleaningMode;
        }

        if (depthOfField != null) 
        {
            depthOfField.active = inCleaningMode;
        }

        if (filmGrain != null) 
        { 
            filmGrain.active = inCleaningMode;
        }
    }
}
