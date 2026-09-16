using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class mopMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 2f;

    private Vector3 startLocalPosition;
    private Quaternion startLocalRotation;

    private float currentOffset = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Store the mop's original position/rotation relative to the player.
        startLocalPosition = transform.localPosition;
        startLocalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        MopControlls();
    }
    private void MopControlls()
    {
        float moveInput = 0f;

        if (Keyboard.current.zKey.IsPressed())
            moveInput = -1f;

        if (Keyboard.current.xKey.IsPressed())
            moveInput = 1f;
        //currentOffset += moveInput * moveSpeed * Time.deltaTime;

        transform.localPosition = startLocalPosition + transform.right * moveInput * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, moveInput * rotationSpeed * 10f * Time.deltaTime);
    }
}
