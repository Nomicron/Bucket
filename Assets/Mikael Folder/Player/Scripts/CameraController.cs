using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity = 2f;
    private float xRotaion = 0f;

    void Start()
    {
        //Locks the mouse in place and hides it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Located Mouse position and update
        Vector2 delta = Mouse.current.delta.ReadValue();
        float mouseX = delta.x * sensitivity;
        float mouseY = delta.y * sensitivity;
        //Locks movement so it does not go backwards
        xRotaion -= mouseY;
        xRotaion = Mathf.Clamp(xRotaion, -90f, 90f);
        //
        transform.localRotation = Quaternion.Euler(xRotaion, 0f, 0f);
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
