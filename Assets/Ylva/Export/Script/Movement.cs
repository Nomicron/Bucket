using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody rigidbody;
    Vector3 movementInput;
    Vector3 movementVector;
    [SerializeField] float movementSpeed = 500;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }


    // Update is called once per frame
    void Update()
    {
        if (movementInput != Vector3.zero)
        {
            // Calculate movement based on the character's orientation
            movementVector =
                transform.right * movementInput.x +
                transform.forward * movementInput.z;

            // Ignore vertical movement
            movementVector.y = 0;

            // Apply velocity
            rigidbody.linearVelocity =
                movementVector * Time.fixedDeltaTime * movementSpeed;
        }
    }
    private void OnMovement(InputValue input) 
    {

        Vector2 inputVector = input.Get<Vector2>();

        movementInput = new Vector3(inputVector.x, 0, inputVector.y);

    }

    private void OnMovementStop(InputValue input)
    {
        // Reset movement when the movement keys are released
        movementInput = Vector3.zero;
        movementVector = Vector3.zero;
        rigidbody.linearVelocity = Vector3.zero;
    }

}
