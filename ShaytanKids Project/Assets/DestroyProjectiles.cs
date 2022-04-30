using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectiles : MonoBehaviour
{
    private bool collided;
    public bool Collided
    {
        get
        {
            return collided;
        }
        set
        {
            collided = value;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Arrow")
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            Destroy(this.gameObject, 0.5f);

            collided = true;
        }

    }
    /* private void OnTriggerEnter2D(Collider2D collision)
     {
         Destroy(this.gameObject, 0.5f);
     }*/
}
