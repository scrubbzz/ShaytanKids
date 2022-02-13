using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int movementSpeed;
    public bool moveLeft;
    public bool moveRight;
    public int horizontalInput;

    public int jumpHeight;
    public int jumpCount;
    public bool isGrounded;
    public bool canJump;
    public Rigidbody2D thePlayer;
    // Start is called before the first frame update
    void Start()
    {

        jumpCount = 0;
        isGrounded = true;
        canJump = true;
        thePlayer = GetComponent<Rigidbody2D>();

        movementSpeed = 20;
        jumpHeight = 10;
    }

    // Update is called once per frame
    void Update()
    {
        ReadInputs();
        Move();
        Jump();
        LimitJump();
        //FlipPlayer();
    }
    public void ReadInputs()
    {
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

    }
    public void Move()
    {
        if (moveLeft || moveRight)
        {
            thePlayer.velocity = new Vector2(horizontalInput * movementSpeed, thePlayer.velocity.y);
        }

    }
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
