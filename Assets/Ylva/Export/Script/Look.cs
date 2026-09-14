using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]int mouseSensitivity = 100;
    Transform playerCamera;
    float xRotation = 0f;
    float yRotation = 0f;
    float mouseX;
    float mouseY;

    public bool canLook = true;


    void Start()
    {
        canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!canLook)
            return;

        float lookX = mouseX * mouseSensitivity * Time.deltaTime;
        float lookY = mouseY * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -35f, 40f);
        yRotation += mouseX;
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);


    }

    private void OnLook(InputValue input)
    {


        Vector2 lookInput = input.Get<Vector2>();

        mouseX = lookInput.x;
        mouseY = lookInput.y;
    }
}


