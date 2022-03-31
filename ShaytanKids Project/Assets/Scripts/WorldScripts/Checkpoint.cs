using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Vector2 checkpointPosition;

    void Start()
    {
        checkpointPosition = this.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")
            && CheckpointManager.playerSpawnPosition != checkpointPosition)
        {
            CheckpointManager.SetRespawn(checkpointPosition);
            Debug.Log("Checkpoint set to " + this.gameObject.name); 
        }
    }


}
