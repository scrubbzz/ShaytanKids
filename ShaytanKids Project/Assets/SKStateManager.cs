using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKStateManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public float distToPlayer;
    public float attackRange;
    public float detecRange;
    public bool mustPatrol, mustTurn;
    public int enemyRadius;
    public float stabDelay;
    public bool notStabbing;
    public float walkspeed;
    public Vector3 playerDirection;
    SKState currentState;
    public Transform groundcheckpos;
    public LayerMask groundLayer;
    public PlayerHealth playerHealth;
    public Collider2D bodycollider;
    public ShaytanPatrollingState skPatrollingState;
    public ShaytanChasingState skChasingState;
    public ShaytanAttackingState skAttackingState;
    public GameObject player;
    public GameObject shaytankid;




    [SerializeField] public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        /*Grenade = GameObject.FindGameObjectWithTag("Projectile");*/
        shaytankid = GameObject.FindGameObjectWithTag("ShaytanKid");
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();



        enemyRadius = 4;

        skPatrollingState = new ShaytanPatrollingState();
        skChasingState = new ShaytanChasingState();
        skAttackingState = new ShaytanAttackingState(stabDelay, this);
        notStabbing = true;
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
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, enemyRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detecRange);

    }


    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundcheckpos.position, 0.1f, groundLayer);
        }
    }

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


        if (skStateManager.distToPlayer <= skStateManager.detecRange)
        {
            skStateManager.ChangeState(skStateManager.skChasingState);
            Debug.Log("you are chasing now");


        }


    }

    public void Flip(SKStateManager sKStateManager)
    {
        sKStateManager.transform.localScale = new Vector2(sKStateManager.transform.localScale.x * -1, sKStateManager.transform.localScale.y);
        sKStateManager.walkspeed *= -1;
    }

}

public class ShaytanChasingState : SKState
{
    public override void UpdateState(SKStateManager skStateManager)
    {
        skStateManager.anim.SetBool("moving", true);

        if (skStateManager.player.transform.position.x > skStateManager.transform.position.x && skStateManager.transform.localScale.x < 0 || skStateManager.player.transform.position.x < skStateManager.transform.position.x && skStateManager.transform.localScale.x > 0)
        {

            skStateManager.skPatrollingState.Flip(skStateManager);
        }

        //if (skStateManager.transform.position != skStateManager.player.transform.position)
        //{ 
        //skStateManager.rb.velocity = Vector3.MoveTowards(skStateManager.transform.position, skStateManager.player.transform.position, (skStateManager.walkspeed * 2) * Time.deltaTime);
        //}


        skStateManager.mustPatrol = false;
        skStateManager.notStabbing = false;


        if (skStateManager.distToPlayer <= skStateManager.attackRange)
        {

            skStateManager.ChangeState(skStateManager.skAttackingState);
        }


        if (skStateManager.distToPlayer >= skStateManager.detecRange)
        {
            skStateManager.notStabbing = true;
            skStateManager.mustPatrol = true;
            skStateManager.ChangeState(skStateManager.skPatrollingState);
            Debug.Log("back to patrolling");
        }
    }

}


public class ShaytanAttackingState : SKState
{
    public float meleeTimer;
    public Vector2 directionToPlayer;

    public ShaytanAttackingState(float shotDelay, SKStateManager sKStateManager)
    {
        sKStateManager.stabDelay = shotDelay;

    }
    public override void UpdateState(SKStateManager sKStateManager)
    {
        sKStateManager.anim.SetBool("moving", false);
        sKStateManager.rb.velocity = Vector2.zero;
        DamageTarget(sKStateManager);
        meleeTimer += Time.deltaTime;
        Debug.Log("YOU ARE Hitting");

        if (sKStateManager.distToPlayer >= sKStateManager.detecRange)
        {
            sKStateManager.notStabbing = true;
            sKStateManager.mustPatrol = true;
            sKStateManager.ChangeState(sKStateManager.skPatrollingState);
            Debug.Log("back to patrolling");


        }

        if (sKStateManager.distToPlayer >= sKStateManager.attackRange && sKStateManager.distToPlayer <= sKStateManager.detecRange)
        {
            sKStateManager.ChangeState(sKStateManager.skChasingState);
            Debug.Log("back to Chasing");
        }
    }


    public void DamageTarget(SKStateManager sKStateManager)
    {
        if (sKStateManager.player != null)
        {

            directionToPlayer = sKStateManager.player.transform.position - sKStateManager.transform.position;
        }

        if (Vector2.Distance(sKStateManager.shaytankid.transform.position, sKStateManager.player.transform.position) <= sKStateManager.enemyRadius)
        {
            if (meleeTimer > 2)
            {
                sKStateManager.anim.SetTrigger("meleeAttack");
                sKStateManager.playerHealth.TakeDamage(10);
                sKStateManager.playerHealth.tookDamage = true;
                sKStateManager.playerHealth.currentHealthRegenTimer = sKStateManager.playerHealth.healthRegenTimer;
                meleeTimer = 0;
            }

        }



    }
}
    
//public class ShaytanAttackingState : SKState
//{
//    public float shotTimer;
//    public Vector2 directionToPlayer;

//    public ShaytanAttackingState(float shotDelay, SKStateManager sKStateManager)
//    {
//        sKStateManager.shotDelay = shotDelay;
//    }
//    public override void UpdateState(SKStateManager sKStateManager)
//    {

//        if (sKStateManager.player.transform.position.x > sKStateManager.transform.position.x && sKStateManager.transform.localScale.x < 0 || sKStateManager.player.transform.position.x < sKStateManager.transform.position.x && sKStateManager.transform.localScale.x > 0)
//        {
//            sKStateManager.skPatrollingState.Flip(sKStateManager);
//        }

//        sKStateManager.mustPatrol = false;
//        sKStateManager.anim.SetBool("moving", false);
//        sKStateManager.rb.velocity = Vector2.zero;
//        sKStateManager.notShooting = false;
//        DamageWarrior(sKStateManager);
//        //  BizzaroMelee(bizzaroStateManager);
//        shotTimer += Time.deltaTime;
//        Debug.Log("YOU ARE SHOOTING");

//        if (sKStateManager.distToPlayer >= sKStateManager.detecRange)
//        {
//            sKStateManager.notShooting = true;
//            sKStateManager.mustPatrol = true;
//            sKStateManager.ChangeState(sKStateManager.skPatrollingState);
//            Debug.Log("back to patrolling");


//        }








//    }




//    public void DamageWarrior(SKStateManager skStateManager)
//    {
//        if (skStateManager.player != null)
//        {

//            directionToPlayer = skStateManager.player.transform.position - skStateManager.transform.position;
//        }

//        if (Vector2.Distance(skStateManager.shaytankid.transform.position, skStateManager.player.transform.position) <= skStateManager.enemyRadius)
//        {
//            skStateManager.anim.SetTrigger("meleeAttack");
//            skStateManager.playerHealth.TakeDamage(20);

//        }

//    }




//}






