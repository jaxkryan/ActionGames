using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour

{
    public List<GameObject> enemyPrefabs; // List of enemy prefabs to spawn from
    public List<Transform> spawnPoints; // List of spawn points
    public int enemiesPerWave = 5; // Number of enemies per wave
    public int spawnPointsPerWave = 2; // Number of spawn points to use per wave
    public float timeBetweenWaves = 5f; // Time between waves

    private int waveNumber = 1; // Current wave number
    private int activeEnemies = 0; // Active enemy count

    void Start()
    {
        StartCoroutine(SpawnWaves()); // Start spawning waves
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            List<Transform> selectedSpawnPoints = SelectRandomSpawnPoints(spawnPointsPerWave);
            for (int i = 0; i < selectedSpawnPoints.Count; i++)
            {
                for (int j = 0; j < enemiesPerWave; j++)
                {
                    SpawnEnemy(selectedSpawnPoints[i]);
                }
            }

            while (activeEnemies > 0)
            {
                yield return null; // Wait until all enemies are defeated
            }

            waveNumber++;
        }
    }

    void SpawnEnemy(Transform spawnPoint)
    {
        if (enemyPrefabs.Count == 0)
        {
            Debug.LogError("No enemy prefabs assigned to the spawner!");
            return;
        }

        int randomIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject enemyToSpawn = enemyPrefabs[randomIndex];
        GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
        activeEnemies++;

        // Add a collider and trigger event to detect when the enemy is destroyed
        EnemyTracker tracker = spawnedEnemy.AddComponent<EnemyTracker>();
        tracker.spawner = this;
    }

    public void HandleEnemyDeath()
    {
        activeEnemies--;
    }

    List<Transform> SelectRandomSpawnPoints(int count)
    {
        List<Transform> selectedPoints = new List<Transform>();
        List<int> usedIndices = new List<int>();

        while (selectedPoints.Count < count)
        {
            int randomIndex = Random.Range(0, spawnPoints.Count);
            if (!usedIndices.Contains(randomIndex))
            {
                usedIndices.Add(randomIndex);
                selectedPoints.Add(spawnPoints[randomIndex]);
            }
        }

        return selectedPoints;
    }
}

public class EnemyTracker : MonoBehaviour
{
    public EnemySpawnerController spawner;

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.HandleEnemyDeath();
        }
    }
}


