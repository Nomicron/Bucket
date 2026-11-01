using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_CookerScript : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 3.0f;
    [SerializeField] RayController rayController;
    [SerializeField] Camera playerCamera;
    [SerializeField] private float rotationSpeed = 50.0f;
    [SerializeField] private float knobTimer = 2.0f;
    [SerializeField] GameObject cookPlate;

    private float timer = 0f;
    private bool isOn = false;
    private bool isRotating = false;
    private Ray ray;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Cooker();
                }
            }
        }
    }
    void Cooker()
    {
        if (isRotating)
            return;

        isOn = !isOn;

        ToggleCookPlate();

        StartCoroutine(RotateKnob());
    }

    IEnumerator RotateKnob()
    {
        isRotating = true;

        float timer = 0f;

        // Turning ON = negative Z
        // Turning OFF = positive Z
        float direction = isOn ? -1f : 1f;

        while (timer < knobTimer)
        {
            transform.Rotate( 0f, 0f, direction * rotationSpeed * Time.deltaTime);

            timer += Time.deltaTime;

            yield return null;
        }

        isRotating = false;
    }

    void ToggleCookPlate()
    {
        Transform[] children = cookPlate.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child.CompareTag("Lightsource"))
            {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }
    }
}
