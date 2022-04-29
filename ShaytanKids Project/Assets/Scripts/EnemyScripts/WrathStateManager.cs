using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrathStateManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool mustPatrol, mustTurn;
    public float distToPlayer;
    public bool notHitting;
    public float detectionRange;
    public float hitRange;
    public float shotDelay;
    public float walkspeed;
    WrathState currentState;
    public Transform groundcheckpos;
    public LayerMask groundLayer;
    public Collider2D bodycollider;
    public WrathPatrollingState wrathPatrollingState;
    public WrathChasingState WrathChasingState;
    public WrathAttackingState wrathAttackingState;
    public GameObject player;
    public GameObject hit;
    public float hitSpeed;
    public PlayerHealth playerHealth;
    public GameObject wrath;
    public int wrathRadius;
    [SerializeField] public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }
    void Start()
    {

        wrath = GameObject.FindGameObjectWithTag("Wrath");
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        wrathRadius = 4;
        wrathPatrollingState = new WrathPatrollingState();
        WrathChasingState = new WrathChasingState();
        wrathAttackingState = new WrathAttackingState(shotDelay, this);
        notHitting = true;
        mustPatrol = true;
        currentState = wrathPatrollingState;

        wrathRadius = (int)hitRange;
    }


    void Update()
    {

        distToPlayer = Vector2.Distance(this.transform.position, player.transform.position);
        currentState.UpdateState(this);



    }


    public void ChangeState(WrathState desiredState)
    {
        currentState = desiredState;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, wrathRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

    }

  
    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundcheckpos.position, 0.1f, groundLayer);
        }
    }



}
public abstract class WrathState
{

    public abstract void UpdateState(WrathStateManager wrathStateManager);

}

public class WrathPatrollingState : WrathState
{

    public override void UpdateState(WrathStateManager wrathStateManager)
    {
        wrathStateManager.anim.SetBool("moving", true);

        if (wrathStateManager.mustPatrol)
        {
            if (wrathStateManager.bodycollider.IsTouchingLayers(wrathStateManager.groundLayer))
            {
                Flip(wrathStateManager);
            }
            wrathStateManager.rb.velocity = new Vector2(wrathStateManager.walkspeed * Time.fixedDeltaTime, wrathStateManager.rb.velocity.y);// Patrol()
        }
        Debug.Log("you are patrolling");


        if (wrathStateManager.distToPlayer <= wrathStateManager.detectionRange)
        {
            wrathStateManager.ChangeState(wrathStateManager.WrathChasingState);
            Debug.Log("you are Chasing now");
        }


    }


    public void Flip(WrathStateManager wrathStateManager)
    {
        wrathStateManager.transform.localScale = new Vector2(wrathStateManager.transform.localScale.x * -1, wrathStateManager.transform.localScale.y);
        wrathStateManager.walkspeed *= -1;
    }
}


public class WrathChasingState : WrathState
{
    public override void UpdateState(WrathStateManager wrathStateManager)
    {
        wrathStateManager.anim.SetBool("moving", true);

        if (wrathStateManager.player.transform.position.x > wrathStateManager.transform.position.x && wrathStateManager.transform.localScale.x < 0 || wrathStateManager.player.transform.position.x < wrathStateManager.transform.position.x && wrathStateManager.transform.localScale.x > 0)
        {

            wrathStateManager.wrathPatrollingState.Flip(wrathStateManager);
        }

        wrathStateManager.mustPatrol = false;
        wrathStateManager.notHitting = false;


        if (wrathStateManager.distToPlayer <= wrathStateManager.hitRange)
        {

            wrathStateManager.ChangeState(wrathStateManager.wrathAttackingState);
        }


        if (wrathStateManager.distToPlayer >= wrathStateManager.detectionRange)
        {
            wrathStateManager.notHitting = true;
            wrathStateManager.mustPatrol = true;
            wrathStateManager.ChangeState(wrathStateManager.wrathPatrollingState);
            Debug.Log("back to patrolling");


        }



    }



}

public class WrathAttackingState : WrathState
{
    public float meleeTimer;
    public Vector2 directionToPlayer;

    public WrathAttackingState(float shotDelay, WrathStateManager wrathStateManager)
    {
        wrathStateManager.shotDelay = shotDelay;

    }
    public override void UpdateState(WrathStateManager wrathStateManager)
    {
        wrathStateManager.anim.SetBool("moving", false);
        wrathStateManager.rb.velocity = Vector2.zero;
        DamageTarget(wrathStateManager);
        meleeTimer += Time.deltaTime;
        Debug.Log("YOU ARE Hitting");

        if (wrathStateManager.distToPlayer >= wrathStateManager.detectionRange)
        {
            wrathStateManager.notHitting = true;
            wrathStateManager.mustPatrol = true;
            wrathStateManager.ChangeState(wrathStateManager.wrathPatrollingState);
            Debug.Log("back to patrolling");


        }

        if (wrathStateManager.distToPlayer >= wrathStateManager.hitRange && wrathStateManager.distToPlayer <= wrathStateManager.detectionRange)
        {
            wrathStateManager.ChangeState(wrathStateManager.WrathChasingState);
            Debug.Log("back to Chasing");
        }







    }








    public void DamageTarget(WrathStateManager wrathStateManager)
    {
        if (wrathStateManager.player != null)
        {

            directionToPlayer = wrathStateManager.player.transform.position - wrathStateManager.transform.position;
        }

        if (Vector2.Distance(wrathStateManager.wrath.transform.position, wrathStateManager.player.transform.position) <= wrathStateManager.wrathRadius)
        {
            if (meleeTimer > 2)
            {
                wrathStateManager.anim.SetTrigger("slash");
                wrathStateManager.playerHealth.TakeDamage(30);
                wrathStateManager.playerHealth.tookDamage = true;
                wrathStateManager.playerHealth.currentHealthRegenTimer = wrathStateManager.playerHealth.healthRegenTimer;
                meleeTimer = 0;
            }





        }





    }
}