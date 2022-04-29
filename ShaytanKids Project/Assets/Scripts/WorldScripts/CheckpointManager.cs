using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handles player respawning and saves the last active checkpoint. There should only be one manager in the scene.
/// </summary>
public class CheckpointManager : MonoBehaviour
{

    public static GameObject player;

    public static Vector2 playerSpawnPosition;
    static Vector2 defaultSpawnpoint; // meant to be set to the player's position at the start of the level

    public static CheckpointManager manager;

    void Awake()  //singleton pattern. make sure there's only one manager
    {
        if (manager == null)
             manager = this; 
        else
             Destroy(this); 
    }

    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        defaultSpawnpoint = player.transform.position;
        
        Respawn();  // respawn should be called when the scene is loaded
    }

    public static void Respawn()
    {
        if (playerSpawnPosition == Vector2.zero)
                playerSpawnPosition = defaultSpawnpoint;

        player.transform.position = playerSpawnPosition;
        // call player respawn animation here

        Debug.Log("Player respawned at " + playerSpawnPosition + "."); 
    }

    public static void SetRespawn(Vector2 checkpointPos)
    {
        playerSpawnPosition = checkpointPos;
        Debug.Log("Player spawn position set to " + playerSpawnPosition); 
    }

}
