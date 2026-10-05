using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpObject : ObjectiveMechanics
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

    Vector3 throwStartPosition;

    [SerializeField]
    bool destroyObject = false;
    bool isHolding = false;
    bool isPlaced = false;
    bool isThrown = false;

    float dist;

    TempParent tempParent;
    Rigidbody rb;
    Collider myCollider;
    Collider[] playerColliders;
    PlacementZone currentZone;

    // Added by Bob
    [SerializeField] private roomScript homeRoom;

    private Vector3 originalPos;
    private Quaternion originalRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        tempParent = TempParent.Instance;

        if (tempParent != null)
        {
            playerColliders = tempParent.GetComponentsInParent<Collider>();
        }

        originalPos = transform.position;
        originalRotation = transform.rotation;
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
                isThrown = true;
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
            dist = Vector3.Distance(transform.position, tempParent.transform.position);
            if (dist <= maxDist)
            {
                isHolding = true;
                isThrown = false;

                // Initialize holdDist to match current distance on pickup
                holdDist = Mathf.Clamp(dist, minHoldDist, maxHoldDist);

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
        dist = Vector3.Distance(transform.position, tempParent.transform.position);
        if (dist >= maxDist)
        {
            Drop();
            return;
        }

        Vector3 origin = tempParent.transform.position;
        Vector3 direction = tempParent.transform.forward;


        Vector3 castOrigin = origin + direction * objectRad;
        float maxCastDist = Mathf.Max(0.01f, holdDist - objectRad);

        float targetDist = holdDist;


        if (Physics.SphereCast(castOrigin, objectRad, direction, out RaycastHit hit, maxCastDist, obstacleLayers))
        {
            targetDist = objectRad + hit.distance;
        }

        Vector3 targetPosition = origin + direction * targetDist;


        Vector3 moveVelocity = (targetPosition - transform.position) * followSpeed;
        rb.linearVelocity = moveVelocity;
        rb.angularVelocity = Vector3.zero;

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

        zone.AddItem(isThrown, throwStartPosition, gameObject, destroyObject);
        Finish();   // Task
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

    //Added by Bob

    private void OnTriggerExit(Collider other)
    {
        roomScript room = other.GetComponent<roomScript>();

        if (room != null && room == homeRoom && !isPlaced)
        {
            RespawnObject();
        }
    }
  
    private void RespawnObject()
    {
        isHolding = false;

        TogglePlayerCollisions(false);

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = false;
        rb.useGravity = true;

        transform.position = originalPos;
        transform.rotation = originalRotation;

        Debug.Log(gameObject.name + " returned to " + homeRoom.roomName);
    }
}