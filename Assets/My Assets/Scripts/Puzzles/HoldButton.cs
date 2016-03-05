using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class HoldButton : MonoBehaviour {

    private SpriteRenderer myRenderer;
    private Collider2D myCollider;

    public Sprite pressedSprite;
    public Sprite unpressedSprite;
    private bool isPressed = false;

    public Activatable[] connectedObjects;

    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myCollider = gameObject.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //If this player is being dragged, don't try and make it stop...
        if (coll.gameObject.layer == 10)
        {
            return;
        }

        //Only allies or crates can activate 
        if (!isPressed && coll.gameObject.tag == "Ally"
                || coll.gameObject.tag == "Crate"
                || coll.gameObject.tag == "Heavy Crate")
        {
            foreach (Activatable a in connectedObjects)
            {
                a.Activate();
            }

            //Make the character stop walking.
            CharacterMove character = coll.gameObject.GetComponent<CharacterMove>();
            if (character != null)
            {
                character.autoWalk = false;
            }

            //Change sprite to the pressedSprite.
            myRenderer.sprite = pressedSprite;

            isPressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (isPressed && coll.gameObject.tag == "Ally"
                || coll.gameObject.tag == "Crate"
                || coll.gameObject.tag == "Heavy Crate")
        {            
            foreach (Activatable a in connectedObjects)
            {
                a.Activate();
            }
            myRenderer.sprite = unpressedSprite;
            myCollider.enabled = true;
            isPressed = false;

            //Make the character start walking.                       
            // CharacterMove character = coll.gameObject.GetComponent<CharacterMove>();
            // character.autoWalk = true;            
        }
    }
}