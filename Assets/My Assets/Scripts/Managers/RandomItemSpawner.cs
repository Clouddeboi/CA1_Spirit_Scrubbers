using System.Collections;
using UnityEngine;

public class RandomItemSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] itemPrefabs; //Array of item prefabs to spawn
    public Transform[] spawnPoints; //Array of empty GameObjects as spawn points
    public float minSpawnInterval = 1f; //Min time between spawns
    public float maxSpawnInterval = 5f; //Max time between spawns

    public bool spawnEnabled = true; //Flag for spawning

    private void Start()
    {
        //Debug statements if spawners aren't correctly set up
        if (itemPrefabs.Length == 0)
        {
            Debug.LogError("No item prefabs assigned to the spawner.");
            return;
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned to the spawner.");
            return;
        }

        //Starts the item spawning coroutine when the game starts 
        StartCoroutine(SpawnItems());
    }

    private IEnumerator SpawnItems()
    {
        //While spawning is allowed
        while (spawnEnabled)
        {
            //waits a random time between the min and max to spawn an item
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        //Pick a random prefab
        GameObject prefabToSpawn = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

        //Pick a random spawn point
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        //Spawn the item at the chosen spawn point
        Instantiate(prefabToSpawn, randomSpawnPoint.position, Quaternion.identity);
    }
}