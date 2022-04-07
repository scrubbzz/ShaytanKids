using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingRotation : MonoBehaviour
{
    public PlayerShoot playerShoot;
    public Vector3 mousePosInWorldSpace;
    public Camera camera;
    public Vector2 direction;
    public bool isAiming;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ReadInputs();
        ReadyBow();
        RotateBow();
    }
    public void ReadInputs()
    {
        mousePosInWorldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }
    }
    public void ReadyBow()
    {
        if (isAiming)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void RotateBow()
    {
         direction = new Vector2(mousePosInWorldSpace.x - transform.position.x, mousePosInWorldSpace.y - transform.position.y);
        transform.up = direction;
    }
}
