using UnityEngine;
using System.Collections;

public class MasterDriver : MonoBehaviour {

    public static MasterDriver master;

    public static LayerMask regularCharacterMask;
    public static LayerMask draggedCharacterMask;

    public Texture2D cursorTexture;
    public Texture2D cursorDownTexture;

    public static bool isPaused = false;
    public float dragSpeed = 10.0f;
    public static LevelDriver levelDriver;

    public GameObject endLevelObject;

    private bool wasPaused = false;

    //Initialize things on the global scale.
    void Awake()
    {
        //Setup singleton here.
        if (master == null)
        {
            master = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(this.gameObject);
        }
        else if (master != this)
        {
            Destroy(this);
        }

        //Raycasts ignore the ignoreRaycast, select character, and non-draggable area layers.
        regularCharacterMask = ~0 & ~((1 << 2) | (1 << 10) | (1 << 11) | (1 << 12));

        //Raycasts ignore the ignoreRaycast, selectCharacters, and other characters.
        draggedCharacterMask = ~0 & ~((1 << 2) | (1 << 10) | (1 << 8));

        //Ignore collision between characters and the selected characters.
        Physics2D.IgnoreLayerCollision(8, 10, true);

        Cursor.SetCursor(cursorTexture, new Vector2(13, 13), CursorMode.Auto);
    }

    void Update()
    {
        if (!isPaused)
        {
            if (wasPaused || Input.GetMouseButtonDown(0))
            {
                Cursor.SetCursor(cursorDownTexture, new Vector2(13, 13), CursorMode.Auto);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Cursor.SetCursor(cursorTexture, new Vector2(13, 13), CursorMode.Auto);
            }
            wasPaused = false;
        }
        else if (!wasPaused)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            wasPaused = true;
        }
    }

}
