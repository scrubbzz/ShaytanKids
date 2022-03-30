using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SupressionAbility : Ability
{

    DestroyProjectiles destroyProjectiles;
  
    private void Awake()
    {
      
    }
    public override void Activate()
    {
        if(destroyProjectiles.Collided == true)
        {
            var collision = destroyProjectiles.GetComponent<Collision>();

        }

    }
    /* private void Awake()
     {
         arrows = GameObject.FindGameObjectsWithTag("Arrow");
         enemyLayerMask = LayerMask.GetMask("Supression");
     }
     public override void Activate()
     {
         for (int i = 0; i < arrows.Length; i++)
         {
             Collider2D[] collider = Physics2D.OverlapBoxAll(arrows[i].transform.position, new Vector2(20, 20), 0, enemyLayerMask);
             if (collider != null)
             {
                 enemy = collider[i].gameObject;

                 var enemySpeed = enemy.GetComponent<ArchangelMovement>();
                 enemySpeed.movementSpeed = 1;
                 Debug.Log(enemySpeed.movementSpeed);
             }
         }
     }*/
}
