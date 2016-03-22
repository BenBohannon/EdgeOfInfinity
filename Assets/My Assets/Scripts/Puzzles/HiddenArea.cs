using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class HiddenArea : MonoBehaviour {

    private SpriteRenderer myRenderer;
    private Collider2D myCollider;

    private int numChar = 0;

    void Start () {
        myRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();
    }
	
	void Update () {
	
	}

    public virtual void OnCollisionEnter2D(Collision2D coll)
    {
        // If a character enters the area, decrease alpha if it is only the first character
        CharacterMove character = coll.transform.GetComponent<CharacterMove>();
        if (character != null)
        {
            numChar++;
            if (numChar == 1)
            {
                myRenderer.color = new Color(1f, 1f, 1f, .5f);
            }
        }
    }

    public virtual void OnCollisionExit2D(Collision2D coll)
    {
        // Make opaque if all characters have left
        CharacterMove character = coll.transform.GetComponent<CharacterMove>();
        if (character != null)
        {
            numChar--;
            if (numChar == 0)
            {
                myRenderer.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }
}
