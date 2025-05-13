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
    private int timeSeconds = 0;
    private Character character;
    
    public SceneSignal sceneSignal;


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
        StartCoroutine(startTime());
        RefreshUI();
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
                    character.AddExp(scoreEntry.value);
                    break;
                case ScoreType.Health:
                    character.AddHealth(scoreEntry.value);
                    break;
                case ScoreType.SwordUpgrade:
                    character.AddSwordLevel(scoreEntry.value);
                    break;
                default:
                    break;
            }

        }
        
    }

    private IEnumerator startTime()
    {
        while (true)
        {
            timeSeconds +=1;
            UpdateTimerUI();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateTimerUI()
    {

        ScoreEvent.RaiseTimeUpdated(timeSeconds);
    }

    public void RefreshUI()
    {
        ScoreEvent.RaiseScoreUpdated(score);
        
        character = FindObjectOfType<Character>();
        if (character != null)
        {
            int exp = character.exp;
            int level = character.level;
            int maxExp = character.GetMaxExp();
            ScoreEvent.RaiseExpUpdated(exp, maxExp, level);
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
}
