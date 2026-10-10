using UnityEngine;
using UnityEngine.InputSystem;

public class lightControlScript : ObjectiveMechanics
{
    [SerializeField] private float interactionDistance = 3.0f;
    [SerializeField] RayController rayController;
    [SerializeField] Camera playerCamera;

    Ray ray;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward
         );

            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    LightSwitch();
                }
            }
        }
    }
    void LightSwitch()
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child.CompareTag("Lightsource"))
            {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
                Finish();
            }

        }
    }
}
