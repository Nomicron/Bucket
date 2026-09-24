using UnityEngine;
using UnityEngine.InputSystem;

public class CursorScript : MonoBehaviour
{
    public Texture2D[] textures;
    public Vector2 hotspot = Vector2.zero;

    private float cursorSize = 32f;
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
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit)) 
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
                //Rag cursor
                currentTexture = textures[3];
                return;
            }
            else if (hit.collider.CompareTag("Stain")) 
            {
                //Brush cursor
                currentTexture = textures[4];
                return;
            }
            else if (hit.collider.CompareTag("Clue")) 
            {
                //Spyglass cursor
                currentTexture = textures[5];
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
            float x = (Screen.width / 2f) - (cursorSize / 2f) + hotspot.x;
            float y = (Screen.height / 2f) - (cursorSize / 2f) + hotspot.y;

            GUI.DrawTexture(new Rect(x, y, currentTexture.width, currentTexture.height), currentTexture);
        }
    }
}
