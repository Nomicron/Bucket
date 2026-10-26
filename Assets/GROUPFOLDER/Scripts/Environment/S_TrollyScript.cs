using UnityEngine;

public class S_TrollyScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 centerOfMass =
        new Vector3(0f, -0.5f, 0f);

    [SerializeField] WheelCollider leftFrontCollider, rightFrontCollider, leftBackCollider, rightBackCollider;
    [SerializeField] Transform leftFrontWheel, rightFrontWheel, leftBackWheel, rightBackWheel;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }

    private void UpdateWheel(
    WheelCollider wheelCollider,
    Transform wheelMesh)
    {
        wheelCollider.GetWorldPose(
            out Vector3 position,
            out Quaternion rotation
        );

        wheelMesh.position = position;
        wheelMesh.rotation = rotation;
    }

    void Update()
    {
        UpdateWheel(leftFrontCollider, leftFrontWheel);
        UpdateWheel(rightFrontCollider, rightFrontWheel);
        UpdateWheel(leftBackCollider, leftBackWheel);
        UpdateWheel(rightBackCollider, rightBackWheel);
    }
}
