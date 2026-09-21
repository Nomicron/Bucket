using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class mopMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float maxHorizontalOffset = 0.5f;

    [SerializeField] private float liftSpeed = 1f;
    [SerializeField] private float maxVerticalOffset = 0.5f;



    [Header("Rotation")]
    [SerializeField] private float maxRotation = 20f;

    private Vector3 startLocalPosition;
    private Quaternion startLocalRotation;

    private float currentHorizontalOffset;
    private float currentVerticalOffset;

    private void Start()
    {
        startLocalPosition = transform.localPosition;
        startLocalRotation = transform.localRotation;
    }

    private void Update()
    {
        MopControls();
    }

    private void MopControls()
    {
        float horizontaInput = 0f;
        float verticalInput = 0f;

        // Z = Left, X = Right, C = Up, V = Down (Let's change this later!)

        if (Keyboard.current.zKey.isPressed)
            horizontaInput -= 1f;

        if (Keyboard.current.xKey.isPressed)
            horizontaInput += 1f;

        if(Keyboard.current.cKey.isPressed)
            verticalInput += 1f;

        if (Keyboard.current.vKey.isPressed)
            verticalInput -= 1f;

        //Horizontal movement
        currentHorizontalOffset += horizontaInput * moveSpeed * Time.deltaTime;
        currentHorizontalOffset = Mathf.Clamp(currentHorizontalOffset, -maxHorizontalOffset, maxHorizontalOffset);

        // Vertical movement
        currentVerticalOffset += verticalInput * liftSpeed * Time.deltaTime;
        currentVerticalOffset = Mathf.Clamp(currentVerticalOffset, -maxVerticalOffset, maxVerticalOffset);

        // Final transformation
        transform.localPosition = startLocalPosition + Vector3.right * currentHorizontalOffset + Vector3.up * currentVerticalOffset;

        float normalizedOffset = currentHorizontalOffset / maxHorizontalOffset;
        float angle =  -normalizedOffset * maxRotation;

        transform.localRotation = startLocalRotation * Quaternion.Euler(0f, 0f, angle);
    }
}
