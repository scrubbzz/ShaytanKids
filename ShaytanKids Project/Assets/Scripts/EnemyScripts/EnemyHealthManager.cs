using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        //health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyEnemy();
    }
    public void TakeDamage(int playerAttackDamage)
    {
        health -= playerAttackDamage;
        Debug.Log(health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Arrow"))
        {
            health -= 33;
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
