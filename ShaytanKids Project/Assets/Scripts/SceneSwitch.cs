using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] int numberOfKeysNeeded;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Player") && 
            ItemCounter.gateKeyCount >= numberOfKeysNeeded)
        {
            ItemCounter.gateKeyCount =- numberOfKeysNeeded;
            CheckpointManager.playerSpawnPosition = Vector2.zero; // set spawn pos to 0 so the Respawn function
                                                                  // can set it to the default spawn point.

            SceneManager.LoadScene(1);
        }

    }
}
