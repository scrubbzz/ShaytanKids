using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKStateManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool mustPatrol, mustTurn;
    public float distToPlayer;
    public float range;
    public float shotDelay;
    public bool notShooting;
    public float walkspeed;
    public Vector3 playerDirection;
    SKState currentState;
    public Transform groundcheckpos;
    public LayerMask groundLayer;
    public Collider2D bodycollider;
    public ShaytanPatrollingState skPatrollingState;
    // public ChasingState chasingState;
    public ShaytanAttackingState skAttackingState;
    public GameObject player;
    public GameObject Grenade;
    public PlayerHealth playerHealth;
    public float grenadeSpeed;
    public GameObject shaytankid;
    public int enemyRadius;




    [SerializeField] public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }
    void Start()
    {
        /*Grenade = GameObject.FindGameObjectWithTag("Projectile");*/
        shaytankid = GameObject.FindGameObjectWithTag("ShaytanKid");
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRadius = 4;

        skPatrollingState = new ShaytanPatrollingState();
        skAttackingState = new ShaytanAttackingState(shotDelay, this);
        notShooting = true;
        mustPatrol = true;
        currentState = skPatrollingState;
    }


    void Update()
    {
        //  playerDirection = player.transform.position - bizzaro.transform.position;
        distToPlayer = Vector2.Distance(this.transform.position, player.transform.position);
        currentState.UpdateState(this);



    }


    public void ChangeState(SKState desiredState)
    {
        currentState = desiredState;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyRadius);

    }

    public GameObject Spawnbullet(SKStateManager sKStateManager)
    {
        GameObject thebullet = Instantiate(sKStateManager.Grenade, (Vector2)sKStateManager.transform.position + skAttackingState.directionToPlayer.normalized, Quaternion.identity);
        return thebullet;
    }
    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundcheckpos.position, 0.1f, groundLayer);
        }
    }


    /* public GameObject SpawnBullet(BizzaroStateManager attackingState)
     {

         GameObject bullet = Instantiate(Grenade, transform.position + offset , Quaternion.identity);
         return bullet;
     }
 */
}


public abstract class SKState
{


    public abstract void UpdateState(SKStateManager skStateManager);

}

public class ShaytanPatrollingState : SKState
{

    public override void UpdateState(SKStateManager skStateManager)
    {
        skStateManager.anim.SetBool("moving", true);

        if (skStateManager.mustPatrol)
        {
            if (skStateManager.bodycollider.IsTouchingLayers(skStateManager.groundLayer))
            {
                Flip(skStateManager);
            }
            skStateManager.rb.velocity = new Vector2(skStateManager.walkspeed * Time.fixedDeltaTime, skStateManager.rb.velocity.y);// Patrol()
        }
        Debug.Log("you are patrolling");


        if (skStateManager.distToPlayer <= skStateManager.range)
        {
            skStateManager.ChangeState(skStateManager.skAttackingState);
            Debug.Log("you are attacking now");


        }


    }

    public void Flip(SKStateManager sKStateManager)
    {

        sKStateManager.transform.localScale = new Vector2(sKStateManager.transform.localScale.x * -1, sKStateManager.transform.localScale.y);
        sKStateManager.walkspeed *= -1;

    }

}



public class ShaytanAttackingState : SKState
{
    public float shotTimer;
    public Vector2 directionToPlayer;

    public ShaytanAttackingState(float shotDelay, SKStateManager sKStateManager)
    {
        sKStateManager.shotDelay = shotDelay;
    }
    public override void UpdateState(SKStateManager sKStateManager)
    {

        if (sKStateManager.player.transform.position.x > sKStateManager.transform.position.x && sKStateManager.transform.localScale.x < 0 || sKStateManager.player.transform.position.x < sKStateManager.transform.position.x && sKStateManager.transform.localScale.x > 0)
        {
            sKStateManager.skPatrollingState.Flip(sKStateManager);
        }

        sKStateManager.mustPatrol = false;
        sKStateManager.anim.SetBool("moving", false);
        sKStateManager.rb.velocity = Vector2.zero;
        sKStateManager.notShooting = false;
        DamageWarrior(sKStateManager);
        //  BizzaroMelee(bizzaroStateManager);
        shotTimer += Time.deltaTime;
        Debug.Log("YOU ARE SHOOTING");

        if (sKStateManager.distToPlayer >= sKStateManager.range)
        {
            sKStateManager.notShooting = true;
            sKStateManager.mustPatrol = true;
            sKStateManager.ChangeState(sKStateManager.skPatrollingState);
            Debug.Log("back to patrolling");


        }








    }

    /*public void BizzaroMelee(BizzaroStateManager bizzaroStateManager)
    {
        if(Vector2.Distance(bizzaroStateManager.bizzaro.transform.position, bizzaroStateManager.player.transform.position) <= bizzaroStateManager.enemyRadius)
        {
        bizzaroStateManager.anim.SetTrigger("mace");
            bizzaroStateManager.playerHealth.TakeDamage(20);
            
        }


    }
*/


    public void DamageWarrior(SKStateManager skStateManager)
    {
        if (skStateManager.player != null)
        {

            directionToPlayer = skStateManager.player.transform.position - skStateManager.transform.position;
        }

        if (Vector2.Distance(skStateManager.shaytankid.transform.position, skStateManager.player.transform.position) <= skStateManager.enemyRadius)
        {
            skStateManager.anim.SetTrigger("meleeAttack");
            skStateManager.playerHealth.TakeDamage(20);

        }

        if (skStateManager.player != null && shotTimer >= skStateManager.shotDelay)
        {
            skStateManager.anim.SetTrigger("meleeAttack");
            GameObject thebullet = skStateManager.Spawnbullet(skStateManager);
            Rigidbody2D rb = thebullet.GetComponent<Rigidbody2D>();


            Vector2 direction = directionToPlayer * skStateManager.grenadeSpeed;
            rb.velocity = direction;
            rb.transform.up = direction;

            shotTimer = 0f;

        }

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






