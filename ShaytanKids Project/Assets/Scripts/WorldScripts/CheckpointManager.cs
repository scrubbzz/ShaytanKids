using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class stores a position for the player to spawn in (set by Checkpoint objects), 
// and has a function to spawn the player there (which should be called when the player dies). 
public class CheckpointManager : MonoBehaviour
{
    public CheckpointManager manager;
    static GameObject player;

    public static Vector2 playerSpawnPosition;
    static Vector2 defaultSpawnpoint;

    private void Awake()
    {
        if (manager == null)
            manager = this;
        else
            Destroy(this);

        // gets reference to the player position. 
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public static void Respawn()
    {
        if (playerSpawnPosition == null) // just to make sure a spawn point exists 
            playerSpawnPosition = defaultSpawnpoint;

        player.transform.position = playerSpawnPosition;
        Debug.Log("Player respawned at " + playerSpawnPosition + "."); 
    }

    public static void SetRespawn(Vector2 checkpointPos)
    {
        playerSpawnPosition = checkpointPos;
        Debug.Log("Player spawn position set to " + playerSpawnPosition); 
    }

}
