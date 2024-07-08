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
    public GameObject summonEffectPrefab; // Summon effect prefab
    public float summonEffectDuration = 1f; // Duration of the summon effect animation

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
                    StartCoroutine(SpawnEnemyWithEffect(selectedSpawnPoints[i]));
                }
            }

            while (activeEnemies > 0)
            {
                yield return null; // Wait until all enemies are defeated
            }

            waveNumber++;
        }
    }

    IEnumerator SpawnEnemyWithEffect(Transform spawnPoint)
    {
        // Instantiate the summon effect
        GameObject summonEffect = Instantiate(summonEffectPrefab, spawnPoint.position, spawnPoint.rotation);

        // Wait for the summon effect duration
        yield return new WaitForSeconds(summonEffectDuration);

        // Destroy the summon effect
        Destroy(summonEffect);

        // Instantiate the enemy
        SpawnEnemy(spawnPoint);
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