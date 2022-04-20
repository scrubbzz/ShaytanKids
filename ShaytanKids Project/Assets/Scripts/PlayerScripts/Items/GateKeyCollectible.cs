using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeyCollectible : ItemGeneric
{
    public AudioSource keySound;
    public override void PickUp()
    {
        ItemCounter.gateKeyCount++;
        ItemCounter.playerItemCounter.UpdateUI();
        keySound.Play();
        Debug.Log("Picked up a key.");

        base.PickUp(); // plays animation, destroys the object.
    }
}
 