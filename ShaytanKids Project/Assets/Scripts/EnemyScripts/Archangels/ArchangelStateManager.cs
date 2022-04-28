using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchangelStateManager : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public State currentMovement;
    public PatrollingState patrollingState;
    public ChasingState chasingState;
    public AttackingState attackingState;
    //Variables for patrolling state.
    public float distanceToPlayer;
    public int index;
    public Vector3[] flightPositions;
    public float movementSpeed;
    public float changePosTimerLength;
    //Variables for chasing state.
    public GameObject Target;//Target is the player when we need to follow it.
    public LayerMask playerLayerMask;
    public int detectionRadius;
    public float chaseTimerLength;
    //Variables for attacking state.
    public float fireDelayLength;
    public int projectileSpeed;
    public GameObject projectile;
    public int shootingRadius;
    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

        Target = GameObject.FindGameObjectWithTag("Player");
        chasingState = new ChasingState(chaseTimerLength);
        patrollingState = new PatrollingState(changePosTimerLength);
        attackingState = new AttackingState(fireDelayLength, this);
        currentMovement = patrollingState;

        flightPositions = new Vector3[]
        {
            new Vector3(-10, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3 (10, -1, 0)
        };

        movementSpeed = 10;
        changePosTimerLength = 4;
        detectionRadius = 30;
        chaseTimerLength = 10;
        fireDelayLength = 2;
        projectileSpeed = 5;
        //projectile = GameObject.FindGameObjectWithTag("EnemyArrow");
        shootingRadius = 18;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(this.transform.position, Target.transform.position);
        currentMovement.UpdateState(this);
    }
    public void ChangeState(State desiredState)
    {
        Debug.Log(desiredState.ToString());
        currentMovement = desiredState;
    }
    public GameObject SpawnProjectile(ArchangelStateManager stateManager)
    {
        GameObject theProjectile = Instantiate(stateManager.projectile, (Vector2)stateManager.transform.position + attackingState.directionToTarget.normalized, /*+ spawnOffset,*/ Quaternion.identity);
        return theProjectile;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootingRadius);
    }
    
}
public abstract class State
{
    public abstract void UpdateState(ArchangelStateManager stateManager);

}
public class PatrollingState : State
{
    public float changePosTimer;
    public Vector3 targetPosition;

    public PatrollingState(float changePosTimerLength)
    {
        changePosTimer = changePosTimerLength;
    }
    public override void UpdateState(ArchangelStateManager stateManager)
    {
        changePosTimer -= Time.deltaTime;
        if (changePosTimer <= 0)
        {
            stateManager.index = Random.Range(0, stateManager.flightPositions.Length);
            //targetPosition = stateManager.flightPositions[0] + stateManager.transform.position;
            targetPosition = stateManager.transform.position + stateManager.flightPositions[stateManager.index];
            changePosTimer = stateManager.changePosTimerLength;
        }
        Rigidbody2D rb = stateManager.GetComponent<Rigidbody2D>();
        rb.position = Vector2.MoveTowards(rb.position, targetPosition, stateManager.movementSpeed * Time.deltaTime);
        
        float playerDistance = Vector2.Distance(stateManager.transform.position, stateManager.Target.transform.position);
        
        if(playerDistance < stateManager.shootingRadius)
        {
            stateManager.ChangeState(stateManager.attackingState);
        }
        else if(playerDistance < stateManager.detectionRadius && stateManager.playerAttack.attacking)
        {
            stateManager.ChangeState(stateManager.chasingState);
        }
    }
}

public class ChasingState : State
{
    public float chaseTimer;
    public ChasingState(float chaseTimerLength)
    {
        chaseTimer = chaseTimerLength;
    }
    public override void UpdateState(ArchangelStateManager stateManager)
    {
        
        chaseTimer -= Time.deltaTime;
        Rigidbody2D rb = stateManager.GetComponent<Rigidbody2D>();
        if(stateManager.Target != null)
        {
            stateManager.distanceToPlayer = Vector2.Distance(rb.position, stateManager.Target.transform.position);
        } 

        if (stateManager.distanceToPlayer > 20 && stateManager.distanceToPlayer < stateManager.detectionRadius)
        {
            rb.transform.position = Vector2.MoveTowards(rb.position, stateManager.Target.transform.position, stateManager.movementSpeed * Time.deltaTime);
            //Debug.Log(transform.position);
        }
       
        Debug.Log("You are in chasing State");
        if (chaseTimer <= 0 || stateManager.distanceToPlayer > stateManager.detectionRadius)
        {
            Debug.Log("You are now patrolling");
            chaseTimer = stateManager.chaseTimerLength;
            stateManager.ChangeState(stateManager.patrollingState);
        }
        else if(stateManager.distanceToPlayer < stateManager.shootingRadius)
        {
            stateManager.ChangeState(stateManager.attackingState);
        }
    }
}

public class AttackingState : State
{
    public float fireTimer;
    public Vector2 directionToTarget;

    public AttackingState(float fireDelayLength, ArchangelStateManager stateManager)
    {
        stateManager.fireDelayLength = fireDelayLength;
    }
    public override void UpdateState(ArchangelStateManager stateManager)
    {
       
        DamagePlayer(stateManager);
        fireTimer += Time.deltaTime; 
        if(stateManager.distanceToPlayer > stateManager.detectionRadius)
        {
            stateManager.ChangeState(stateManager.patrollingState);
        }
        else if(stateManager.distanceToPlayer > stateManager.shootingRadius)
        {
            stateManager.ChangeState(stateManager.chasingState);
        }
    }
   
    public void DamagePlayer(ArchangelStateManager stateManager)
    {
        if (stateManager.Target != null)
        {
            directionToTarget = stateManager.Target.transform.position - stateManager.transform.position;
        }

        if (stateManager.Target != null && fireTimer >= stateManager.fireDelayLength)
        {
            GameObject theProjectile = stateManager.SpawnProjectile(stateManager);
            Rigidbody2D rb = theProjectile.GetComponent<Rigidbody2D>();

            Vector2 direction = directionToTarget * stateManager.projectileSpeed;
            rb.velocity = direction;
            rb.transform.up = direction;

            fireTimer = 0;
            Debug.Log("The player is here");
        }


    }

   
}




