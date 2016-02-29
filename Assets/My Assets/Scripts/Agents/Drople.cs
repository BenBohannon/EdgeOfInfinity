﻿using UnityEngine;
using System.Collections;

/**
 *  Droples can move through water.
 *  Still needs the interaction with lava to be added in. (Whenever lava is added)
 *  Currently Droples are unable to be dragged through water.
 */
public class Drople : DraggableCharacter {
    private float gravity;

    public override void Start()
    {
        gravity = 1.0f;
        base.Start();
    }

    override public void FixedUpdate()
    {
        myRigidbody.gravityScale = gravity;
        base.FixedUpdate();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Water")
        {
            gravity = 0.1f;
            speed = 1.5f;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Water")
        {
            gravity = 1.0f;
            speed = 3.0f;
        }
    }
}
