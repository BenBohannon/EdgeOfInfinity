using UnityEngine;
using System.Collections;

public class EndElevator : MonoBehaviour {

    private Animator myAnimator;
    private Animator doorAnimator;
    private Collider2D myCollider;

    private bool isTakingCharacter = false;

	// Use this for initialization
	void Start () {
        doorAnimator = transform.GetChild(0).GetComponent<Animator>();
        myAnimator = gameObject.GetComponent<Animator>();
        myCollider = gameObject.GetComponent<Collider2D>();


	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        //If an ally has hit the end level elevator, send them in!
        if (coll.gameObject.tag == "Ally" && !isTakingCharacter)
        {
            isTakingCharacter = true;
            StartCoroutine(absorbCharacter(coll.GetComponent<CharacterMove>()));
        }
    }

    IEnumerator absorbCharacter(CharacterMove character)
    {
        yield return StartCoroutine(character.walkToAndStop(new Vector2(transform.position.x, character.transform.position.y), null));

        Debug.Log("Playing animation!");

        myAnimator.Play("elevator_anim");
        doorAnimator.Play("elevatorDoors_anim");
        

        isTakingCharacter = false;
        yield return null;
    }
}
