using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupressAbility : MonoBehaviour
{
    public float supressionTimer;
    Vector2 detectorSize = new Vector2(30, 30);
    public LayerMask archangelLayermask;
    public bool hitPlayer;
    // Start is called before the first frame update
    void Start()
    {
        supressionTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(hitPlayer == false)
        {
            supressionTimer = 5f;
        }
    }
    public void StartSupressionTimer()
    {
        Collider2D collider = Physics2D.OverlapBox(this.transform.position, detectorSize, 0, archangelLayermask);
        if(collider != null)
        {
            collider.GetComponent<ArchangelMovement>().movementSpeed /= 4;
            supressionTimer -= Time.deltaTime;
        }
       
        if(supressionTimer <= 0)
        {
            supressionTimer = 5f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Archangel")
        {
            hitPlayer = true;
            var angel = collision.gameObject.GetComponent<ArchangelMovement>();
            angel.movementSpeed /= 4;
            supressionTimer -= Time.deltaTime;
            if(supressionTimer <= 0)
            {
                hitPlayer = false;
            }
            Debug.Log("angel is move speed is " + angel.movementSpeed);
        }
    }
}
