using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BizzaroStateManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool mustPatrol;
    public bool mustTurn, canShoot;
    public float walkspeed;
    public Transform shootPos;
    State currentState;
    public Transform groundcheckpos;
    public LayerMask groundLayer;
    public Collider2D bodycollider;
    public PatrollingState patrollingState;
    public ChasingState chasingState;
    public GameObject player;
    public GameObject bullet;

    void Start()
    {
        currentState = new PatrollingState();
        player = GameObject.FindGameObjectWithTag("Player");
        canShoot = true;
        mustPatrol = true;

        
    }

    public  void FixedUpdate()
    {

        patrollingState.Turns(this);
        patrollingState.DistanceToPlayer(this);
        
        
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
    public float range, shootspeed, timeBtwShots;
    


    public abstract void UpdateState(BizzaroStateManager bizzaroStateManager);

}

public class PatrollingState : State
{
    
    public override void UpdateState(BizzaroStateManager bizzaroStateManager)
    {

        if (bizzaroStateManager.mustPatrol)
        {
            if (bizzaroStateManager.mustTurn || bizzaroStateManager.bodycollider.IsTouchingLayers(bizzaroStateManager.groundLayer))
            {
               bizzaroStateManager.mustPatrol = false;
               bizzaroStateManager.transform.localScale = new Vector2(bizzaroStateManager.transform.localScale.x * -1, bizzaroStateManager.transform.localScale.y);
                bizzaroStateManager.walkspeed *= -1;
               bizzaroStateManager.mustPatrol = true;
            }
            bizzaroStateManager.rb.velocity = new Vector2(bizzaroStateManager.walkspeed * Time.fixedDeltaTime, bizzaroStateManager.rb.velocity.y);// Patrol()
        }

        distToPlayer = Vector2.Distance(bizzaroStateManager.transform.position,bizzaroStateManager.player.transform.position);

        if(distToPlayer >= range)
        {
            bizzaroStateManager.ChangeState(bizzaroStateManager.chasingState);
        }
        


    }

    public void Turns(BizzaroStateManager bizzaroStateManager)
    {
        if (bizzaroStateManager.mustPatrol)
        {
           bizzaroStateManager.mustTurn = !Physics2D.OverlapCircle(bizzaroStateManager.groundcheckpos.position, 0.1f,bizzaroStateManager.groundLayer);
        }
    }

    public void DistanceToPlayer(BizzaroStateManager bizzaroStateManager)
    {
        if (distToPlayer <= range)
        {
            if (bizzaroStateManager.player.transform.position.x > bizzaroStateManager.transform.position.x && bizzaroStateManager.transform.localScale.x < 0 ||bizzaroStateManager.player.transform.position.x < bizzaroStateManager.transform.position.x && bizzaroStateManager.transform.localScale.x > 0)
            {
                bizzaroStateManager.mustPatrol = false;
                bizzaroStateManager.transform.localScale = new Vector2(bizzaroStateManager.transform.localScale.x * -1, bizzaroStateManager.transform.localScale.y);
                bizzaroStateManager.walkspeed *= -1;
                bizzaroStateManager.mustPatrol = true; 
            }

            bizzaroStateManager.mustPatrol = false;
            bizzaroStateManager.rb.velocity = Vector2.zero;

            

            
        }
        }
    
    private void Flip(BizzaroStateManager bizzaroStateManager)
    {
        bizzaroStateManager.mustPatrol = false;
       bizzaroStateManager.transform.localScale = new Vector2(bizzaroStateManager.transform.localScale.x * -1, bizzaroStateManager.transform.localScale.y);
        bizzaroStateManager.walkspeed *= -1;
      bizzaroStateManager.mustPatrol = true;
    }

}




public class ChasingState : State
{
    public override void UpdateState(BizzaroStateManager bizzaroStateManager)
    {




        if (bizzaroStateManager.mustPatrol)
        {
            bizzaroStateManager.ChangeState(bizzaroStateManager.patrollingState);

        }

    }


    public class AttackingState : State
    {
        public override void UpdateState(BizzaroStateManager bizzaroStateManager)
        {

            if (bizzaroStateManager.canShoot)
            {
            }


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
            bizzaroStateManager.canShoot = false;
            yield return new WaitForSeconds(timeBtwShots);
            GameObject newbullet = GameObject.Instantiate(bizzaroStateManager.bullet, bizzaroStateManager.shootPos.position, Quaternion.identity);
            newbullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootspeed * bizzaroStateManager.walkspeed * Time.fixedDeltaTime, 0f);
            Debug.Log("Shoot");
            bizzaroStateManager.canShoot = true;
        }
    }
}



