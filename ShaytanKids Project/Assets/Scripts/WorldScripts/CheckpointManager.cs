using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class stores a position for the player to spawn in (set by Checkpoint objects), 
// and has a function to spawn the player there (which should be called when the player dies). 
/// <summary>
/// Handles player respawning and saves the last active checkpoint. There should only be one manager in the scene.
/// </summary>
public class CheckpointManager : MonoBehaviour
{

    static GameObject player;

    public static Vector2 playerSpawnPosition;
    [SerializeField] static Vector2 defaultSpawnpoint; // ideally this would be set to the player's position at the start of the level

    public static CheckpointManager manager;
    private void Awake()
    {
        if (manager == null)
             manager = this; 
        else
             Destroy(this); 


        player = GameObject.FindGameObjectWithTag("Player");
        defaultSpawnpoint = player.transform.position;
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
