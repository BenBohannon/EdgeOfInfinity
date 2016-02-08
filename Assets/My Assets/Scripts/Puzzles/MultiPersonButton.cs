using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MultiPersonButton : MonoBehaviour {
    
    private SpriteRenderer myRenderer;
    private Collider2D myCollider;

    public Sprite pressedSprite;
    public bool isPressed;

    public Activatable[] connectedObjects;
    public MultiPersonButton[] connectedButtons;

    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myCollider = gameObject.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Only allies can activate 
        if (coll.gameObject.tag == "Ally")
        {
            isPressed = true;

            bool allPressed = true;
            // check all the other buttons
            foreach (MultiPersonButton m in connectedButtons)
            {
                allPressed = allPressed && m.isPressed;
            }

            // if all are pressed, allPressed is true
            if(allPressed)
            {
                foreach (Activatable a in connectedObjects)
                {
                    a.Activate();
                }
            }
            //Change sprite to the pressedSprite.
            myRenderer.sprite = pressedSprite;

            //Disable the collider, so this button can't be pressed again.
            myCollider.enabled = false;
        }
    }
}
