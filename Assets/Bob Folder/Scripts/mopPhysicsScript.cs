using UnityEngine;

public class mopPhysicsScript : MonoBehaviour
{
    [Header("Bones in order")]
    [SerializeField] private Transform[] bones; // Array of bones in order from root to tip.

    [Header("Physics")]
    [SerializeField] private float gravity = 9.81f;

    [Range(0f, 1f)]
    [SerializeField] private float dryDamping = 0.05f;  // Mop will move faster when dry, less damping.
    [SerializeField] private float wetDamping = 0.15f;

    [SerializeField] private int constraintIterations = 5;

    [Header("Movement")]
    [SerializeField] private float maxStretchCorrection = 1f; // How much to correct the length of the segments each iteration. 1 = full correction, 0 = no correction.

    [Header("Rotation Limits")]
    [Range(0f, 180f)]
    [SerializeField] private float maxBendAngle = 45f;  // Maximum angle in degrees that a segment can bend relative to the previous segment.

    [Header("Collision")]
    [SerializeField] private LayerMask collisionMask;   // Layers that the mop can collide with.

    [SerializeField] private float collisionRadius = 0.025f; // Radius of the sphere used for collision detection for each bone.

   // [SerializeField] private int collisionIterations = 2;   // Number of iterations to resolve collisions each frame.

    private Vector3[] positions;    
    private Vector3[] previousPositions;

    private float[] segmentLengths;

    private Vector3[] restDirectionsInParent;   // Rest direction of each segment in local space of the bone.
    private Quaternion[] restRotations; // Rest rotation of each bone in local space of the bone.

    private bool initialized;

    private float damping;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {

        if (bones == null || bones.Length < 2)
        {
            Debug.LogError("MopChainPhysics needs at least 2 bones.");
            return;
        }

        int count = bones.Length;

        positions = new Vector3[count];
        previousPositions = new Vector3[count];

        segmentLengths = new float[count - 1];

        restDirectionsInParent = new Vector3[count - 1];
        restRotations = new Quaternion[count - 1];

        for (int i = 0; i < count; i++)
        {
            positions[i] = bones[i].position;
            previousPositions[i] = bones[i].position;
        }

        for (int i = 0; i < count - 1; i++)
        {
            Vector3 difference =
                bones[i + 1].position - bones[i].position;

            segmentLengths[i] = difference.magnitude;

            restRotations[i] = bones[i].localRotation;

            if (bones[i].parent != null)
            {
                restDirectionsInParent[i] =
                    bones[i].parent.InverseTransformDirection(
                        difference.normalized
                    );
            }
            else
            {
                restDirectionsInParent[i] =
                    difference.normalized;
            }
        }

        damping = dryDamping;

        initialized = true;
    }

    private void LateUpdate()
    {
        if (!initialized)
            return;

        float dt = Time.deltaTime;

        if (dt <= 0f)
            return;

        Simulate(dt);

        ApplyToBones();
    }

    private void Simulate(float dt)
    {
        // Bone 0 is attached to the mop itself.
        positions[0] = bones[0].position;

        // Verlet integration for all hanging bones.
        for (int i = 1; i < positions.Length; i++)
        {
            Vector3 velocity = positions[i] - previousPositions[i];

            previousPositions[i] = positions[i];

            velocity *= 1f - damping;

            positions[i] += velocity;

            positions[i] += Vector3.down * gravity * dt * dt;
        }

        // Keep every link at its original length.
        for (int iteration = 0; iteration < constraintIterations; iteration++)
        {
            positions[0] = bones[0].position;

            for (int i = 0; i < positions.Length - 1; i++)
            {
                Vector3 difference = positions[i + 1] - positions[i];

                float distance = difference.magnitude;

                if (distance < 0.000001f)
                    continue;

                float error = distance - segmentLengths[i];

                Vector3 correction = difference.normalized * error;

                correction *= maxStretchCorrection;

                if (i == 0)
                {
                    // Root cannot move.
                    positions[i + 1] -= correction;
                }
                else
                {
                    // Split correction between both points.
                    positions[i] += correction * 0.5f;
                    positions[i + 1] -= correction * 0.5f;
                }
            }
            // Prevent extreme folding.
            ApplyBendConstraints();

            ResolveCollisions();
        }
    }

