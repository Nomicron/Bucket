using UnityEngine;
using UnityEngine.InputSystem;

public class mopScript : MonoBehaviour
{


    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 2f;

    [Header("References")]
    [SerializeField] private mopPhysicsScript mopPhysics;

    private bool isWet = false;
    private float wetnessLevel = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mopPhysics == null)
            mopPhysics = GetComponent<mopPhysicsScript>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MopControlls();
    }
    public void HandleTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            // Mop entered water.
            isWet = true;
            wetnessLevel = 1f; // Set wetness level to maximum when entering water.

          //Increce the damping of the mop when it is wet.
            if (mopPhysics != null)
                mopPhysics.SetWet(true);

            Debug.Log("Mop is now wet. Wetness level: " + wetnessLevel);
        }
        else if (other.CompareTag("Stain"))
        {
            // Mop entered a stain.
        }
        else
            return;
    }
    public void HandleTriggerStay(Collider other)
    {
        if (other.CompareTag("Stain") && isWet)
        {

            // Cleaning logic here.
            // Clean continuously while touching stain.
            //Make it  so that the wetness level decreces.
        }
    }

    public void HandleTriggerExit(Collider other)
    {
        // Stop any continuous interaction if needed.
    }
    private void MopControlls()
    {
        float moveInput = 0f;

        if (Keyboard.current.qKey.IsPressed())
            moveInput = -1f;

        if (Keyboard.current.eKey.IsPressed())
            moveInput = 1f;

        transform.position += transform.right * moveInput * moveSpeed * Time.deltaTime;
        transform.Rotate (Vector3.forward, moveInput * rotationSpeed * 10f * Time.deltaTime);
    }
}
