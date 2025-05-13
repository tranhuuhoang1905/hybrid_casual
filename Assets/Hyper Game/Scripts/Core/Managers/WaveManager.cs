using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    
    public List<int> TimeWars;  // Danh sách các wave
    // public WaveSpawner waveSpawner;  // Tham chiếu đến WaveSpawner
    [SerializeField] private float normalSpawnInterval = 5f;
    private int nomalWareId = 1;
    private int warWareId = 1;
    private bool isFinal = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        StartCoroutine(WarSpawnWaves());
        StartCoroutine(NormalWaveSpawn());
    }

    private IEnumerator WarSpawnWaves()
    {
        while (!isFinal) 
        {
            if (warWareId>TimeWars.Count) isFinal = true;
            if (!isFinal)
            {
                int WarTimeout = TimeWars[warWareId-1];

                GameEvents.NextWarWare(WarTimeout,WareType.War);
                yield return new WaitForSeconds(WarTimeout); // ⏳ Chờ 10 giây
                GameEvents.WarWareSpawn(warWareId);
                warWareId ++;
            }
            else
            {
                GameEvents.NextWarWare(15,WareType.Final);
                yield return new WaitForSeconds(15); // ⏳ Chờ 10 giây
                GameEvents.FinalWareSpawn();
                yield return new WaitForSeconds(1);
                GameEvents.ShowTutorialGame(TutorialType.FinalWar);
            }
            
        }
    }

    private IEnumerator NormalWaveSpawn()
    {
        while (true) // 🔄 Chạy vô hạn, spawn normal wave mỗi 10 giây
        {
            yield return new WaitForSeconds(normalSpawnInterval); // ⏳ Chờ 10 giây
            GameEvents.NomalWareSpawn(nomalWareId);
            nomalWareId ++;
        }
    }
}