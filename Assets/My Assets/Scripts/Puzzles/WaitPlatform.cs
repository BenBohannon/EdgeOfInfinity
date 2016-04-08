using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class WaitPlatform : Activatable {

    public int space = 1;
    public float spacePadding = 2f;

    protected List<CharacterMove> heldCharacters = new List<CharacterMove>();

	void OnTriggerEnter2D(Collider2D coll)
    {
        //If this player is being dragged, don't try and make it stop...
        if (coll.gameObject.layer == 10)
        {
            return;
        }

        if (coll.gameObject.tag == "Ally")
        {
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
                }
                else
                {
                    removeList.Add(heldCharacters[i]);
                }
            }

            foreach(CharacterMove c in removeList)
            {
                removeCharacter(c);
            }

        }
    }

    public void removeCharacter(CharacterMove character)
    {
		character.transform.parent = null;
        character.myWaitPlatform = null;
        heldCharacters.Remove(character);

        //Sort the characters based on x position.
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

	public override void Activate()
	{
		return;
//		active = !active;
	}
}

public class posComparer : IComparer<CharacterMove>
{
    public int Compare(CharacterMove a, CharacterMove b)
    {
        if (a.transform.position.x >= b.transform.position.x)
        {
            return 1;
        }
        return -1;
    }
}
