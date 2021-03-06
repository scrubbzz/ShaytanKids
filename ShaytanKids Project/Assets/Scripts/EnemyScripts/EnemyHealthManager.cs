using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int health;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        DestroyEnemy();
    }
    public void TakeDamage(float playerAttackDamage)
    {
        health -= (int)playerAttackDamage;
        //Debug.Log(health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Arrow"))
        {
            health -= 33;
            Debug.Log("Health is " + health);
            Destroy(collision.gameObject);
        }
    }
    public void DestroyEnemy()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }


}
