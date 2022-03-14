using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    private float distToPlayer;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn, canShoot;

    public Rigidbody2D rb;
    public float walkspeed, range, timeBtwShots, shootspeed;
    public Transform groundcheckpos;
    public LayerMask groundLayer;
    public Collider2D bodycollider;
    public Transform player, shootPos;
    public GameObject bullet;
    void Start()
    {
        mustPatrol = true;
        canShoot = true;
        
    }

    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

        distToPlayer = Vector2.Distance(transform.position, player.position);

        if(distToPlayer <= range)
        {
            if(player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            mustPatrol = false;
            rb.velocity = Vector2.zero;

            if(canShoot)
            StartCoroutine(shoot());
        }
        else
        {
            mustPatrol = true; 
        }
        
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundcheckpos.position, 0.1f, groundLayer);
        }

    }

    void Patrol()
    {
        if (mustTurn || bodycollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(walkspeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkspeed *= -1;
        mustPatrol = true;
    }


    IEnumerator shoot()
    {
        canShoot = false; 
        yield return new WaitForSeconds(timeBtwShots);
        GameObject newbullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
        newbullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootspeed * walkspeed * Time.fixedDeltaTime, 0f);
        Debug.Log("Shoot");
        canShoot = true;
    }
}
