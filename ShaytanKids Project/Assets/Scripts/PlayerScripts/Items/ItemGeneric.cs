using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A template class for items.
/// </summary>
public abstract class ItemGeneric : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            PickUp();
        
    }

    // function to be overrided with adding to whichever int the respective item corresponds to
    public virtual void PickUp() 
    {
        //Debug.Log("Collected " + this.name);
        // (play animation for pickup, eg puff of smoke)

        Destroy(this.gameObject);
    }

}

