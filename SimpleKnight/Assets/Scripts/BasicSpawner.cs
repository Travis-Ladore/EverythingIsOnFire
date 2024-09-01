using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public float spawnInterval = 3f; // Time interval between spawns

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
