using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{
    Transform target;
      public GameObject thePlayer;
    public PlayerHealth playerHealth;
    public int KidDamage = 10;
    public Transform borderCheck;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);

        }
    }
}
