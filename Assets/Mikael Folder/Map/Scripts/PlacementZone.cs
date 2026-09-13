using UnityEngine;

public class PlacementZone : MonoBehaviour
{
    public string acceptedItemID;

    [SerializeField]
    Transform snapPoint;

    public bool IsOccupied { get; private set; }
    public Transform SnapPoint => snapPoint != null ? snapPoint : transform;

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

    public void MarkOccupied()
    {
        IsOccupied = true;
    }
    //// Draws visual guides in the Unity Editor scene view to make level designing easier
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
    //    Gizmos.DrawSphere(SnapPoint.position, 0.15f);
    //}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
