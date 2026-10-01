using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class mopMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Look look;


    [Header("Movement")]
    [SerializeField] private float xSensitivity = 0.001f;
    [SerializeField] private float zSensitivity = 0.001f;

    [SerializeField] private float maxXOffset = 2.5f;
    [SerializeField] private float maxZOffset = 1f;

    [SerializeField] private float liftSpeed = 0.002f;
    [SerializeField] private float maxYOffset = 0.5f;


    [Header("Rotation")]
    [SerializeField] private float maxRotation = 20f;

    private Vector3 startLocalPosition;
    private Quaternion startLocalRotation;

    private float currentXOffset;
    private float currentYOffset;
    private float currentZOffset;

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
        //float horizontaInput = 0f;
        //float verticalInput = 0f;

        //// Z = Left, X = Right, C = Up, V = Down (Let's change this later!)

        //if (Keyboard.current.zKey.isPressed)
        //    horizontaInput -= 1f;

        //if (Keyboard.current.xKey.isPressed)
        //    horizontaInput += 1f;

        //if(Keyboard.current.cKey.isPressed)
        //    verticalInput += 1f;

        //if (Keyboard.current.vKey.isPressed)
        //    verticalInput -= 1f;

        ////Horizontal movement
        //currentHorizontalOffset += horizontaInput * moveSpeed * Time.deltaTime;
        //currentHorizontalOffset = Mathf.Clamp(currentHorizontalOffset, -maxHorizontalOffset, maxHorizontalOffset);

        //// Vertical movement
        //currentVerticalOffset += verticalInput * liftSpeed * Time.deltaTime;
        //currentVerticalOffset = Mathf.Clamp(currentVerticalOffset, -maxVerticalOffset, maxVerticalOffset);

        //// Final transformation
        //transform.localPosition = startLocalPosition + Vector3.right * currentHorizontalOffset + Vector3.up * currentVerticalOffset;

        //float normalizedOffset = currentHorizontalOffset / maxHorizontalOffset;
        //float angle =  -normalizedOffset * maxRotation;

        //transform.localRotation = startLocalRotation * Quaternion.Euler(0f, 0f, angle);
        if (look == null)
            return;

        // Middle mouse is used for looking around.
        if (Mouse.current.middleButton.isPressed)
            return;

        float mouseX = look.MouseX;
        float mouseY = look.MouseY;

        // Mouse movement controls X and Z.
        currentXOffset += mouseX * xSensitivity;
        currentZOffset += mouseY * zSensitivity;

        // Mouse wheel controls Y.
        float scroll = Mouse.current.scroll.ReadValue().y;
        currentYOffset += scroll * liftSpeed;

        // Clamp movement.
        currentXOffset = Mathf.Clamp(
            currentXOffset,
            -maxXOffset,
            maxXOffset
        );

        currentYOffset = Mathf.Clamp(
            currentYOffset,
            -maxYOffset,
            maxYOffset
        );

        currentZOffset = Mathf.Clamp(
            currentZOffset,
            -maxZOffset,
            maxZOffset
        );

        // Apply final position.
        transform.localPosition =
            startLocalPosition +
            Vector3.right * currentXOffset +
            Vector3.up * currentYOffset +
            Vector3.forward * currentZOffset;

        // Rotate mop based on horizontal X movement.
        float normalizedHorizontal =
            currentXOffset / maxXOffset;

        float angle =
            -normalizedHorizontal * maxRotation;

        transform.localRotation =
            startLocalRotation *
            Quaternion.Euler(0f, 0f, angle);
    }
}
