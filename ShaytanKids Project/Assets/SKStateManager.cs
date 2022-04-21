using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKStateManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool mustPatrol, mustTurn;
    public float distToPlayer;
    public float range;
    public float walkspeed;
    SKState skCurrentState;
    public Transform groundcheckpos;
    public LayerMask groundLayer;
    public Collider2D bodycollider;
    public SKPatrollingState skpatrollingState;
    public SKChasingState skchasingstate;
    public SKAttackingState skattackingState;
    public GameObject player;
   


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        skpatrollingState = new SKPatrollingState();
        skattackingState = new SKAttackingState();
        mustPatrol = true;


        skCurrentState = skpatrollingState;
    }


    void Update()
    {
        distToPlayer = Vector2.Distance(this.transform.position, player.transform.position);
        skCurrentState.UpdateState(this);



    }


    public void ChangeState(SKState desiredState)
    {
        skCurrentState = desiredState;
    }

}


public abstract class SKState
{

    public abstract void UpdateState(SKStateManager skStateManager);

}

public class SKPatrollingState : SKState
{

    public override void UpdateState(SKStateManager skStateManager)
    {
       
        if (skStateManager.mustPatrol)
        {
            if (skStateManager.bodycollider.IsTouchingLayers(skStateManager.groundLayer))
            {
                Flip(skStateManager);
            }
            skStateManager.rb.velocity = new Vector2(skStateManager.walkspeed * Time.fixedDeltaTime, skStateManager.rb.velocity.y);// Patrol()
        }


        if (skStateManager.distToPlayer <= skStateManager.range)
        {
            //skStateManager.ChangeState(skStateManager.attackingState);


        } 


    }

    public void Flip(SKStateManager skStateManager)
    {
        
        skStateManager.transform.localScale = new Vector2(skStateManager.transform.localScale.x * -1, skStateManager.transform.localScale.y);
        skStateManager.walkspeed *= -1;
        
    }

}

public class SKChasingState : SKState
{
    public override void UpdateState(SKStateManager skStateManager)
    {





    }
}



public class SKAttackingState : SKState
{
    public override void UpdateState(SKStateManager skStateManager)
    {





    }

 
}