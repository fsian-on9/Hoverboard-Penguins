using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    public float obstacleSpawnTime = 1f;
    public float obstacleSpeed = 1f;
    public float spawnMin = 1f;
    public float spawnMax = 3f;
    public bool regularSpawn = false;

    private float timeUntilObstacleSpawn;

    private void Awake() {
        // this sets a a randome spawntime for the 1st time
        obstacleSpawnTime = Random.Range(spawnMin, spawnMax);
        // Debug.Log($"Spawn time: {obstacleSpawnTime}");
    }
    private void Update()
    {   
        if (GameManager.Instance.isPlaying)
        {
        SpawnLoop();
        }
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn >= obstacleSpawnTime)
        {
            Spawn();
            //resets timeUntilObstacleSpawn to 0
            timeUntilObstacleSpawn = 0f;
            if (regularSpawn == false) {
            // this sets a a randome spawntime for the 1st time
                obstacleSpawnTime = Random.Range(spawnMin, spawnMax);
                // Debug.Log($"Spawn time: {obstacleSpawnTime}");
            }
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        

        GameObject spawnedObstacle =  Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.linearVelocity = Vector2.left * obstacleSpeed;
        // Debug.Log($"Rock Spawned");
    }
}
