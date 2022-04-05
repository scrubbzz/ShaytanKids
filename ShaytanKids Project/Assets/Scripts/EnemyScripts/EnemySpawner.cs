using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject bizzaro;
    public GameObject shaytanKid;
    public GameObject archangel;
    public GameObject[] spawnLocations;
    public List<GameObject> enemies;
    public List<GameObject> shaytanKids;

    public float timer;
    public int spawnDelay;

    public int spawnRange;

    // Start is called before the first frame update
    void Start()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation");
        enemies = new List<GameObject>();
        //spawnDelay = 3;
              
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        SpawnEnemies();
        spawnRange = Random.Range(0, spawnLocations.Length);
        //Debug.Log(enemies);
    }
    public void SpawnEnemies()
    {
        if (timer >= spawnDelay)
        {
            //enemies.Add(Instantiate(bizzaro, spawnLocations[spawnRange].transform.position, Quaternion.identity));
            enemies.Add(Instantiate(shaytanKid, spawnLocations[spawnRange].transform.position, Quaternion.identity));
            //enemies.Add(Instantiate(archangel, spawnLocations[spawnRange].transform.position, Quaternion.identity));
            shaytanKids.Add(shaytanKid);
            //Debug.Log("There are " + enemies.Count + "enemies");

            timer = 0;
        }
    }
    

}

