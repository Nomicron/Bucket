using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable 
{
    void Interact();
    void OnHover();
    void OnHoverExit();
}
public class InteractionSystem : MonoBehaviour
{
    public Camera cam;
    public float distance = 3f;
    public Color hoverColor = Color.red;

    private IInteractable current;
    private Renderer r;
    private Color originalColor;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance) && hit.collider.TryGetComponent(out IInteractable i)) 
        {
            if(current != i)
            {
                Clear();
                current = i;
                current.OnHover();

                r = hit.collider.GetComponent<Renderer>();
                if (r) 
                {
                    originalColor = r.material.color;
                    r.material.color = hoverColor;
                }
                if (Keyboard.current.eKey.IsActuated())
                {
                    current.Interact();
                }
            }
            else 
            {
                Clear();
            }
        }
    }
    void Clear() 
    {
        if (current != null) 
        { 
            current.OnHoverExit();
        }
        if (r) 
        {
            r.material.color = originalColor;
        }
        current = null;
        r = null;
    }
}
