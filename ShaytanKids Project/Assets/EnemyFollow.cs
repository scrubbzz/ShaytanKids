using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    public float speed;
    public float range;
    float distToPlayer;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        distToPlayer = Vector2.Distance(this.transform.position, target.transform.position);
        if(distToPlayer < range)
        {
        ChasePlayer(); 
        }

    }

    private void ChasePlayer()
    {  
        var targetPos = new Vector2(target.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

    }
    /*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    public float speed;
    public float range;
    float distToPlayer;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        distToPlayer = Vector2.Distance(this.transform.position, target.transform.position);
        if (distToPlayer <= range)
        {
            ChasePlayer();
        }

    }

    private void ChasePlayer()
    {
        float delta = Mathf.Sign(target.position.x - transform.position.x);
        transform.localScale = new Vector2(
          transform.localScale.x * delta, transform.localScale.y);
        var targetPos = new Vector2(target.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }


    private void Flip()
    {
        transform.localScale = new Vector2(
          transform.localScale.x * Mathf.Sign(speed), transform.localScale.y);
        speed *= 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

    }
}*/
}
