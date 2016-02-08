using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ToggleButton : MonoBehaviour {

	private SpriteRenderer myRenderer;
    private Collider2D myCollider;

    public Sprite pressedSprite;
    public Sprite unpressedSprite;

    public Activatable[] connectedObjects;

    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myCollider = gameObject.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D coll) {
        //Only allies can activate 
        if (coll.gameObject.tag == "Ally") {
            foreach (Activatable a in connectedObjects)
            {
               a.Activate();
            }

            //Change sprite to the pressedSprite.
            myRenderer.sprite = pressedSprite;

            //Disable the collider, so this button can't be pressed again.
            // myCollider.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D coll) {

    	if (coll.gameObject.tag == "Ally") {
    		myRenderer.sprite = unpressedSprite;
    		myCollider.enabled = true;
    	}
    }
}
