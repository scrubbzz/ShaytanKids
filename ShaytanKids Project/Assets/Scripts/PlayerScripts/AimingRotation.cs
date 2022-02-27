using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingRotation : MonoBehaviour
{
    public PlayerShoot playerShoot;
    public Vector3 mousePosInWorldSpace;
    public Camera camera;
   
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
        RotatePlayer();
    }
    public void HandleInputs()
    {
        mousePosInWorldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public void RotatePlayer()
    {
        Vector2 direction = new Vector2(mousePosInWorldSpace.x - transform.position.x, mousePosInWorldSpace.y - transform.position.y);
        transform.up = direction;
    }
}