    private void ApplyToBones()
    {
        for (int i = 0; i < bones.Length - 1; i++)
        {
            Vector3 desiredDirection =
                positions[i + 1] - positions[i];

            if (desiredDirection.sqrMagnitude < 0.000001f)
                continue;

            desiredDirection.Normalize();

            Vector3 desiredDirectionInParent;

            if (bones[i].parent != null)
            {
                desiredDirectionInParent =
                    bones[i].parent.InverseTransformDirection(
                        desiredDirection
                    );
            }
            else
            {
                desiredDirectionInParent =
                    desiredDirection;
            }

            Quaternion swingRotation =
                Quaternion.FromToRotation(
                    restDirectionsInParent[i],
                    desiredDirectionInParent
                );

            bones[i].localRotation =
                swingRotation * restRotations[i];
        }
    }

    private void ApplyBendConstraints()
    {
        float maxRadians = maxBendAngle * Mathf.Deg2Rad;

        // Start at 1 because we need a segment before and after the current point.
        for (int i = 1; i < positions.Length - 1; i++)
        {
            Vector3 previousDirection = positions[i] - positions[i - 1];
            Vector3 nextDirection = positions[i + 1] - positions[i];

            if (previousDirection.sqrMagnitude < 0.000001f ||
                nextDirection.sqrMagnitude < 0.000001f)
                continue;

            previousDirection.Normalize();
            nextDirection.Normalize();

            // Limit how far the next segment may rotate away
            // from the previous segment.
            Vector3 limitedDirection = Vector3.RotateTowards(
                previousDirection,
                nextDirection,
                maxRadians,
                0f
            );

            // Put the next point back at the correct segment length.
            positions[i + 1] =
                positions[i] +
                limitedDirection * segmentLengths[i];
        }
    }

    private void ResolveCollisions()
    {
        float skinWidth = 0.002f;

        for (int i = 1; i < positions.Length; i++)
        {
            Vector3 movement =
                positions[i] - previousPositions[i];

            float movementDistance =
                movement.magnitude;

            // Sweep from previous position to current position.
            if (movementDistance > 0.000001f)
            {
                Vector3 direction =
                    movement / movementDistance;

                if (Physics.SphereCast(
                    previousPositions[i],
                    collisionRadius,
                    direction,
                    out RaycastHit hit,
                    movementDistance,
                    collisionMask,
                    QueryTriggerInteraction.Ignore))
                {
                    positions[i] =
                        hit.point +
                        hit.normal *
                        (collisionRadius + skinWidth);

                    // Remove velocity pointing into the surface.
                    Vector3 velocity =
                        positions[i] -
                        previousPositions[i];

                    float intoSurface =
                        Vector3.Dot(
                            velocity,
                            hit.normal
                        );

                    if (intoSurface < 0f)
                    {
                        velocity -=
                            hit.normal *
                            intoSurface;

                        previousPositions[i] =
                            positions[i] -
                            velocity;
                    }
                }
            }

            // Extra penetration correction for primitive
            // and convex colliders.
            Collider[] hits =
                Physics.OverlapSphere(
                    positions[i],
                    collisionRadius,
                    collisionMask,
                    QueryTriggerInteraction.Ignore
                );

            foreach (Collider col in hits)
            {
                // A hollow/non-convex MeshCollider cannot
                // use Collider.ClosestPoint().
                if (col is MeshCollider meshCollider &&
                    !meshCollider.convex)
                {
                    continue;
                }

                Vector3 closest =
                    col.ClosestPoint(positions[i]);

                Vector3 offset =
                    positions[i] - closest;

                float distance =
                    offset.magnitude;

                if (distance > 0.000001f &&
                    distance < collisionRadius)
                {
                    Vector3 normal =
                        offset / distance;

                    positions[i] +=
                        normal *
                        (collisionRadius - distance);
                }
            }
        }
    }
    public void SetWet(bool wet)
    {
        damping = wet ? wetDamping : dryDamping;
    }
}
