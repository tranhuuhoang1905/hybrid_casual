using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public ScoreEvent ScoreEvent;
    private int playerType = 1;
    // private int score = 0;
    private int map_id = 0;
    private Character character;
    public SceneSignal sceneSignal;
    public PlayerData playerData;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        ScoreEvent.OnAddScore += AddToScore; // Đăng ký sự kiện
        if (sceneSignal != null)
        {
            sceneSignal.OnSceneLoaded.AddListener(RefreshUI);
        }
    }

    void Start() 
    {
        playerData = SaveManager.LoadPlayer();
        
        character = FindObjectOfType<Character>();
        RefreshUI();
    }

    void OnApplicationQuit()
    {
        SaveManager.SavePlayer(playerData); // Save khi thoát
    }
    
    void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        ScoreEvent.OnAddScore -= AddToScore;
        if (sceneSignal != null)
        {
            sceneSignal.OnSceneLoaded.RemoveListener(RefreshUI);
        }
    }

    public void AddToScore(ScoreEntry scoreEntry)
    {
        switch (scoreEntry.scoreType)
        {
            case ScoreType.Score:
            Debug.Log("check AddToScore: score");
                // score += scoreEntry.value; // Cộng vào điểm số
                playerData.AddScore(scoreEntry.value);
                GameEvents.RefreshPlayerData();
                break;
            case ScoreType.Experience:
                playerData.AddExp(scoreEntry.value);
                GameEvents.RefreshPlayerData();
                break;
            case ScoreType.Health:
                if (character != null)
                {
                    character.AddHealth(scoreEntry.value);
                }
                break;
            case ScoreType.SwordUpgrade:
                if (character != null)
                {
                    character.AddSwordLevel(scoreEntry.value);
                }
                break;
            default:
                break;
        }
    }

    public void RefreshUI()
    {
        // ScoreEvent.RaiseScoreUpdated(score);
        GameEvents.RefreshPlayerData();
        // character = FindObjectOfType<Character>();
        // if (character != null)
        // {
        //     character.ApplyDataFromGameManager();
        // }
    }
    public void SetPlayerType(int type)
    {
        playerType = type;
    }

    public int GetPlayerType()
    {
        return playerType;
    }
    
    public int GetScore()
    {
        return playerData.GetScore();
    }

    public CharacterStats GetCurrentStats()
    {
        return playerData.stats;
    }
    public void SetMapId(int value)
    {
        map_id = value;
    }
    
    public int GetMapId()
    {
        return map_id;
    }

    public bool ClearMapBonus()
    {
        return playerData.ClearMapBonus();
    }

    
}
