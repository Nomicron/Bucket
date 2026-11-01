using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_Closet : MonoBehaviour
{
    public RayController rayController;
    public Evidence currentEvidence;
    public Evidence[] evidences;
    private int count = 0;

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
                if (Mouse.current.leftButton.isPressed)
                {
                    currentEvidence = evidences[0 + count];
                    count++;

                    gameObject.IsDestroyed();
                }
            }
        }
    }

    void PlaceEvidenceInCloset() 
    {
        foreach (Evidence evidence in evidences) 
        {
        }
    }
}
