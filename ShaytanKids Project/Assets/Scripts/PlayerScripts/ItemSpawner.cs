using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject itemToSpawn;

    [SerializeField] int buffer = 50; // value for how far up potions should spawn.

    float spawnTimer = 0;
    float spawnDelay = 5;
    [SerializeField] float maxSpawnDelay = 15; 
    [SerializeField] float minSpawnDelay = 10;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnDelay)
        {
            spawnTimer = 0;
            spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

            SpawnItem();
        }
    }

    // get the height of the screen, pick a random point along the x axis of the screen, 
    // translate to worldspace via Unity function, and spawn the 
    void SpawnItem()
    {
        Vector3 currentSpawnPos = new Vector3(
            Random.Range(0, Screen.width), Screen.height + buffer, 1); 
        

        Instantiate(itemToSpawn, Camera.main.ScreenToWorldPoint(currentSpawnPos), Quaternion.identity);
    }
}
