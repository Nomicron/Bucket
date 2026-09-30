using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CursorScript : MonoBehaviour
{
    public Texture2D[] textures;
    public Vector2 hotspot = Vector2.zero;
    [SerializeField] public RayController rayController;

    Stain hitStain;
    bool needsRag = false;

    //private float cursorSize = 32f;
    private Texture2D currentTexture = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        LockCursor();
        ChangeCursor();
    }

    public void ChangeCursor() 
    {
        if (rayController.TryGetHit(out RaycastHit hit)) 
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                //Hand open cursor
                currentTexture = textures[1];

                if (Mouse.current.leftButton.isPressed) 
                {
                    //Hand closed cursor
                    currentTexture = textures[2];
                }

                return;
            }
            if (hit.collider.CompareTag("Openable")) 
            {
                //Hand pointing cursor
                currentTexture = textures[0];

                if (Mouse.current.leftButton.isPressed)
                {
                    //Hand closed cursor
                    currentTexture = textures[2];
                }

                return;
            }
            else if (hit.collider.CompareTag("Dirt"))
            {
                //Brush cursor
                currentTexture = textures[3];
                return;
            }
            else if (hit.collider.CompareTag("Clue"))
            {
                //Spyglass cursor
                currentTexture = textures[4];
                return;
            }
            else if (hit.collider.CompareTag("Stain")) 
            {
                hitStain = hit.collider.GetComponent<Stain>();

                if (hitStain != null) 
                {
                    if (!CleaningModeController.InCleaningMode) 
                    {
                        currentTexture = textures[5];
                    }
                    else if (hitStain.stainType == StainType.Blood) 
                    {
                        currentTexture = textures[6 + hitStain.currentStage];
                    }
                    else 
                    {
                        currentTexture = null;
                    }
                }

                return;
            }
        }

        currentTexture = null;
    }

    public void LockCursor() 
    {
        if (PauseController.IsGamePaused) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            currentTexture = textures[0];
        }
        else if (CleaningModeController.InCleaningMode && currentTexture != null) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void OnGUI() 
    {
        if (currentTexture != null && !PauseController.IsGamePaused) 
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            float x = mousePos.x - (currentTexture.width / 2f) + hotspot.x;
            //uses Screen.height since Unity input system uses bottom left as (0,0) while OnGUI uses top left as (0,0)
            float y = Screen.height - mousePos.y - (currentTexture.height / 2f) + hotspot.y;

            GUI.DrawTexture(new Rect(x, y, currentTexture.width, currentTexture.height), currentTexture);
        }
    }
}
