using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float currentMoveSpeed;
    public int maxMoveSpeed;
    public bool moveLeft;
    public bool moveRight;
    public int horizontalInput;

    /*public bool shouldSprint;
    public int sprintSpeed;
*/
    public int jumpHeight;
    public int jumpCount;
    public bool isGrounded;
    public bool canJump;

    public bool shouldSlide;
    public int slidingPower;
    public Rigidbody2D thePlayer;
    // Start is called before the first frame update
    void Start()
    {

        jumpCount = 0;
        isGrounded = true;
        canJump = true;
        thePlayer = GetComponent<Rigidbody2D>();

        maxMoveSpeed = 20;
        jumpHeight = 10;
    }

    // Update is called once per frame
    void Update()
    {
        ReadInputs();
        Move();
        //Sprint();
        Jump();
        LimitJump();
        Slide();
        //FlipPlayer();
    }
    public void ReadInputs()
    {
        //For moving
        if (Input.GetKey(KeyCode.A))
        {
            moveLeft = true;
            horizontalInput = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveRight = true;
            horizontalInput = 1;
        }
        else
        {
            horizontalInput = 0;
        }
        //For sprinting
      /*  if (Input.GetKey(KeyCode.LeftShift))
        {
            shouldSprint = true;
        }
        if(shouldSprint && Input.GetKey(KeyCode.LeftShift))
        {
            shouldSprint = false;
        }*/
       //For sliding
        if(isGrounded && Input.GetKey(KeyCode.S))
        {
            shouldSlide = true;
        }
        else
        {
            shouldSlide = false;
        }

    }
    public void Move()
    {
        if (moveLeft || moveRight)
        {
            thePlayer.velocity = new Vector2(horizontalInput * currentMoveSpeed, thePlayer.velocity.y);
        }

    }
   /* public void Sprint()
    {
        if(shouldSprint && moveLeft || shouldSprint && moveRight)
        {
            thePlayer.velocity = new Vector2(horizontalInput * sprintSpeed, thePlayer.velocity.y);
        }
    }*/
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            jumpCount++;
            thePlayer.velocity = new Vector2(thePlayer.velocity.x, jumpHeight);
            isGrounded = false;

        }
    }
    public void LimitJump()
    {
        if (isGrounded == false)
        {
            if (jumpCount > 1)
            {
                canJump = false;
            }
        }
    }
    public void Slide()
    {
        if (shouldSlide)
        {
            currentMoveSpeed -= slidingPower * Time.deltaTime;
        }
        else
        {
            currentMoveSpeed = maxMoveSpeed;
        }
        if(shouldSlide && currentMoveSpeed < 0.001)
        {
            currentMoveSpeed = 0;
        }
        //Debug.Log("Speed is " + currentMoveSpeed);
    }
    public void FlipPlayer()
    {
        if (horizontalInput > 0)
        {
            transform.localScale = Vector3.one * 15;
        }
        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-15, 15, 15);
        }
    }
   /* private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = 1;
        }
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            canJump = true;
            jumpCount = 0;
        }
    }



}
