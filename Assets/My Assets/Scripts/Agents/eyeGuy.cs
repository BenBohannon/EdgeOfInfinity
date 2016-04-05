using UnityEngine;
using System.Collections;

/** 
 * Eye Guys shoot projectiles to kill the player's characters.  Flamels are capable of killing Eye Guys.
 */
public class eyeGuy : CharacterMove {
    public float sight;

	// Use this for initialization
	override public void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	override public void Update () {
        Vector3 pos = transform.position + (isMovingRight ? new Vector3(1f, 0, 0) : new Vector3(-1f, 0, 0));

        //If we see an "Ally"
        RaycastHit2D hit = Physics2D.Raycast(pos, new Vector2(isMovingRight ? 0.2f : -0.2f, 0), sight, MasterDriver.regularCharacterMask);
        if (hit.collider != null) {
            if (hit.transform.tag == "Ally")
            {
                CharacterMove character = hit.transform.GetComponent<CharacterMove>();
                character.die();
            }

        }

        base.Update();
	}

    /**
     * If this enemy hits a flamel, it dies.
     */
    override public void OnCollisionEnter2D(Collision2D coll)
    {
        Flamel character = coll.transform.GetComponent<Flamel>();
        if (character != null)
        {
            die();
        }
    }
}
