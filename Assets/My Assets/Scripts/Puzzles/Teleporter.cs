using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Teleporter : Activatable
{
    private Collider2D myCollider;
    private Collider2D connectedTeleporterColl;
    private bool teleporting;
    private double teleportedTime;

    public bool active;
    public Activatable connectedTeleporter;

    // Use this for initialization
    void Start()
    {
        myCollider = gameObject.GetComponent<Collider2D>();
        connectedTeleporterColl = connectedTeleporter.GetComponent<Collider2D>();
        teleporting = false;
        teleportedTime = 0.0;
        if (!active)
        {
            myCollider.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Makes time for the ally to step off the teleporter before it is ready to teleport again
        if (teleporting && Time.time > teleportedTime + 0.6)
        {
            connectedTeleporterColl.enabled = true;
            teleportedTime = 0.0;
            teleporting = false;
        }
    }

    override public void Activate()
    {
        //Turning off the teleporter
        if (active)
        {
            myCollider.enabled = false;
        }
        //Turning on the teleporter
        else
        {
            myCollider.enabled = true;
        }
        active = !active;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Only allies are capable of using active teleporters
        if (coll.gameObject.tag == "Ally" && connectedTeleporterColl.enabled == true)
        {
            //Moves the ally to the connected teleporter
            coll.gameObject.transform.position = connectedTeleporter.transform.position + new Vector3(0, 0.23f, 0);
            connectedTeleporterColl.enabled = false;
            teleporting = true;
            teleportedTime = Time.time;
        }
    }
}