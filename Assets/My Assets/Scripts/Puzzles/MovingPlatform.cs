using UnityEngine;
//using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class MovingPlatform : Activatable
{
    private Transform currentWaypoint;
    private int wayPointIndex;
    private double beginWait;
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
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Ally")
        {
            beginWait = Time.time;
        }
    }

    public override void Activate()
    {
        active = !active;
    }
}