using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeyCollectible : ItemGeneric
{
    public AudioSource keySound;
    public override void PickUp()
    {
        ItemCounter.gateKeyCount++;
        
        // keySound.Play();    // please uncomment this when you've found a sound effect for the key. :>
        Debug.Log("Picked up a key.");

        base.PickUp(); // plays animation, destroys the object.
    }
}
 