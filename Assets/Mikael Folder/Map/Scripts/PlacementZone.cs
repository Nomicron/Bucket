using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class PlacementZone : MonoBehaviour
{
    public string acceptedItemID;

    [SerializeField]
    Transform snapPoint;
    [SerializeField]
    int maxCapacity = 1;

    [SerializeField]
    float minThrowDist = 2f;
    [SerializeField]
    float maxThrowDist = 200f;

    [SerializeField]
    Transform trashVisual;
    [SerializeField]
    Vector3 startPos = new Vector3(0f, -0.5f, 0f);
    [SerializeField]
    Vector3 endPos = new Vector3(0f, 0, 0f);
    [SerializeField]
    bool isTrashCan = false;

    int currentItemCount = 0;
    public bool IsOccupied => currentItemCount >= maxCapacity;
    public Transform SnapPoint => snapPoint != null ? snapPoint : transform;

    //Suspicion
    [SerializeField]
    int suspicionAmount = 0;
    [SerializeField]
    Character targetNPC;


    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip successThrowSound;
    [SerializeField]
    AudioClip tooCloseSound;

    [SerializeField] private VisualEffect dustEffect;
    [SerializeField] private float destroyDelay = 3.0f;
    public bool AcceptsItem(string itemID) 
    {
        if (IsOccupied)
        {
            return false;
        }

        if (string.IsNullOrEmpty(acceptedItemID))
        {
            return true;
        }

        return acceptedItemID == itemID;
    }
    public void AddItem(bool wasThrown, Vector3 throwOrigin, GameObject incomingItem, bool destroyOnPlacement) 
    {
        currentItemCount++;
        UpdateTrashVisualPosition();
        if(wasThrown)
        {
            float actualThrowDist = Vector3.Distance(throwOrigin, transform.position);

            if (actualThrowDist >= minThrowDist && actualThrowDist <= maxThrowDist)
            {
                OnSuccessfulThrow(actualThrowDist);
            }
            else
            {
                OnCloseThrow(actualThrowDist);
                Debug.Log($"Thrown, but too close! Distance: {actualThrowDist} (Min required: {minThrowDist})");
            }
        }

        if (targetNPC != null && SuspicionController.Instance != null)
        {
            SuspicionController.Instance.AddSuspicion(targetNPC, suspicionAmount);
        }

        if (destroyOnPlacement)
        {
            Destroy(incomingItem);
        }

        if (IsOccupied)
        {
            if (!isTrashCan) 
            {
                Destroy(gameObject,destroyDelay);
            }
        }
     
    }
    private void UpdateTrashVisualPosition()
    {
        if (trashVisual == null) return;


        float fillPercent = Mathf.Clamp01((float)currentItemCount / maxCapacity);


        trashVisual.localPosition = Vector3.Lerp(startPos, endPos, fillPercent);
        trashVisual.gameObject.SetActive(true);
    }

    private void OnSuccessfulThrow(float dist) 
    {
        Debug.Log($"Amazing throw. Distance : {dist}");
        PlaySound(successThrowSound);
        TriggerDustEffect();
    }

    private void OnCloseThrow(float dist)
    {
        PlaySound(tooCloseSound);
        TriggerDustEffect();
    }

    private void TriggerDustEffect()
    {
        if (dustEffect == null) return;


        dustEffect.Reinit();
        dustEffect.Play();
        Invoke(nameof(StopDustEffect), 0.25f);
    }

    private void StopDustEffect()
    {
        if (dustEffect != null)
        {
            dustEffect.Stop();
        }
    }
    private void PlaySound(AudioClip clip)
    {
        if (audioSource == null || clip == null) return;

        audioSource.Stop();

        audioSource.clip = clip;
        audioSource.Play();
    }

    void Start()
    {
        if(trashVisual != null) 
        {
            trashVisual.gameObject.SetActive(false);
        }
        dustEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
