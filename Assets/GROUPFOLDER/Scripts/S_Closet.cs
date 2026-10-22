using UnityEngine;

public class S_Closet : MonoBehaviour
{
    public RayController rayController;
    public Evidence[] evidences;
    private Evidence currentEvidence; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PickUpEvidence()
    {
        if (rayController.TryGetHit(out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Evidence")) 
            {
            }
        }
    }
}
