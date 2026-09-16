using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]float mouseSensitivity = 0.5f;
    Transform playerCamera;
    float xRotation = 0f;
    float yRotation = 0f;
    float mouseX;
    float mouseY;

    private float originalSens;
    public bool canLook = true;


    void Start()
    {
        originalSens = mouseSensitivity;
        canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (PauseController.IsGamePaused)
        {
            mouseSensitivity = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mouseSensitivity = originalSens;
        }
        if (!canLook)
            return;

        float lookX = mouseX * mouseSensitivity * Time.deltaTime;
        float lookY = mouseY * mouseSensitivity * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);
        yRotation += lookX;
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);


    }

    private void OnLook(InputValue input)
    {


        Vector2 lookInput = input.Get<Vector2>();

        mouseX = lookInput.x;
        mouseY = lookInput.y;
    }
}


