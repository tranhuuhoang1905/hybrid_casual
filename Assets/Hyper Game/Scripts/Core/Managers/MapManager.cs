using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapManager : MonoBehaviour
{
    private int level = 3;
    private int exp = 0;
    private int maxExp = 0;
    [SerializeField] private TextMeshProUGUI level_ui;
    [SerializeField] private Slider slider;
    [SerializeField] private AudioClip StartScenebackgroundMusic; // Nhạc nền mặc định
    // public SceneSignal sceneSignal;

   

    private CharacterStats stats;
    
    void Start()
    {
        if (StartScenebackgroundMusic != null)
        {
            AudioManager.Instance.PlayMusic(StartScenebackgroundMusic);
        }
        
        updateMapID();
        ApplyDataFromGameManager();
        RefreshUI();
        Debug.Log($"Check mapid: {GameManager.Instance.GetMapId()}");
    }
    void updateMapID()
    {
        int map_id = GameManager.Instance.GetMapId();
        GameManager.Instance.SetMapId(map_id+1);
    }

    void RefreshUI()
    {
        level_ui.text = level.ToString();
        float sliderValue = 1f;
        if (maxExp != 0)
        {
            sliderValue = (float)exp / maxExp; // Ép kiểu float
            sliderValue = Mathf.Round(sliderValue * 100f) / 100f;
        }
        Debug.Log($"check sliderValue{sliderValue}");
        slider.value = sliderValue;
    }

    public void ApplyDataFromGameManager()
    {
        level = GameManager.Instance.playerData.level;
        exp = GameManager.Instance.playerData.exp;
        maxExp = GameManager.Instance.playerData.GetMaxExp();
        stats = GameManager.Instance.playerData.stats;
    }

    
}
