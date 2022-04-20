using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject bizzaro;
    public GameObject player;
    public float speed = 10f;

    private float playerX;
    private float bizzaroX;
    private float dist;
    private float nextX;
    private float baseY;
    private float height;
    void Start()
    {
        bizzaro = GameObject.FindGameObjectWithTag("Bizzaro");
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    
    void Update()
    {
        playerX = player.transform.position.x;
        bizzaroX = bizzaro.transform.position.x;

        dist = playerX - bizzaroX;
        nextX = Mathf.MoveTowards(transform.position.x, playerX, speed * Time.deltaTime);
        baseY = Mathf.Lerp(bizzaro.transform.position.y, player.transform.position.y, (nextX - bizzaroX) / dist);
        height = 2 * (nextX - bizzaroX) * (nextX - playerX) / (-0.25f * dist * dist);

        Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
        transform.rotation = LookAtPlayer(movePosition - transform.position);
        transform.position = movePosition;

        


    }

    private static Quaternion LookAtPlayer(Vector2 rotation)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    }
}
