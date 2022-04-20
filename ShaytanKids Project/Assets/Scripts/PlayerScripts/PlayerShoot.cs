using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public GameObject arrow;
    public GameObject arm;
    public float projectileSpeed;
    public bool shouldShoot;
    public Vector3 spawnOffset;
    // Start is called before the first frame update
    void Start()
    {
        arm = GameObject.Find("Arm");
    }

    // Update is called once per frame
    void Update()
    {
        ShootArrow();
    }

   
    
    public void ShootArrow()
    {
        if (arm.GetComponent<AimingRotation>().isAiming && playerAttack.attacking == true)
        {
            shouldShoot = true;
            if (shouldShoot)
            {
                GameObject theArrow = Instantiate(arrow, transform.position + transform.TransformDirection(spawnOffset), this.transform.rotation);
                Rigidbody2D rb = theArrow.GetComponent<Rigidbody2D>();

                Vector2 arrowSpeed = transform.TransformDirection(Vector2.up) * projectileSpeed;

                rb.velocity = arrowSpeed;
            }
        }
    }
    public void StopMeleeAttacks()
    {
        if (arm.GetComponent<AimingRotation>().isAiming)
        {
            playerAttack.enabled = false;
        }
    }
   
}
