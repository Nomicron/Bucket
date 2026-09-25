using UnityEngine;
using UnityEngine.InputSystem;

public class RayController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] public Camera playerCamera;
    [SerializeField] public float rayRange = 2f;

    public bool TryGetHit(out RaycastHit hit)
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        return Physics.Raycast(ray, out hit, rayRange);
    }

    public bool TryGetComponent<T>(out T component) where T : Component
    {
        if (TryGetHit(out RaycastHit hit))
        {
            component = hit.collider.GetComponent<T>();
            return component != null;
        }
        component = null;
        return false;
    }
}
