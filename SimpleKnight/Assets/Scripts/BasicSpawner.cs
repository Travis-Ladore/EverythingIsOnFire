using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab of the enemy to spawn
    public Transform[] spawnPoints; // Possible spawn points
    public float initialSpawnDelay = 2f; // Initial delay before the first spawn
    public float minSpawnInterval = 1f; // Minimum time between spawns
    public float maxSpawnInterval = 5f; // Maximum time between spawns
    public float spawnIntervalReductionRate = 0.1f; // Rate at which the spawn interval decreases
    public float minBreakDuration = 2f; // Minimum break duration
    public float maxBreakDuration = 5f; // Maximum break duration

    private float currentSpawnInterval;
    private bool isSpawning = true;

    private void Start()
    {
        currentSpawnInterval = maxSpawnInterval;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        while (isSpawning)
        {
            SpawnEnemy();

            // Wait for the current spawn interval
            yield return new WaitForSeconds(currentSpawnInterval);

            // Optionally add a random break duration
            float breakDuration = Random.Range(minBreakDuration, maxBreakDuration);
            yield return new WaitForSeconds(breakDuration);

            // Reduce the spawn interval but ensure it doesn't go below the minimum
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnIntervalReductionRate);
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        // Choose a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    // Call this method to stop spawning if needed
    public void StopSpawning()
    {
        isSpawning = false;
    }
}
