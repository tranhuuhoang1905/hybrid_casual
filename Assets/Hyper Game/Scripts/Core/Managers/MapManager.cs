using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI score_ui;
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
        Debug.Log($"Check mapid: {GameManager.Instance.GetMapId()}");
    }

    private void OnEnable()
    {
        GameEvents.OnRefreshPlayerData += ApplyDataFromGameManager;
    }

    private void OnDisable()
    {
        GameEvents.OnRefreshPlayerData -= ApplyDataFromGameManager;
    }

    void updateMapID()
    {
        int map_id = GameManager.Instance.GetMapId();
        GameManager.Instance.SetMapId(map_id+1);
    }

    void RefreshUI()
    {
        score_ui.text = score.ToString();
    }

    public void ApplyDataFromGameManager()
    {
        score = GameManager.Instance.GetScore();
        RefreshUI();
    }

    
}
