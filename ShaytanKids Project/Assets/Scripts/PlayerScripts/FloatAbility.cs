using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAbility : MonoBehaviour
{
    
    public PlayerMovement PlayerMovement;
    public bool notFloating;
    public float floatSpeed;
    public float currentFloatTimer;
    public float maxFloatTimer;
    public float floatCooldown;
 private Rigidbody2D rb;
    public float gravity;


   //private GameObject flyingPosition;
    void Start()
    {
        notFloating = true;
          rb = GetComponent<Rigidbody2D>();
        currentFloatTimer = maxFloatTimer;

        //flyingPosition = GameObject.Find("Floatspot");
        //flyingPosition = GameObject.FindGameObjectWithTag("Floatspot");

    }


    private void FixedUpdate()
    {
        Checkfloating();
        Float();
        FloatTimer();

    }

    public void Float()
    {
            if(PlayerMovement.isGrounded == false && !notFloating )
            {
                Vector2 floatStrength = new Vector2(rb.velocity.x, gravity);
                rb.velocity = floatStrength;


            }

    }

    public void FloatTimer()
    {
        if (PlayerMovement.isGrounded == false && !notFloating && Input.GetKey(KeyCode.P))
        {
            currentFloatTimer -= Time.deltaTime;
            if(currentFloatTimer <= 0)
            {
                notFloating = true;
                

            }


           
        }

        if(PlayerMovement.isGrounded == true)
        {
            currentFloatTimer = maxFloatTimer;
        }
    }
        public void Checkfloating()
    {

        if ( Input.GetKey(KeyCode.P) && currentFloatTimer > 0)
        {
                notFloating = false;
            
                //transform.position = Vector2.MoveTowards(transform.position, flyingPosition.transform.position, 50 * Time.deltaTime);
               
            

        }
        else
        {
            notFloating = true;
        }

    }
}
