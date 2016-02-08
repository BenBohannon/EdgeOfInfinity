using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CharacterMove : MonoBehaviour {

    protected Animator myAnimator;
    protected Rigidbody2D myRigidbody;
    protected Collider2D myCollider;

    protected float dropDistanceBeforeDeath = 6.0f;

    //Movement Variables,
    public bool autoWalk = true;
    public bool isMovingRight = true;
    public float speed = 1f; //in Units per second
    public bool avoidsLedges = false;

    protected WaitPlatform myWaitPlatform; //Platform this character is currently waiting on.

	// Use this for initialization
	public virtual void Start () {
        myAnimator = gameObject.GetComponent<Animator>();
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        myCollider = gameObject.GetComponent<Collider2D>();
        
        //If starting out walking, start the walking animation.
        if (autoWalk)
        {
            myAnimator.SetTrigger("isWalking");
        }

        if (!isMovingRight)
        {
            transform.Rotate(0, 180, 0);
        }

        //Set the animation speed based on the Character speed.
        myAnimator.SetFloat("speed", speed);
	}
	
	// Update is called once per frame
	public virtual void Update () {

	}

    //Update for Physics things.
    public virtual void FixedUpdate()
    {
        //If the character is walking, move him in the direction he's walking.
        if (autoWalk)
        {
            myRigidbody.velocity = new Vector2(speed * (isMovingRight ? 1 : -1), myRigidbody.velocity.y);
        }

        //If this character automatically avoids falling to his death.
        if (avoidsLedges)
        {
            Vector3 pos = transform.position + (isMovingRight ? new Vector3(0.7f, 0, 0) : new Vector3(-0.7f, 0, 0));
            //Debug.DrawRay(pos, new Vector2(0, -1 * dropDistanceBeforeDeath), Color.blue, 2f);

            //If we hit something to land on.
            RaycastHit2D hit = Physics2D.Raycast(pos, new Vector2(0, -1), dropDistanceBeforeDeath, MasterDriver.regularCharacterMask);
            if (hit.collider == null)
            {
                reverseDirection();
            }
        }
    }

    //Called when gameobject collides with another collider.
    public virtual void OnCollisionEnter2D(Collision2D coll)
    {
        Vector3 pos = transform.position + (isMovingRight ? new Vector3(0.6f, 0, 0) : new Vector3(-0.6f, 0, 0));
        //Debug.DrawRay(pos, new Vector2(isMovingRight ? 0.2f : -0.2f, 0), Color.blue, 2f);

        //If we hit a wall.
        RaycastHit2D hit = Physics2D.Raycast(pos, new Vector2(isMovingRight ? 0.2f : -0.2f, 0), 1f, MasterDriver.regularCharacterMask);
        if (hit.collider != null)
        {
            reverseDirection();

            //If we hit another character, reverse them too.
            if (hit.transform.tag == "Ally")
            {
                //If they're already going away from us, don't reverse them.
                CharacterMove script = hit.transform.GetComponent<CharacterMove>();
                if (script.isMovingRight == isMovingRight)
                {
                    script.reverseDirection();
                }
            }
        }
    }

    protected void reverseDirection()
    {
        //Reverse movement direction and animation facing.
        isMovingRight = !isMovingRight;
        if (!isMovingRight)
        {
			transform.Rotate (0, 180, 0);
        }
        else
        {
			transform.Rotate (0, 180, 0);
        }
    }

    //Move the character to the specified position and stops them there.
    public IEnumerator walkToAndStop(Vector2 pos, WaitPlatform platform)
    {
        myWaitPlatform = platform;

        //Stop automatic movement.
        autoWalk = false;

        while (true)
        {
            //Get the direction we need to move in.
            Vector2 direction = (pos - myRigidbody.position);

            //If we're already pretty much there.
            if (direction.magnitude < 0.1)
            {
                //Stop moving.
                myRigidbody.velocity = Vector2.zero;
                myAnimator.ResetTrigger("isWalking");
                myAnimator.SetTrigger("isIdle");
                break;
            }

            //Else, move towards that position at the speed of 2.
            myRigidbody.velocity = new Vector2(2 * direction.normalized.x, myRigidbody.velocity.y);

            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

}
