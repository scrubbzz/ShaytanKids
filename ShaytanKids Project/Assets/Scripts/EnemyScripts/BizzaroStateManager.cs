using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BizzaroStateManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool mustPatrol;
    public bool notShooting;
    public float range, shootspeed, timeBtwShots;
    public float walkspeed;
    public Transform shootPos;
    State currentState;
    public Transform groundcheckpos;
    public LayerMask groundLayer;
    public Collider2D bodycollider;
    public PatrollingState patrollingState;
    // public ChasingState chasingState;
    public AttackingState attackingState;
    public GameObject player;
    public GameObject bullet;

    void Start()
    {
        currentState = new PatrollingState();
        player = GameObject.FindGameObjectWithTag("Player");
        notShooting = true;
        mustPatrol = true;


    }

   
    void Update()
    {
        currentState.UpdateState(this);

    }

    public void ChangeState(State desiredState)
    {
        currentState = desiredState;
    }

}


public abstract class State
{

    public float distToPlayer;



    public abstract void UpdateState(BizzaroStateManager bizzaroStateManager);

}

public class PatrollingState : State
{

    public override void UpdateState(BizzaroStateManager bizzaroStateManager)
    {

        if (bizzaroStateManager.mustPatrol)
        {
            if (bizzaroStateManager.bodycollider.IsTouchingLayers(bizzaroStateManager.groundLayer))
            {
                Flip(bizzaroStateManager);
            }
            bizzaroStateManager.rb.velocity = new Vector2(bizzaroStateManager.walkspeed * Time.fixedDeltaTime, bizzaroStateManager.rb.velocity.y);// Patrol()
        }

        distToPlayer = Vector2.Distance(bizzaroStateManager.transform.position, bizzaroStateManager.player.transform.position);

        if (distToPlayer <= bizzaroStateManager.range)
        {
            bizzaroStateManager.ChangeState(bizzaroStateManager.attackingState);


        } else if(distToPlayer >= bizzaroStateManager.range)
        {
            bizzaroStateManager.mustPatrol = true;
        }
        
            
    }

    private static void Flip(BizzaroStateManager bizzaroStateManager)
    {
        bizzaroStateManager.mustPatrol = false;
        bizzaroStateManager.transform.localScale = new Vector2(bizzaroStateManager.transform.localScale.x * -1, bizzaroStateManager.transform.localScale.y);
        bizzaroStateManager.walkspeed *= -1;
        bizzaroStateManager.mustPatrol = true;
    }

}



public class AttackingState : State
{
    public override void UpdateState(BizzaroStateManager bizzaroStateManager)
    {

        bizzaroStateManager.mustPatrol = false;
        bizzaroStateManager.rb.velocity = Vector2.zero;

        if (bizzaroStateManager.notShooting)
        {
            StartCoroutine(shoot(bizzaroStateManager));
        }
       
        else

            bizzaroStateManager.mustPatrol = true;

        if (bizzaroStateManager.mustPatrol)
        {
            bizzaroStateManager.ChangeState(bizzaroStateManager.patrollingState);
        }
    }


    private void StartCoroutine(IEnumerator enumerator)
    {

    }

    IEnumerator shoot(BizzaroStateManager bizzaroStateManager)
    {
        bizzaroStateManager.notShooting = false;
        yield return new WaitForSeconds(bizzaroStateManager.timeBtwShots);
        GameObject newbullet = GameObject.Instantiate(bizzaroStateManager.bullet, bizzaroStateManager.shootPos.position, Quaternion.identity);
        newbullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bizzaroStateManager.shootspeed * bizzaroStateManager.walkspeed * Time.fixedDeltaTime, 0f);
        Debug.Log("Shoot");
        bizzaroStateManager.notShooting = true;
    }
}






