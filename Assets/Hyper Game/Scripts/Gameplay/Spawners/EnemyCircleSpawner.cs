using UnityEngine;

public class EnemyCircleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyCount = 100;
    [SerializeField] private float spawnRadius = 100f;

    void Start()
    {
        SpawnEnemyCircle();
    }

    void SpawnEnemyCircle()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            float angle = i * (360f / enemyCount) * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * spawnRadius;
            float y = Mathf.Sin(angle) * spawnRadius;

            Vector3 spawnPosition = new Vector3(x, y, 0);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            
        }
    }
}
