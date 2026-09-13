using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 4.0f;


    private Rigidbody rb;
    private Vector2 moveInput;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(
         Keyboard.current.dKey.isPressed ? 1 : Keyboard.current.aKey.isPressed ? -1 : 0,
         Keyboard.current.wKey.isPressed ? 1 : Keyboard.current.sKey.isPressed ? -1 : 0
     );
    }

    private void FixedUpdate()
    {
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        //Vector3 move = (transform.forward * vertical + transform.right * horizontal) * moveSpeed; 
        Vector3 move = (transform.forward * moveInput.y + transform.right * moveInput.x) * moveSpeed; 
        Vector3 newVelocity = new Vector3(move.x, rb.linearVelocity.y , move.z);
        rb.linearVelocity = newVelocity;


    }

}
