using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // currently this script works with individual gameObjects, which spawn items from
    // their position, and can be adjusted to spawn at different rates or positions.
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject itemToSpawn;

    [SerializeField] float offset; // value for how random the actual spawn point of the potion is, relative to the spawner.
    //[SerializeField] int buffer = 50; // value for how far up potions should spawn.

    float spawnDelay;
    [SerializeField] float maxSpawnDelay = 15; 
    [SerializeField] float minSpawnDelay = 10;

    private void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    // this function continually repeats itself from Start -
    // it spawns an item, and randomises the next spawn time.
    IEnumerator SpawnTimer()
    {
        SpawnItem();

        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnTimer());
    }

    // gets the position of the gameobject and randomly offsets it,
    // then instantiates items on the offset point.
    void SpawnItem()
    {
        Vector2 objectPos = transform.position;

        Vector3 currentSpawnPos = new Vector3(Random.Range(objectPos.x + offset, objectPos.x - offset), 
            Random.Range(objectPos.y + offset, objectPos.y - offset), 0);
        //Random.Range(0, Screen.width), Screen.height + buffer, 1); 


        Instantiate(itemToSpawn, currentSpawnPos, Quaternion.identity); //Camera.main.ScreenToWorldPoint(currentSpawnPos),
    }
    // OLD LOGIC: get the height of the screen, pick a random point along the x axis of the screen, translate to worldspace via Unity function, and spawn the item there.

}
