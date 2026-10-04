using UnityEngine;
using UnityEngine.InputSystem;


public class mopScript : MonoBehaviour
{


    [Header("References")]
    [SerializeField] private mopPhysicsScript mopPhysics;

    public bool isWet = false;
    public float wetnessLevel = 0f;

    [SerializeField] private Renderer mopHeadRenderer;
    [SerializeField] private Gradient wetnessGradient;

    //Added by Ylva 
    [SerializeField] Cleaning cleaning;
    [SerializeField] Transform mopHead;
    [SerializeField] float dryingSpeed = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mopPhysics == null)
            mopPhysics = GetComponent<mopPhysicsScript>();

        UpdateMopColor();
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

            UpdateMopColor();

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
        //Added by Ylva 
        if (!cleaning.hasMop)
            return;

        MopPainter surface = other.GetComponent<MopPainter>();

        if (surface == null)
            return;

        // Start slightly above the mop head and raycast downward.
        Vector3 rayOrigin = mopHead.position + Vector3.up * 0.1f;

        Ray ray = new Ray(rayOrigin, Vector3.down);

        if (other.Raycast(ray, out RaycastHit hit, 0.3f))
        {
            surface.CleanAtUV(hit.textureCoord, hit.point);
        }

    //if (other.CompareTag("Stain") && isWet)
    //{

    //    // Cleaning logic here.
    //    // Clean continuously while touching stain.
    //    //Make it  so that the wetness level decreces.
    //}
    }

    public void HandleTriggerExit(Collider other)
    {
        // Stop any continuous interaction if needed.
    }

    public void MopUseWater()
    {
        wetnessLevel -= dryingSpeed * Time.deltaTime;

        wetnessLevel = Mathf.Clamp01(wetnessLevel);

        UpdateMopColor();
        if (wetnessLevel <= 0f)
        {
            wetnessLevel = 0f;
            isWet = false;
            Debug.Log("Mop is Dry");
        }
    }

    private void UpdateMopColor()
    {
        Color color = wetnessGradient.Evaluate(wetnessLevel);

        mopHeadRenderer.material.color = color;
    }

}
