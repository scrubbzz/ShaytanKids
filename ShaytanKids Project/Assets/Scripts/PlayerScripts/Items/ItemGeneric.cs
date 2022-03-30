using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// not to be used on its own, this is purely meant to be overrided by deriving classes.
public class ItemGeneric : MonoBehaviour
{

    protected ItemCounter player; // reference to the player's item counter


    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<ItemCounter>();
        PickUp(player);
    }

    // function to be overrided with adding to whichever int the respective item corresponds to
    public virtual void PickUp(ItemCounter player)
    {
        Debug.Log("Collected " + this.name);
        // (play animation for pickup, eg puff of smoke)

        Destroy(this.gameObject);
    }

}

    /*
    public enum itemType
    {
        gateKey,
        greenPotion,
        letterClue
    }*/
