using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeyCollectible : ItemGeneric
{
    public override void PickUp(ItemCounter player)
    {
        player.gateKeyCount++;
        
        Debug.Log("Picked up a key.");
        // (call the ItemCounter's UI update function here)

        base.PickUp(player); // plays animation, destroys the object.
    }
}
