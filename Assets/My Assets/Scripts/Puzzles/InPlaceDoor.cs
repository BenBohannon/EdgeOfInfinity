using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class InPlaceDoor : Activatable {

    private SpriteRenderer myRenderer;
    private Collider2D myCollider;

    public bool isOpen = false;
    public Sprite openSprite;
    private Sprite closedSprite;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();

        closedSprite = myRenderer.sprite;

        if (isOpen)
        {
            myRenderer.sprite = openSprite;
            myCollider.enabled = false;
        }
    }

    override public void Activate()
    {
        //If we're closing.
        if (isOpen)
        {
            myRenderer.sprite = closedSprite;
            myCollider.enabled = true;
        }
        //Else, we're opening.
        else
        {
            myRenderer.sprite = openSprite;
            myCollider.enabled = false;
        }

        isOpen = !isOpen;
    }
}
