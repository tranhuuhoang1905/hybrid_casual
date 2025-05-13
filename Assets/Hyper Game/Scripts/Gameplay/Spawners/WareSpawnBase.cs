using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareSpawnBase : MonoBehaviour
{
    
    [SerializeField] protected GameObject spawnEffectPrefab;
    public Transform enemyPool;
    [SerializeField] private float spawnRadius = 2f; // 🔥 Bán kính random vị trí spawn
    void Start()
    {
        if (enemyPool == null)
        {
            GameObject poolObject = new GameObject("EnemyPool");
            enemyPool = poolObject.transform;
        }

    }

    protected virtual void SpawnAction()
    {
        
    }

    protected virtual void SpawnAction(int wareId)
    {
        
    }

    protected Vector3 GetRandomSpawnPosition(Vector3 position)
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius; // 🔥 Random trong vòng tròn bán kính `spawnRadius`
        return new Vector3(position.x + randomCircle.x, position.y+ randomCircle.y, position.z + randomCircle.y);
    }
    

}
