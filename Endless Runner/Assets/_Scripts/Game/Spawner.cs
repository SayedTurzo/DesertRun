using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] SpawnableObjects;
    public float spawnDelayTime;
    
    int randIndex;

    float spawnDelay;

    void Start()
    {
        spawnDelay = spawnDelayTime;
    }

    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        randIndex = Random.Range(0, SpawnableObjects.Length);
        spawnDelay -= Time.deltaTime;
        if (spawnDelay <= 0)
        {
            Instantiate(SpawnableObjects[randIndex], transform.position, transform.rotation);
            spawnDelay = spawnDelayTime;
        }
    }
}
