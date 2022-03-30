using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCounter : MonoBehaviour
{
    public int gateKeyCount;
    public int potionCount; 
    // since there are only two collectibles and both are stackable,
    // these counters will function as the player's collectibles.

    public ItemCounter itemCounter;
    private void Awake() // sets up the class to make sure there's only one ItemCounter in the scene
    {
        if (itemCounter == null)
        {
            itemCounter = this;
        }
    }

    public void UpdateUI()
    {
        // this class needs a reference to the UI to display item vars,
        // and to update them when an item is collected or used.
    }

}

    // ItemGeneric item;

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            item = collision.GetComponent<ItemGeneric>();
            item.PickUp();
        }
    }*/
