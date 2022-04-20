using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    int numberOfKeysNeeded;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Player") && 
            ItemCounter.gateKeyCount >= numberOfKeysNeeded)
        {
            ItemCounter.gateKeyCount = ItemCounter.gateKeyCount - numberOfKeysNeeded;
            SceneManager.LoadScene(1);
        }

    }
}
