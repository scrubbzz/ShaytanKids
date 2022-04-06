using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchangelAttackManager : MonoBehaviour
{
    public int projectileDamage;

    public GameObject Target;
    public Transform detectorOrigin;
    public Vector2 detectorSize = new Vector2(30, 30);
    public Vector2 detectorOriginOffset = Vector2.zero;

    public Vector2 directionToTarget;
    public LayerMask detectorLayermask;

    public float timer;
    public float fireDelay;

    public GameObject projectile;
    public int projectileSpeed;
    public Vector2 spawnOffset;

    // Start is called before the first frame update 
    void Start()
    {
        
        detectorOrigin = this.transform;
    }

    // Update is called once per frame 
    void Update()
    {
        timer += Time.deltaTime;
        LocatePlayer();
        DamagePlayer();
    }
    public void LocatePlayer()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, detectorLayermask);
        if (collider != null && collider.gameObject.tag == "Player")
        {
            Target = collider.gameObject;
            Debug.Log("DetectedPlayer");
        }
        else
        {
            Target = null;
        }
    }
    public void DamagePlayer()
    {
        if(Target != null)
        {
            directionToTarget = Target.transform.position - detectorOrigin.position;
        }
        

        if (Target != null && timer >= fireDelay)
        {
            GameObject theProjectile = Instantiate(projectile, (Vector2)transform.position + directionToTarget.normalized, /*+ spawnOffset,*/ Quaternion.identity);
            Rigidbody2D rb = theProjectile.GetComponent<Rigidbody2D>();

            Vector2 direction = directionToTarget * projectileSpeed;
            rb.velocity = direction;
            rb.transform.up = direction;

            timer = 0;
            Debug.Log("The player is here");
        }
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, detectorSize);
    }

}