using UnityEngine;
using System.Collections;

public class EndElevator : MonoBehaviour {

    private Animator myAnimator;
    private Animator doorAnimator;
    private Collider2D myCollider;
    private SpriteRenderer doorRenderer;

    private bool isTakingCharacter = false;

	// Use this for initialization
	void Start () {
        doorAnimator = transform.GetChild(0).GetComponent<Animator>();
        myAnimator = gameObject.GetComponent<Animator>();
        myCollider = gameObject.GetComponent<Collider2D>();
        doorRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
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

        myAnimator.SetTrigger("isClosed");
        doorAnimator.SetTrigger("isClosed");

        //Disable this character's collider, so others will walk by.
        character.GetComponent<Collider2D>().enabled = false;
        character.GetComponent<Rigidbody2D>().gravityScale = 0;
        character.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //Render the door over the character.
        doorRenderer.sortingLayerName = "Character";
        character.GetComponent<SpriteRenderer>().sortingLayerName = "Pre-Character";

        //Wait for animation to finish.
        yield return new WaitForSeconds(1.5f);

        MasterDriver.levelDriver.saveCharacter(character);

        //Destroy the character, render the door back again.
        DestroyImmediate(character.gameObject);
        doorRenderer.sortingLayerName = "Background Object";

        yield return new WaitForSeconds(1f);

        isTakingCharacter = false;
        yield return null;
    }
}
