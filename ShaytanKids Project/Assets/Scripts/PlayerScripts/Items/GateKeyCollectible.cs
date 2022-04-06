using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeyCollectible : ItemGeneric
{
    public override void PickUp()
    {
        ItemCounter.gateKeyCount++;
        ItemCounter.playerItemCounter.UpdateUI();

        Debug.Log("Picked up a key.");

        base.PickUp(); // plays animation, destroys the object.
    }
}
