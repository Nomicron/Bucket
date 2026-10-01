using UnityEngine;

public class PlacementZone : MonoBehaviour
{
    public string acceptedItemID;

    [SerializeField]
    Transform snapPoint;
    [SerializeField]
    int maxCapacity = 1;

    [SerializeField]
    float minThrowRange = 50f;
    [SerializeField]
    float maxThrownRange = 150f;

    int currentItemCount = 0;
    public bool IsOccupied => currentItemCount >= maxCapacity;
    public Transform SnapPoint => snapPoint != null ? snapPoint : transform;

    //Suspicion
    [SerializeField]
    int suspicionAmount = 0;
    [SerializeField]
    Character targetNPC;
    public bool AcceptsItem(string itemID) 
    {
        // Reject if there is already an item here
        if (IsOccupied)
        {
            return false;
        }

        // If the zone doesn't have a specific ID set, it accepts everything
        if (string.IsNullOrEmpty(acceptedItemID))
        {
            return true;
        }

        // Only accept if the object's ID matches the zone's required ID
        return acceptedItemID == itemID;
    }
    public void AddItem(bool wasThrown, float throwDist) 
    {
        currentItemCount++;
        if(wasThrown && throwDist >= minThrowRange && throwDist <= maxThrownRange)
        {
            OnSuccessfullThrow(throwDist);
        }

        if (targetNPC != null && SuspicionController.Instance != null)
        {
            SuspicionController.Instance.AddSuspicion(targetNPC, suspicionAmount);
        }
        if (IsOccupied)
        {
            Destroy(gameObject);
        }
    }

    private void OnSuccessfullThrow(float dist) 
    {
        Debug.Log("Amazing throw");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
