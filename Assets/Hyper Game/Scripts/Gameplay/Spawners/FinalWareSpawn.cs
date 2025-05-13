using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWareSpawn : WareSpawnBase
{
    [SerializeField] private List<Wave> waves;

    [System.Serializable]
    public class Wave
    {
        public List<GameObject> enemies; // Danh sách enemy trong wave
    }

    private Vector3[] spawnPositions; // Mảng chứa vị trí spawn cho từng wave

    void OnEnable()
    {
        GameEvents.OnFinalWareSpawn += SpawnAction;
    }

    void OnDisable()
    {
        GameEvents.OnFinalWareSpawn -= SpawnAction;
    }

    protected override void SpawnAction()
    {
        // Tạo vị trí spawn cho từng Wave
        CalculateSpawnPositions();

        // Spawn từng Wave tại vị trí đã tính toán
        for (int i = 0; i < waves.Count; i++)
        {
            if (i >= spawnPositions.Length) break; // Nếu có quá nhiều Wave, giới hạn theo số vị trí spawn
            foreach (GameObject enemyPrefab in waves[i].enemies)
            {
                if (enemyPrefab != null)
                {
                    GameObject newEnemy = Instantiate(enemyPrefab, GetRandomSpawnPosition(spawnPositions[i]), Quaternion.identity);
                    newEnemy.transform.SetParent(enemyPool, true);
                }
            }
        }
    }

    private void CalculateSpawnPositions()
    {
        spawnPositions = new Vector3[waves.Count];

        // Góc xuất hiện của từng Wave quanh Player (cách đều nhau)
        float angleStep = 360f / waves.Count; // Góc giữa các wave
        float radius = 50f; // Khoảng cách từ Player

        for (int i = 0; i < waves.Count; i++)
        {
            float angle = angleStep * i;
            float radians = angle * Mathf.Deg2Rad;

            Vector3 offset = new Vector3(Mathf.Cos(radians),Mathf.Sin(radians), 0) * radius;
            spawnPositions[i] = transform.position + offset;
        }
    }
}