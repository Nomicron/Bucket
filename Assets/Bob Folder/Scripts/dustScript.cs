using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class dustScript : MonoBehaviour
{
    [SerializeField] private VisualEffect dustEffect;
    [SerializeField] private float interactionDistance = 3.0f;
    [SerializeField] private float destroyDelay = 3.0f;

    private bool destroyed = false;

    void Start()
    {
        dustEffect.Stop();
    }

    void Update()
    {
        if (destroyed)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ViewportPointToRay(
                new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    ExplodeIntoDust();
                }
            }
        }
    }

    void ExplodeIntoDust()
    {
        destroyed = true;

        // Disable the object's visible mesh
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
            meshRenderer.enabled = false;

        // Disable collider so it can no longer be clicked
        Collider objectCollider = GetComponent<Collider>();

        if (objectCollider != null)
            objectCollider.enabled = false;

        // Restart and play the dust VFX
        dustEffect.Reinit();
        dustEffect.Play();

        // Remove everything after the dust has disappeared
        Destroy(gameObject, destroyDelay);
    }
}
