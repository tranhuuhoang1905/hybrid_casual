using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSystem : MonoBehaviour
{
    [SerializeField] GameObject swordPrefab;
    private List<GameObject> swords = new List<GameObject>();
    [SerializeField] private float radius = 2.0f;
    private Character playerCharacter;
    
    void Awake()
    {
        StatsRefresh.OnRefresh += RefreshSwordStats; // Đăng ký sự kiện
    }
    
    void OnDestroy()
    {
        StatsRefresh.OnRefresh -= RefreshSwordStats; // Đăng ký sự kiện
    }
    void Start()
    {
        playerCharacter = GetComponent<Character>();
    }
    // Cập nhật số lượng kiếm trong mảng dựa vào level
    public void UpdateSwordInventory(int levelSword)
    {
        foreach (GameObject sword in swords)
        {
            Destroy(sword);
        }
        swords.Clear();

        for (int i = 0; i < levelSword; i++)
        {
            SpawnSword(i,levelSword);
        }
        
    }
    private void SpawnSword(int i, int levelSword)
    {
        float angle = i * (360f / levelSword); // Chia đều góc trên vòng tròn
            float radian = angle * Mathf.Deg2Rad; // Chuyển sang radian

            // Tính vị trí x, y theo vòng tròn
            float x = Mathf.Cos(radian) * radius;
            float y = Mathf.Sin(radian) * radius;
            
            Vector3 spawnPosition = new Vector3(x,y,0);
            
            GameObject newSword = Instantiate(swordPrefab, spawnPosition, Quaternion.identity);
            Sword newSwordComponent = newSword.GetComponent<Sword>();
            Attr TotalStats = playerCharacter.GetCharacterStats();
            
            newSwordComponent.RefreshSwordStats(TotalStats);
            swords.Add(newSword);
            newSword.transform.SetParent(this.transform, false);
    }

    
    public void RefreshSwordStats( Attr totalStats)
    {
        foreach (GameObject sword in swords)
        {
            Sword swordComponent = sword.GetComponent<Sword>();
            swordComponent.RefreshSwordStats(totalStats);
        }
    }
}
