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
    private int score = 0;
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
        RefreshUI();
        playerData = SaveManager.LoadPlayer();
        Debug.Log($"check playerData.level:{playerData.level}");
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
        if (character != null)
        {
            switch (scoreEntry.scoreType)
            {
                case ScoreType.Score:
                    score += scoreEntry.value; // Cộng vào điểm số
                    ScoreEvent.RaiseScoreUpdated(score);
                    break;
                case ScoreType.Experience:
                    playerData.AddExp(scoreEntry.value); 

                    if (character != null)
                    {
                        character.SetLevel(playerData.level); 
                        character.SetExp(playerData.exp);
                    }
                    ScoreEvent.RaiseExpUpdated(playerData.exp, playerData.GetMaxExp(), playerData.level);
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
    }

    public void RefreshUI()
    {
        ScoreEvent.RaiseScoreUpdated(score);
        
        character = FindObjectOfType<Character>();
        if (character != null)
        {
            character.ApplyDataFromGameManager();
        }
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
        return score;
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

}
