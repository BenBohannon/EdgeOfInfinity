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
        DraggableCharacter character = coll.transform.GetComponent<DraggableCharacter>();
        if (character != null && myRigidbody.velocity.magnitude < .64
                && System.Math.Abs(character.transform.position.y - this.transform.position.y) < 1.5)                
        {
            character.reverseDirection();            
        }
        if (coll.transform.tag == "Crate" || coll.transform.tag == "Heavy Crate")
        {
            myRigidbody.velocity = Vector2.zero;
        }
    }    
}
