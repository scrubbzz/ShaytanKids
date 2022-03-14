using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybullet : MonoBehaviour
{
    public float dieTime, damage;
    public GameObject diePeect;
    void Start()
    {
        StartCoroutine(CountdownTimer());
    }

   
    private void OnCollisionEnter2D(Collision2D col)
    {

        Die();
        Debug.Log("Die");
    }


    IEnumerator CountdownTimer()
    {

        yield return new WaitForSeconds(dieTime);
        Die();
    }


    void Die()
    {
        Destroy(gameObject);

    }
}
