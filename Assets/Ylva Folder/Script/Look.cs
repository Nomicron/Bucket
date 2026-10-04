using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Transform head;
    [SerializeField]float mouseSensitivity = 0.5f;
    Transform playerCamera;
    [SerializeField] Cleaning cleaning;
    float xRotation = 0f;
    float yRotation = 0f;
    float mouseX;
    float mouseY;

    // public getters (added by Bob)
    public float MouseX => mouseX;
    public float MouseY => mouseY;

    private float originalSens;
    public bool canLook = true;


    void Start()
    {
        originalSens = mouseSensitivity;
        canLook = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (PauseController.IsGamePaused)
        {
            mouseSensitivity = 0;
            return;
        }
        else if(!PauseController.IsGamePaused && !cleaning.cleaningMode)
        {
            mouseSensitivity = originalSens;
        }
        if (!canLook && !MopLookOverrideActive)
            return;

        float lookX = mouseX * mouseSensitivity * Time.deltaTime;
        float lookY = mouseY * mouseSensitivity * Time.deltaTime;

        // Look up/down with head only.
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        head.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Look left/right by rotating the whole player.
        transform.Rotate(Vector3.up * lookX);


    }

    private void OnLook(InputValue input)
    {


        Vector2 lookInput = input.Get<Vector2>();

        mouseX = lookInput.x;
        mouseY = lookInput.y;
    }

    public bool MopLookOverrideActive
    {
        get
        {
            return cleaning != null &&
                   cleaning.cleaningMode &&
                   Mouse.current.rightButton.isPressed;
        }
    }
}


