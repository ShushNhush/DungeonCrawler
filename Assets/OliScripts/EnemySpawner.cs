using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign enemy prefab in Inspector
    public int enemyCount = 5; // Number of enemies to spawn
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f); // Size of the spawn zone

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 randomPos = GetRandomPosition();
            Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        return transform.position + randomOffset;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
