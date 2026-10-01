using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpObject : MonoBehaviour
{
    public string itemID;

    [SerializeField] 
    float throwForce = 600f;
    [SerializeField] 
    float maxDist = 3f;
    [SerializeField] 
    float snapRad = 0.5f;
    [SerializeField] 
    float holdDist = 2f;
    [SerializeField] 
    float objectRad = 0.25f; // Radius of the object to prevent clipping edges
    [SerializeField] 
    float followSpeed = 20f;    // Speed at which object follows the hold point

    [SerializeField]
    float minHoldDist = 1f;
    [SerializeField]
    float maxHoldDist = 2.5f;
    [SerializeField]
    float scrollSense = 0.25f;

    [SerializeField] 
    LayerMask zoneLayer;
    [SerializeField] 
    LayerMask obstacleLayers;

    [SerializeField]
    Vector3 throwStartPosition;

    bool isHolding = false;
    bool isPlaced = false;
    bool isThown = false;
    float distance;

    TempParent tempParent;
    Rigidbody rb;
    Collider myCollider;
    Collider[] playerColliders;
    PlacementZone currentZone;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        tempParent = TempParent.Instance;
        playerColliders = tempParent.GetComponentsInParent<Collider>();
    }

    void Update()
    {
        if (isHolding)
        {
            float scrollD = Mouse.current.scroll.ReadValue().y;
            if (Mathf.Abs(scrollD) > 0.01f)
            {
                holdDist += Mathf.Sign(scrollD) * scrollSense;
                holdDist = Mathf.Clamp(holdDist, minHoldDist, maxHoldDist);
            }

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                isThown = true;
                throwStartPosition = transform.position;
                rb.AddForce(tempParent.transform.forward * throwForce);
                Drop();
            }
        }
    }

    void FixedUpdate()
    {
        if (isHolding)
        {
            Hold();
        }
    }

    private void OnMouseDown()
    {
        if (isPlaced) 
        {
            return; 
        }
            distance = Vector3.Distance(transform.position, tempParent.transform.position);
            if (distance <= maxDist)
            {
                isHolding = true;

                // Initialize holdDist to match current distance on pickup
                holdDist = Mathf.Clamp(distance, minHoldDist, maxHoldDist);

                rb.useGravity = false;
                rb.detectCollisions = true;
                rb.isKinematic = false;

                TogglePlayerCollisions(ignore: true);
            }
    }

    private void OnMouseUp()
    {
        Drop();
    }

    private void Hold()
    {
        distance = Vector3.Distance(transform.position, tempParent.transform.position);
        if (distance >= maxDist)
        {
            Drop();
            return;
        }

        Vector3 origin = tempParent.transform.position;
        Vector3 direction = tempParent.transform.forward;

        // Offset cast origin forward so it starts outside player's body collider
        Vector3 castOrigin = origin + direction * objectRad;
        float maxCastDist = Mathf.Max(0.01f, holdDist - objectRad);

        float targetDist = holdDist;

        // Cast sphere only against Environment layers
        if (Physics.SphereCast(castOrigin, objectRad, direction, out RaycastHit hit, maxCastDist, obstacleLayers))
        {
            targetDist = objectRad + hit.distance;
        }

        Vector3 targetPosition = origin + direction * targetDist;

        // Smoothly push item toward target position without clipping inside walls/player
        Vector3 moveVelocity = (targetPosition - transform.position) * followSpeed;
        rb.linearVelocity = moveVelocity;
        rb.angularVelocity = Vector3.zero;

        // Check for placement zones
        Collider[] hits = Physics.OverlapSphere(transform.position, snapRad, zoneLayer);
        foreach (Collider hitCollider in hits)
        {
            PlacementZone zone = hitCollider.GetComponent<PlacementZone>();
            if (zone != null && zone.AcceptsItem(itemID))
            {
                isHolding = false;
                SnapToZone(zone);
                return;
            }
        }
    }

    private void Drop()
    {
        if (isHolding)
        {
            isHolding = false;

            if (currentZone != null)
            {
                SnapToZone(currentZone);
            }
            else
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
    }

    private void TogglePlayerCollisions(bool ignore)
    {
        if (myCollider == null) 
        {
            return; 
        }

        // Refresh player colliders if not cached yet
        if (playerColliders == null || playerColliders.Length == 0)
        {
            if (tempParent != null) 
            {
                playerColliders = tempParent.GetComponentsInParent<Collider>();
            }

        }

        if (playerColliders != null)
        {
            foreach (Collider pCol in playerColliders)
            {
                if (pCol != null)
                {
                    Physics.IgnoreCollision(myCollider, pCol, ignore);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlaced || isHolding)
        {
            return;
        }

        PlacementZone zone = other.GetComponent<PlacementZone>();
        if (zone != null && zone.AcceptsItem(itemID))
        {
            SnapToZone(zone);
        }
    }

    private void SnapToZone(PlacementZone zone)
    {
        isPlaced = true;
        TogglePlayerCollisions(ignore: false);
        transform.position = zone.SnapPoint.position;
        transform.rotation = zone.SnapPoint.rotation;

        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        float throwDist = 0;
        if (isThown) 
        {
            throwDist = Vector3.Distance(throwStartPosition, transform.position);
        }
        zone.AddItem(isThown, throwDist);
    }
}