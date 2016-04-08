using UnityEngine;
//using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(SliderJoint2D))]
public class MovingPlatform : WaitPlatform
{
    private Transform currentWaypoint;
    private int wayPointIndex;
    private double beginWait;
//	private SliderJoint2D joint;
    //Animator anim;

    public bool active;
    public int speed = 5;
    public double timeAtWaypoint;
    public Transform[] waypoints;


    // Use this for initialization
    void Start()
    {
        currentWaypoint = waypoints[0];
        wayPointIndex = 0;
        beginWait = 0.0;
        //anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (active)
        {
            Vector2 destPos = currentWaypoint.position;
            Vector2 currPos = transform.position;
            Vector2 direction = destPos - currPos;
            if (direction.magnitude < 0.1 && beginWait == 0.0)
            {
                beginWait = Time.time;
            }
            else if (beginWait > 0.0)
            {
                //forces the platform to wait for a specified portion of time
                if (Time.time > beginWait + timeAtWaypoint)
                {
                    wayPointIndex = (wayPointIndex + 1) % waypoints.Length;
                    currentWaypoint = waypoints[wayPointIndex];
                    beginWait = 0.0;
                }
            }
            else
            {
                //check if the transform movement goes over where it should (causes it to juggle back and forth)
                //if it does not, update the movement normally
                if (direction.x < 0)
                {
                    //anim.SetTrigger("movingLeft");
                    //if the change would make the x position go beyond the waypoint's x position, go to the waypoint's x position
                    if (destPos.x - (currPos.x + -0.01f * speed) > 0)
                    {
                        transform.position = new Vector3(destPos.x, transform.position.y, 0);
                    }
                    else
                    {
                        transform.position += new Vector3(-.01f * speed, 0, 0);
                    }
                }
                else if (direction.x > 0)
                {
                    //anim.SetTrigger("movingRight");
                    if (destPos.x - (currPos.x + 0.01f * speed) < 0)
                    {
                        transform.position = new Vector3(destPos.x, transform.position.y, 0);
                    }
                    else
                    {
                        transform.position += new Vector3(+.01f * speed, 0, 0);
                    }
                }
                if (direction.y < 0)
                {
                    if (destPos.y - (currPos.y + -0.01f * speed) > 0)
                    {
                        transform.position = new Vector3(transform.position.x, destPos.y, 0);
                    }
                    else
                    {
                        transform.position += new Vector3(0, -0.01f * speed, 0);
                    }
                }
                else if (direction.y > 0)
                {
                    if (destPos.y - (currPos.y + 0.01f * speed) < 0)
                    {
                        transform.position = new Vector3(transform.position.x, destPos.y, 0);
                    }
                    else
                    {
                        transform.position += new Vector3(0, +0.01f * speed, 0);
                    }
                }
            }
			heldCharacters.Sort(new posComparer());

			//Calculate the max distance from the starting platform we need.
			float maxDistance = -(spacePadding * (heldCharacters.Count - 1)) / 2f;

            List<CharacterMove> removeList = new List<CharacterMove>();

            //Move each character to their new position.
            for (int i = 0; i < heldCharacters.Count; i++)
            {
                if (heldCharacters[i].myWaitPlatform == this)
                {
                    Vector2 myPlace = new Vector2(transform.position.x + maxDistance + (i * spacePadding), heldCharacters[i].transform.position.y);
                    StartCoroutine(heldCharacters[i].walkToAndStop(myPlace));
                }
                else
                {
                    removeList.Add(heldCharacters[i]);
                }
            }

            foreach (CharacterMove c in removeList)
            {
                heldCharacters.Remove(c);
            }
        }
    }

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag != "Ally")
        {
            beginWait = Time.time;
        }
		//If this player is being dragged, don't try and make it stop...
		if (coll.gameObject.layer == 10)
		{
			return;
		}

		if (coll.gameObject.tag == "Ally")
		{
//			ConnectTo (coll.gameObject.GetComponent<Rigidbody2D> ());
			//If this plaform is full, let the character pass.
			if (heldCharacters.Count >= space)
			{
				return;
			}

			//Otherwise, add them to the list and make them stop here.
			CharacterMove character = coll.gameObject.GetComponent<CharacterMove>();
			heldCharacters.Add(character);
            character.myWaitPlatform = this;

			//Calculate the max distance from the starting platform we need.
			float maxDistance = -(spacePadding * (heldCharacters.Count - 1)) / 2f;

			//Stop any current movement.
			StopAllCoroutines();

			//Sort the characters based on position.
			heldCharacters.Sort(new posComparer());

            List<CharacterMove> removeList = new List<CharacterMove>();

            //Move each character to their new position.
            for (int i = 0; i < heldCharacters.Count; i++)
            {
                if (heldCharacters[i].myWaitPlatform == this)
                {
                    Vector2 myPlace = new Vector2(transform.position.x + maxDistance + (i * spacePadding), heldCharacters[i].transform.position.y);
                    StartCoroutine(heldCharacters[i].walkToAndStop(myPlace));
                    heldCharacters[i].transform.parent = transform;
                }
                else
                {
                    removeList.Add(heldCharacters[i]);
                }
            }

            foreach (CharacterMove c in removeList)
            {
                removeCharacter(c);
            }

		}
	}

	public void removeCharacter(CharacterMove character)
	{
		character.transform.parent = null;
		heldCharacters.Remove(character);
        character.myWaitPlatform = null;

		//Sort the characters based on x position.
		heldCharacters.Sort(new posComparer());

		//Calculate the max distance from the starting platform we need.
		float maxDistance = -(spacePadding * (heldCharacters.Count - 1)) / 2f;

		//Move each character to their new position.
		for (int i = 0; i < heldCharacters.Count; i++)
		{
			heldCharacters[i].StopCoroutine("walkToAndStop");
			Vector2 myPlace = new Vector2(transform.position.x + maxDistance + (i * spacePadding), heldCharacters[i].transform.position.y);
			heldCharacters[i].StartCoroutine(heldCharacters[i].walkToAndStop(myPlace));
		}

	}

//    void OnTriggerEnter2D(Collider2D coll)
//    {
//        if (coll.gameObject.tag != "Ally")
//        {
//            beginWait = Time.time;
//        }
//    }

    public override void Activate()
    {
        active = !active;
    }
}