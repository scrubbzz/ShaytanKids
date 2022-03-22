using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPotionCollectible : ItemGeneric
{
    public override void PickUp(ItemCounter player)
    {
        player.potionCount++;

        Debug.Log("Picked up a potion.");
        // (call the ItemCounter's UI update function here)

        base.PickUp(player); // plays animation, destroys the object.
    }
}
