using UnityEngine;
using System.Collections;

public class DraggableCharacter : CharacterMove {

    private bool isBeingDragged = false;

	// Use this for initialization
	override public void Start ()
    {
        base.Start();
	}

    override public void FixedUpdate()
    {
        //If not being dragged, behave as a normal character.
        if (!isBeingDragged)
        {
            base.FixedUpdate();
            return;
        }

        //Otherwise, be dragged!
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        myRigidbody.MovePosition(moveToPosition(mousePos));
        
    }

    override public void OnCollisionEnter2D(Collision2D coll)
    {
        //Don't do collision things while being dragged.
        if (isBeingDragged)
        {
            return;
        }

        base.OnCollisionEnter2D(coll);
    }

    //Turn dragging on and off.
    public void toggleDragging()
    {
        isBeingDragged = !isBeingDragged;
        
        //If we're starting to be dragged around.
        if (isBeingDragged)
        {
            if (myWaitPlatform != null)
            {
                myWaitPlatform.removeCharacter(this);
                myWaitPlatform = null;
            }
            //Stop trying to walk onto a waitPlatform, if you are.
            this.StopCoroutine("walkToAndStop");

            //Put the character on the selected character layer. (ignores collisions and raycasts with other characters)
            gameObject.layer = 10;

            //Turn off gravity for this character.
            myRigidbody.gravityScale = 0;
        }
        //Else, we're stopping being dragged.
        else
        {
            //Put the character on the regular character layer.
            gameObject.layer = 8;

            //Turn gravity back on.
			myRigidbody.gravityScale = GravButton.getGravityScale();

            //Zero out their velocity. Don't want them flying around.
            myRigidbody.velocity = Vector2.zero;

            autoWalk = true;

            myAnimator.ResetTrigger("isIdle");
            myAnimator.SetTrigger("isWalking");
        }
    }

    void OnMouseDown()
    {
        toggleDragging();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            reverseDirection();
        }
    }

    void OnMouseUp()
    {
        toggleDragging();
    }


    private Vector2 moveToPosition(Vector2 pos)
    {
        //Get direction the character has to travel.
        Vector2 direction = (pos - (Vector2)transform.position);

        //Cast to that position, to ensure we don't jump over walls.
        RaycastHit2D hit = Physics2D.Raycast(myRigidbody.position, direction, direction.magnitude, MasterDriver.draggedCharacterMask);

        //If we do hit a wall or floor.
        if (hit.collider != null)
        {
            //Calculate the closest position we can get to the point.
            Vector2 closestPos = pos - (Vector2)Vector3.Project(direction, hit.normal);
            direction = closestPos - (Vector2)transform.position;

            //If it's already nearby, just forget it and don't move.
            if (direction.magnitude < 0.5f)
            {
                return (Vector2) transform.position;
            }

            //Otherwise, path to THAT point.
            pos = moveToPosition(closestPos);

        }

        return pos;
    }

}
