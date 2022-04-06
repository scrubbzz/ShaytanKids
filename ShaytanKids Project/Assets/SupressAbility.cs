using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupressAbility : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Archangel")
        {
            var angel = collision.gameObject.GetComponent<ArchangelMovement>();
            angel.movementSpeed /= 4;
            Debug.Log("angel is move speed is " + angel.movementSpeed);
        }
    }
}
