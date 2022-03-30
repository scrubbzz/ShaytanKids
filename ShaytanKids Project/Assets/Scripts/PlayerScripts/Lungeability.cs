using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lungeability : MonoBehaviour
{
    /*  public PlayerMovement playerMovement;
      public AimingRotation aimingRotation;
      private Rigidbody2D rb;
      public float Lungespeed;
      private float LungeTime;
      public float startLungeTime;
      public bool isAiming;

      void Start()
      {
          rb = GetComponent<Rigidbody2D>();
          LungeTime = startLungeTime;

      }


      void Update()
      {
          ReadInput();
          Lunge();


      }

      public void ReadInput()
      {
          if (Input.GetMouseButton(1))
          {
              isAiming = true;
          }else
          {
              isAiming = false;
          }

      }
      public void Lunge()
      {
          if(isAiming && Input.GetMouseButtonUp(0)){
              Vector2 lungespeed = new Vector2(rb.velocity.x, aimingRotation.direction.y);
              rb.velocity = lungespeed;
          }

      }
      */
    private Rigidbody2D rb;

    public bool notLunging;
    public float lungeForce = 25.0f;
    public float lungeCooldown = 10;
    public float lungeTimer = 10;
    // public AimingRotation aimingRotation;
    public Camera cam;
    Vector2 mousePos;
    Vector2 Playerposition;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        lungeForce = 100;
        notLunging = true;
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        if (notLunging)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Playerposition = new Vector2(transform.position.x, transform.position.y);
                
                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                //  transform.position = mousePos;
                rb.AddForce((mousePos - Playerposition) * lungeForce );
                notLunging = false;
                lungeTimer = 0.0f;
                
            }
        }


        lungeTimer += Time.deltaTime; //Makes timer increase
        if (lungeTimer >= lungeCooldown)
        {
            lungeTimer = lungeCooldown; //Stops the timer
        }
    }

    void FixedUpdate()
    {
        if (lungeTimer >= lungeCooldown)
            notLunging = true;
    }
}


