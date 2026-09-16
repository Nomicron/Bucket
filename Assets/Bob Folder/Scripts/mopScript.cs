using UnityEngine;
using UnityEngine.InputSystem;

public class mopScript : MonoBehaviour
{


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
    
}
