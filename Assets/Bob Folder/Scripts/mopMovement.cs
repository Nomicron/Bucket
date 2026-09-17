using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class mopMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float maxOffset = 0.5f;

    [Header("Rotation")]
    [SerializeField] private float maxRotation = 20f;

    private Vector3 startLocalPosition;
    private Quaternion startLocalRotation;

    private float currentOffset;

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
        float input = 0f;

        if (Keyboard.current.zKey.isPressed)
            input -= 1f;

        if (Keyboard.current.xKey.isPressed)
            input += 1f;

        currentOffset += input * moveSpeed * Time.deltaTime;

        currentOffset = Mathf.Clamp(currentOffset, -maxOffset, maxOffset);

        transform.localPosition = startLocalPosition + Vector3.right * currentOffset;

        float normalizedOffset = currentOffset / maxOffset;

        float angle =  -normalizedOffset * maxRotation;

        transform.localRotation =
            startLocalRotation *
            Quaternion.Euler(0f, 0f, angle);
    }
}
