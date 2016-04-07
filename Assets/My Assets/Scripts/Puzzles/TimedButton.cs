using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class TimedButton : MonoBehaviour
{

    private SpriteRenderer myRenderer;
    private Collider2D myCollider;

    public Sprite unpressedSprite;
    public Sprite pressedSprite;

    public Activatable[] connectedObjects;

    public int duration;

    public bool isReady = true;

    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myCollider = gameObject.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Start countdown when timer is ready
        if (isReady)
        {
            isReady = false;
            StartCoroutine("Countdown", duration);
        }
        //Only allies can activate 
        if (coll.gameObject.tag == "Ally")
        {
            foreach (Activatable a in connectedObjects)
            {
                a.Activate();
            }

            //Change sprite to the pressedSprite.
            myRenderer.sprite = pressedSprite;

            //Disable the collider, so this button can't be pressed again.
            myCollider.enabled = false;
        }
    }

    private IEnumerator Countdown(int duration)
    {
        int timer = 0;
        while (timer != duration)
        {
            timer++;
            yield return new WaitForSeconds(1);
        }
        //When time is up, reset sprite, colliders, and duration
        if (timer == duration)
        {
            myRenderer.sprite = unpressedSprite;
            myCollider.enabled = true;
            isReady = true;
            foreach (Activatable a in connectedObjects)
            {
                a.Activate();
            }
        }
    }
}
