using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Crate : MonoBehaviour
{

    private Collider2D myCollider;
    protected Rigidbody2D myRigidbody;

    public virtual void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        myCollider = gameObject.GetComponent<Collider2D>();
    }
        
    public virtual void OnCollisionStay2D(Collision2D coll)
    {       
        CharacterMove character = coll.transform.GetComponent<CharacterMove>();
        if (character != null && myRigidbody.velocity.magnitude < .64)
        {
            character.reverseDirection();                       
        }        
    }    
}
