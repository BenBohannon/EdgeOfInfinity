using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Key : MonoBehaviour
{
    //Identifier must match corresponding Door
    public int Identifier;
    private static Vector2 position;
    private Collider2D myCollider;

    void Start()
    {
        //This will need tochange if character size is modified
        position = new Vector2(0, 1.3f);
        myCollider = this.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Only allies can activate and ally cannot be holding another object
        if (coll.gameObject.tag == "Ally" && coll.gameObject.transform.childCount == 0)
        {
            this.transform.parent = coll.gameObject.transform;
            this.transform.localPosition = position;
            myCollider.enabled = false;
        }
    }
}

