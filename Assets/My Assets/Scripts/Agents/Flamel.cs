using UnityEngine;
using System.Collections;

/**
 * Flamels can walk on lava, but die to water.
 */
public class Flamel : DraggableCharacter {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Water")
        {
            die();
        }
    }
}
