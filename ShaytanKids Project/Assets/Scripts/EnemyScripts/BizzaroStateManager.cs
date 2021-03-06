using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BizzaroStateManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool mustPatrol, mustTurn;
    public float distToPlayer;
    public bool notShooting;
    public float range;
    public float shotDelay;
    public float walkspeed;
    public Vector3 playerDirection;
    BizzaroState currentState;
    public Transform groundcheckpos;
    public LayerMask groundLayer;
    public Collider2D bodycollider;
    public BizzaroPatrollingState bizzaroPatrollingState;
    // public ChasingState chasingState;
    public BizzaroAttackingState bizzaroAttackingState;
    public GameObject player;
    /*public GameObject Grenade;
    public float grenadeSpeed;*/
    public PlayerHealth playerHealth;
    public GameObject bizzaro;
    // public Rigidbody2D grenadeRb;
    public int enemyRadius;




    [SerializeField] public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }
    void Start()
    {
        /*Grenade = GameObject.FindGameObjectWithTag("Projectile");*/
        bizzaro = GameObject.FindGameObjectWithTag("Bizzaro");
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyRadius = 4;

        bizzaroPatrollingState = new BizzaroPatrollingState();
        bizzaroAttackingState = new BizzaroAttackingState();
        notShooting = true;
        mustPatrol = true;
        currentState = bizzaroPatrollingState;
    }


    void Update()
    {

        distToPlayer = Vector2.Distance(this.transform.position, player.transform.position);
        currentState.UpdateState(this);



    }


    public void ChangeState(BizzaroState desiredState)
    {
        currentState = desiredState;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyRadius);

    }

    /* public GameObject Spawnbullet(BizzaroStateManager bizzaroStateManager)
     {
         GameObject thebullet = Instantiate(bizzaroStateManager.Grenade, (Vector2)bizzaroStateManager.transform.position + bizzaroAttackingState.directionToPlayer.normalized, Quaternion.identity);
         return thebullet;
     }*/
    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundcheckpos.position, 0.1f, groundLayer);
        }
    }
}


public abstract class BizzaroState
{


    public abstract void UpdateState(BizzaroStateManager bizzaroStateManager);

}

public class BizzaroPatrollingState : BizzaroState
{

    public override void UpdateState(BizzaroStateManager bizzaroStateManager)
    {
        bizzaroStateManager.anim.SetBool("walk", true);

        if (bizzaroStateManager.mustPatrol)
        {
            if (bizzaroStateManager.bodycollider.IsTouchingLayers(bizzaroStateManager.groundLayer))
            {
                Flip(bizzaroStateManager);
            }
            bizzaroStateManager.rb.velocity = new Vector2(bizzaroStateManager.walkspeed * Time.fixedDeltaTime, bizzaroStateManager.rb.velocity.y);// Patrol()
        }
        Debug.Log("you are patrolling");


        if (bizzaroStateManager.distToPlayer <= bizzaroStateManager.range)
        {
            bizzaroStateManager.ChangeState(bizzaroStateManager.bizzaroAttackingState);
            Debug.Log("you are attacking now");


        }


    }

    public void Flip(BizzaroStateManager bizzaroStateManager)
    {

        bizzaroStateManager.transform.localScale = new Vector2(bizzaroStateManager.transform.localScale.x * -1, bizzaroStateManager.transform.localScale.y);
        bizzaroStateManager.walkspeed *= -1;

    }

}



public class BizzaroAttackingState : BizzaroState
{
    public float meleeTimer;
    public Vector2 directionToPlayer;


    public override void UpdateState(BizzaroStateManager bizzaroStateManager)
    {

        if (bizzaroStateManager.player.transform.position.x > bizzaroStateManager.transform.position.x && bizzaroStateManager.transform.localScale.x < 0 || bizzaroStateManager.player.transform.position.x < bizzaroStateManager.transform.position.x && bizzaroStateManager.transform.localScale.x > 0)
        {
            bizzaroStateManager.bizzaroPatrollingState.Flip(bizzaroStateManager);
        }

        bizzaroStateManager.mustPatrol = false;
        bizzaroStateManager.anim.SetBool("walk", false);
        bizzaroStateManager.rb.velocity = Vector2.zero;
        bizzaroStateManager.notShooting = false;
        DamageTarget(bizzaroStateManager);
        meleeTimer += Time.deltaTime;
        Debug.Log("YOU ARE SHOOTING");

        if (bizzaroStateManager.distToPlayer >= bizzaroStateManager.range)
        {
            bizzaroStateManager.notShooting = true;
            bizzaroStateManager.mustPatrol = true;
            bizzaroStateManager.ChangeState(bizzaroStateManager.bizzaroPatrollingState);
            Debug.Log("back to patrolling");


        }








    }



    public void DamageTarget(BizzaroStateManager bizzaroStateManager)
    {
        if (bizzaroStateManager.player != null)
        {

            directionToPlayer = bizzaroStateManager.player.transform.position - bizzaroStateManager.transform.position;
        }

        if (bizzaroStateManager.distToPlayer <= bizzaroStateManager.range)
        {
            if (meleeTimer > 1)
            {

                bizzaroStateManager.anim.SetTrigger("mace");
                bizzaroStateManager.playerHealth.TakeDamage(10);
                bizzaroStateManager.playerHealth.tookDamage = true;
                bizzaroStateManager.playerHealth.currentHealthRegenTimer = bizzaroStateManager.playerHealth.healthRegenTimer;
                meleeTimer = 0;
            }

        }

        /*  if (bizzaroStateManager.player != null && shotTimer >= bizzaroStateManager.shotDelay)
          {
              bizzaroStateManager.anim.SetTrigger("mace");
              GameObject thebullet = bizzaroStateManager.Spawnbullet(bizzaroStateManager);
              Rigidbody2D rb = thebullet.GetComponent<Rigidbody2D>();


              Vector2 direction = directionToPlayer * bizzaroStateManager.grenadeSpeed;
              rb.velocity = direction;
              rb.transform.up = direction;

              shotTimer = 0f;

          }*/

    }


    /*public void DamagePlayer(BizzaroStateManager bizzaroStateManager)
    {
        Collider2D PlayerCollider = Physics2D.OverlapCircle(bizzaroStateManager.player.transform.position, bizzaroStateManager.playerRadius);
        if(PlayerCollider != null && PlayerCollider.gameObject.tag == ("Player"))
        {
            var playerHealth = PlayerCollider.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(20);
            bizzaroStateManager.anim.SetTrigger("");
 
        }
    }
 
*/

    /* public void Shoot(BizzaroStateManager bizzaroStateManager)
     {
         bizzaroStateManager.SpawnBullet(bizzaroStateManager);
         bizzaroStateManager.shotdelay = 2;
     }
 */
    /*   public void GrenadeMovement(BizzaroStateManager bizzaroStateManager)
        {
           bizzaroStateManager.grenadeRb = bizzaroStateManager.Grenade.GetComponent<Rigidbody2D>();
           bizzaroStateManager.grenadeRb.AddForce(bizzaroStateManager.playerDirection, ForceMode2D.Impulse); 



        }*/



}






