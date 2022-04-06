using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPotionCollectible : ItemGeneric
{
    // code for having potions slow fall.
    Rigidbody2D rb;
    [SerializeField] float fallSpeed;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, fallSpeed);
    }


    public override void PickUp() 
    {
        ItemCounter.potionCount++;
        ItemCounter.playerItemCounter.UpdateUI();

        Debug.Log("Picked up a potion.");

        base.PickUp(); // plays animation, destroys the object.
    }
}
