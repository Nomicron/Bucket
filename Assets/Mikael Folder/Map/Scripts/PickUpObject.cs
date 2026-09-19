using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpObject : MonoBehaviour
{
    public string itemID;

    //[SerializeField]
    //float itemWeight = 2f;
    [SerializeField]
    float throwForce = 600f;
    [SerializeField]
    float maxDistance = 3f;
    [SerializeField]
    float snapRadius = 0.5f;
    [SerializeField]
    float holdDistance = 2f;
    [SerializeField]
    LayerMask zoneLayer;


    bool isHolding = false;
    bool isPlaced = false;
    float distance;

    TempParent tempParent;
    Rigidbody rb;
    PlacementZone currentZone;

    Vector3 objectPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tempParent = TempParent.Instance;

        //if(rb != null) 
        //{
        //    rb.mass = itemWeight;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is currently holding the item, update its position every frame
        if (isHolding)
        {
            Hold();
        }
    }
    // Triggered when the player presses the left mouse button while pointing at this object's collider
    private void OnMouseDown()
    {
        // If it's already locked into a zone, we can't pick it up again
        if (isPlaced)
        {
            return;
        }
        // Pick up
        // Checks if there is a tempParent in scene
        if (tempParent != null)
        {
            // Calculate how far the object is from the player's holding point
            distance = Vector3.Distance(this.transform.position, tempParent.transform.position);
            // Only allow pickup if the player is close enough
            if (distance <= maxDistance)
            {
                isHolding = true;
                rb.useGravity = false;
                rb.detectCollisions = true;
                // Parent the object to the player's holding point so it moves with them
                this.transform.SetParent(tempParent.transform);
            }
        }
        else
        {
            Debug.Log("Temp parent Item not found in scene");
        }
    }

    private void OnMouseUp()
    {
        //Drop
        Drop();
    }

    //private void OnMouseExit()
    //{
    //    //Drop
    //    Drop();
    //}
    // Handles the logic for keeping the object in front of the player and checking for zones/throws
    private void Hold()
    {
        // Check if the player moved too far away
        distance = Vector3.Distance(this.transform.position, tempParent.transform.position);

        if (distance >= maxDistance)
        {
            Drop();
            return;
        }

        Vector3 targetPosition = tempParent.transform.position + tempParent.transform.forward * holdDistance;

        //float followSpeed = Mathf.Max(5f, 30f / itemWeight);
        //Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        rb.MovePosition(targetPosition);
        // Remove any existing momentum so the object doesn't spin or drift wildly while being held
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        currentZone = null;
        // Create an invisible sphere around the object to check for PlacementZones
        Collider[] hits = Physics.OverlapSphere(transform.position, snapRadius, zoneLayer);
        foreach (Collider hit in hits)
        {
            PlacementZone zone = hit.GetComponent<PlacementZone>();
            if (zone != null && zone.AcceptsItem(itemID))
            {
                isHolding = false;
                this.transform.SetParent(null);
                SnapToZone(zone);
                return;
                ////Only snaps when dropped
                //currentZone = zone;
                //break;
            }
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            //Throw
            rb.AddForce(tempParent.transform.forward * throwForce);
            Drop();
        }
    }
    // Resets the object to normal physics behavior when let go
    private void Drop()
    {
        if (isHolding)
        {
            isHolding = false;
            objectPos = this.transform.position;
            this.transform.position = objectPos;
            this.transform.SetParent(null);

            // If the player dropped it while hovering over a valid zone, snap it
            if (currentZone != null)
            {
                SnapToZone(currentZone);
            }
            else
            {
                // Otherwise, reactivate normal physics so it falls to the ground
                rb.isKinematic = false;
                rb.useGravity = true;
            }

        }
    }
    // Triggered when this object physically collides with a trigger collider (like a zone)
    private void OnTriggerEnter(Collider other)
    {
        if(isPlaced || isHolding) 
        {
            return;
        }

        PlacementZone zone = other.GetComponent<PlacementZone>();
        if (zone != null && zone.AcceptsItem(itemID))
        {
            SnapToZone(zone);
        }
    }
    // Locks the object into the designated PlacementZone
    private void SnapToZone(PlacementZone zone)
    {
        isPlaced = true; // Lock it so it can't be picked up again

        // Align the object's position and rotation precisely to the zone's snap point
        transform.position = zone.SnapPoint.position;
        transform.rotation = zone.SnapPoint.rotation;

        // Freeze physics entirely so it stays perfectly still
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        // Tell the zone that it is now full
        zone.AddItem();
    }
}
