using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class OneWayDoor : MonoBehaviour {

	private Collider2D myCollider;

	public bool isExit;

	void Start()
	{
		myCollider = gameObject.GetComponent<Collider2D>();
		myCollider.isTrigger = true;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		//Only allies can activate 
		if (coll.gameObject.tag == "Ally")
		{
			Collider2D door = gameObject.transform.parent.gameObject.GetComponent<Collider2D> ();
			if (!isExit) {
				Physics2D.IgnoreCollision (coll.gameObject.GetComponent<Collider2D> (), door);
			}
		}

	}

	void OnTriggerExit2D(Collider2D coll)
	{
		//Only allies can activate 
		if (coll.gameObject.tag == "Ally")
		{
			Collider2D door = gameObject.transform.parent.gameObject.GetComponent<Collider2D> ();
			if (isExit) {
				Physics2D.IgnoreCollision (coll.gameObject.GetComponent<Collider2D> (), door, false);
			}
		}

	}
		
}