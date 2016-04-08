using UnityEngine;
using System.Collections;

/** 
 * Eye Guys shoot projectiles to kill the player's characters.  Flamels are capable of killing Eye Guys.
 */
public class eyeGuy : CharacterMove {
    public float sight;
    public GameObject beam;
    
    private float activationTime;
    private GameObject instantiatedBeam;
    private float prevX;
    private float prevY;

    // Use this for initialization
    override public void Start () {
        beam.SetActive(false);
        base.Start();
	}
	
	// Update is called once per frame
	override public void Update () {
        float currentX = transform.position.x;
        float currentY = transform.position.y;
        if (instantiatedBeam != null && instantiatedBeam.activeSelf)
        {
            instantiatedBeam.transform.position += new Vector3(currentX - prevX, currentY - prevY, 0);
            prevX = transform.position.x;
            prevY = transform.position.y;
            if (Time.time > activationTime + 0.3f)
            {
                instantiatedBeam.SetActive(false);
                Destroy(instantiatedBeam);
                prevX = 0f;
                prevY = 0f;
            }
        }

        Vector3 pos = transform.position + (isMovingRight ? new Vector3(1f, 0, 0) : new Vector3(-1f, 0, 0));
        //If we see an "Ally"
        RaycastHit2D hit = Physics2D.Raycast(pos, new Vector2(isMovingRight ? 0.2f : -0.2f, 0), sight, MasterDriver.regularCharacterMask);
        if (hit.collider != null) {
            if (hit.transform.tag == "Ally")
            {
                CharacterMove character = hit.transform.GetComponent<CharacterMove>();
                if (character is Flamel)
                {
                    return;
                }
                character.die();
                instantiatedBeam = Instantiate(beam);
                instantiatedBeam.transform.position = transform.position + new Vector3(isMovingRight ? 2.7f : -2.7f, 0.4f, 0);
                instantiatedBeam.transform.localScale = new Vector3(1.2f, 1, 1);
                instantiatedBeam.SetActive(true);
                activationTime = Time.time;
                prevX = transform.position.x;
                prevY = transform.position.y;
            }

        }

        base.Update();
	}

    /**
     * If this enemy hits a flamel, it dies.
     */
    override public void OnCollisionEnter2D(Collision2D coll)
    {
        Flamel character = coll.transform.GetComponent<Flamel>();
        if (character != null)
        {
            die();
        }
        base.OnCollisionEnter2D(coll);
    }
}