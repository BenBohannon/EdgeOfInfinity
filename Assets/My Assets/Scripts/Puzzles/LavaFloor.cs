using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class LavaFloor : MonoBehaviour {

    private Collider2D myCollider;
    
    void Start () {
        myCollider = gameObject.GetComponent<Collider2D>();
    }
    
    void Update () {
	
	}

    public virtual void OnCollisionEnter2D(Collision2D coll)
    {
        // If a character hits this, kill it
        CharacterMove character = coll.transform.GetComponent<CharacterMove>();
<<<<<<< HEAD
        if (character != null && coll.transform.childCount == 0)
=======
        Flamel flame = coll.transform.GetComponent<Flamel>();
        if (character != null && flame == null)
>>>>>>> origin/master
        {
            character.die();
        }
    }
}
