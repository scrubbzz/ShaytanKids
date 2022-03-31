using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public GameObject arrow;
    public int projectileSpeed;

    public bool isAiming;
    public bool shouldShoot;
    public Vector3 spawnOffset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ReadInputs();
        ShootArrow();
        //StopMeleeAttacks();
    }

    public void ReadInputs()
    {
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }
    }
    public void ShootArrow()
    {
        if (isAiming && playerAttack.attacking == true)
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
        if (isAiming)
        {
            playerAttack.enabled = false;
        }
    }
   
}
