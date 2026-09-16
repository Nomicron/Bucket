using UnityEngine;
using UnityEngine.InputSystem;

public class mopMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MopControlls();
    }
    private void MopControlls()
    {
        float moveInput = 0f;

        if (Keyboard.current.qKey.IsPressed())
            moveInput = -1f;

        if (Keyboard.current.eKey.IsPressed())
            moveInput = 1f;

        transform.position += transform.right * moveInput * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, moveInput * rotationSpeed * 10f * Time.deltaTime);
    }
}
