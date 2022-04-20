using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPotionCollectible : ItemGeneric
{
    // note: falling physics for potions is now done with Rigidbody2D. check the component
    // and edit the mass, drag and gravity values if you'd like to change fall speed.

    float timer;
    [SerializeField] float timeTillDestroyed = 20;
    public override void PickUp() 
    {
        ItemCounter.potionCount++;

        //Debug.Log("Picked up a potion.");

        base.PickUp(); // plays animation, destroys the object.
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeTillDestroyed)
        {
            Destroy(gameObject);
        }
    }
}
