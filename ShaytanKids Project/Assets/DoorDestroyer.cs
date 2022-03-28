using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag== "Arrow")
        {
            Destroy(this.gameObject);
        }

    }
}
