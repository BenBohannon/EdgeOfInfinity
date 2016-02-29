using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class ToggleLaser : Activatable {

    private SpriteRenderer myRenderer;
    private Collider2D myCollider;

    public bool isOn = true;
    public Sprite offSprite;
    private Sprite onSprite;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();

        onSprite = myRenderer.sprite;

        if (isOn)
        {
            myRenderer.sprite = onSprite;
            myCollider.enabled = true;
        }
        else
        {
            myRenderer.sprite = offSprite;
            myCollider.enabled = false;
        }
    }

    override public void Activate()
    {
        //If laser is on.
        if (isOn)
        {
            myRenderer.sprite = offSprite;
            myCollider.enabled = false;
        }
        //Else, the laser is off.
        else
        {
            myRenderer.sprite = onSprite;
            myCollider.enabled = true;
        }

        isOn = !isOn;
    }

    public virtual void OnCollisionEnter2D(Collision2D coll)
    {
        // If a character hits this, kill it
        CharacterMove character = coll.transform.GetComponent<CharacterMove>();
        if (character != null)
        {
            character.die();
        }
    }
}