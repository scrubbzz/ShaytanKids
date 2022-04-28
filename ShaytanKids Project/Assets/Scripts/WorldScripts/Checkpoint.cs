using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the trigger zone for a Checkpoint prefab. (The spawn location is 
/// a separate child object in the prefab, and should be placed manually.)
/// </summary>
public class Checkpoint : MonoBehaviour
{
    Vector2 checkpointPosition;
    bool hasBeenUsed = false;

    void Start()
    {
        checkpointPosition = gameObject.transform.GetChild(0).position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasBeenUsed)
        {
            CheckpointManager.SetRespawn(checkpointPosition);
            hasBeenUsed = true;
        }
    }


}
