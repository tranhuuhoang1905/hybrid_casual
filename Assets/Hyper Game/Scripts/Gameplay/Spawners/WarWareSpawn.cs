using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarWareSpawn : WareSpawnBase
{
    [SerializeField] bool IsSpawn = true;
    [SerializeField] List<Wave> waves;
    [System.Serializable]
    public class Wave
    {
        public List<GameObject> enemies; // Danh sách enemy trong wave
    }

    void OnEnable()
    {
        GameEvents.OnWarWareSpawn += SpawnAction;
    }

    void OnDisable()
    {
        GameEvents.OnWarWareSpawn -= SpawnAction;
    }

    protected override void SpawnAction(int wareId)
    {
        if (!IsSpawn || waves == null || waves.Count == 0)
        {
            return;
        }
        wareId = Mathf.Min(wareId,waves.Count);
        Wave currentWave = waves[wareId - 1]; // Lấy wave tương ứng

        foreach (GameObject enemyPrefab in currentWave.enemies)
        {
            if (enemyPrefab != null)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, GetRandomSpawnPosition(transform.position), Quaternion.identity);
                newEnemy.transform.SetParent(enemyPool, true);
            }
        }
    }
    
}
