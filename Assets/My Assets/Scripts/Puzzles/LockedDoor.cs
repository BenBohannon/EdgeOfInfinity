using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class LockedDoor : MonoBehaviour
{
    //Identifier must match corresponding Key
    public int Identifier;
    private SpriteRenderer myRenderer;
    private Collider2D myCollider;

    public bool isOpen = false;
    public Sprite openSprite;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();

        if (isOpen)
        {
            myRenderer.sprite = openSprite;
            myCollider.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Only allies can activate 
        if (coll.gameObject.tag == "Ally")
        {
            //Character is holding an item
            if (coll.gameObject.transform.childCount == 1)
            {
                //Check if item is correct key
                if (coll.gameObject.GetComponentInChildren<Key>().Identifier == this.Identifier)
                {
                    myRenderer.sprite = openSprite;
                    myCollider.enabled = false;
                    Destroy(coll.gameObject.transform.GetChild(0).gameObject);
                }
            }
        }
    }
}
