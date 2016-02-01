using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class GravButton : MonoBehaviour {

	private static int gravityScale = 1;
	private SpriteRenderer myRenderer;
	private Collider2D myCollider;

	public Sprite pressedSprite;

	void Start()
	{
		myRenderer = gameObject.GetComponent<SpriteRenderer>();
		myCollider = gameObject.GetComponent<Collider2D>();
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		//Only allies can activate 
		if (coll.gameObject.tag == "Ally")
		{
			//Activate Zero Gravity Mode.
			GameObject[] gos;
			gos = GameObject.FindGameObjectsWithTag ("Ally");
			gravityScale = -1 * gravityScale;
			foreach (GameObject go in gos) {
				go.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
				go.transform.Rotate (0, 180, 180);
			}

			//Change sprite to the pressedSprite.
			myRenderer.sprite = pressedSprite;

			//Disable the collider, so this button can't be pressed again.
			myCollider.enabled = false;
		}
	}

	public static int getGravityScale()
	{
		return gravityScale;
	}
}
