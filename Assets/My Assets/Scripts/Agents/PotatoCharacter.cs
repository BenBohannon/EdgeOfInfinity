using UnityEngine;
using System.Collections;

public class PotatoCharacter : DraggableCharacter {

	override public void Start () {
        base.Start();
    }

    public override void FixedUpdate()
    {
        if (myRigidbody.velocity.y < -.1)
        {          
            if (!falling)
            {
                falling = true;
                myAnimator.ResetTrigger("isWalking");
                myAnimator.ResetTrigger("isIdle");
                myAnimator.SetTrigger("isFalling");               
            }
        }
        else if (myRigidbody.velocity.y >= 0)
        {                        
            if (falling)
            {
                falling = false;
                myAnimator.ResetTrigger("isFalling");
                myAnimator.SetTrigger("isWalking");                
            }
        }
        base.FixedUpdate();        
    }

    override public void OnCollisionEnter2D(Collision2D coll)
    {
        Vector3 pos = transform.position + (isMovingRight ? new Vector3(1f, 0, 0) : new Vector3(-1f, 0, 0));
        //Debug.DrawRay(pos, new Vector2(isMovingRight ? 1f : -1f, 0), Color.blue, 2f);

        //If we hit a wall.
        RaycastHit2D hit = Physics2D.Raycast(pos, new Vector2(isMovingRight ? 0.2f : -0.2f, 0), 1f, MasterDriver.regularCharacterMask);
        if (hit.collider != null)
        {            
            // If we don't hit a pushable crate
            if (hit.transform.tag != "Crate" && hit.transform.tag != "Heavy Crate")
            {                
                reverseDirection();
            }

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

    override public void reverseDirection()
    {
        //Reverse movement direction and animation facing.
        isMovingRight = !isMovingRight;
        if (!isMovingRight)
        {
            transform.Rotate(0, 180, 0);
        }
        else
        {
            transform.Rotate(0, 180, 0);
        }
    }

    override public void die()
    {
        myAnimator.ResetTrigger("isWalking");
        myAnimator.SetTrigger("isDead");
        autoWalk = false;
        StartCoroutine("DeathCountdown", 78);        
    }

    private IEnumerator DeathCountdown(int duration)
    {
        int timer = 0;
        while (timer < duration)
        {
            timer++;
            Debug.Log(timer);
            yield return new WaitForFixedUpdate();
        }
        //When time is up, remove the character
        if (timer == duration)
        {
            Destroy(this.gameObject);
            print("dead");
        }
        Debug.Log("Countdown Complete!");
    }
}
