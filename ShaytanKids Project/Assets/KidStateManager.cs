/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidStateManager : MonoBehaviour
{
    public GameObject MeleeEnemy;
    public GameObject[] patrolPoints;
    public float speed;
    public int damage;
    State currentState;
    public AttackState attacking;
    public PatrollingState patrolling;
    public ChasingState chasing;
    public int patrolIndex;

    void Start()
    {
        currentState = new PatrollingState();
        patrolIndex = 0;
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
    public abstract void UpdateState(KidStateManager stateManager);
}
public class PatrollingState : State
{

    public override void UpdateState(KidStateManager movementManager)
    {

       

    }

}
public class AttackState : State
{
    public override void UpdateState(KidStateManager movementManager)
    {

    }
}

 public class ChasingState : State
    {
        public override void UpdateState(KidStateManager movementManager)
        {

        }

    }
   */