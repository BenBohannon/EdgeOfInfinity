using UnityEngine;
using System.Collections;

public class MasterDriver : MonoBehaviour {

    public static MasterDriver master;

    public static LayerMask selectedCharacterMask;
    public static LayerMask allCharactersMask;
    

    public float dragSpeed = 10.0f;

    //Initialize things on the global scale.
    void Awake()
    {
        //Setup singleton here.
        if (master == null)
        {
            master = this;
        }
        else if (master != this)
        {
            Destroy(this);
        }

        //Raycasts ignore layers 3 and 8
        selectedCharacterMask = ~0 & ~((1 << 3) | (1 << 10));
        allCharactersMask = ~0 & ~((1 << 3) | (1 << 10) | (1 << 8));

        //Ignore collision between characters and the selected characters.
        Physics2D.IgnoreLayerCollision(8, 10, true);
    }

}
