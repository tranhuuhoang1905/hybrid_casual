using UnityEngine;
using System;
public class ScoreEvent
{
    public static event Action<ScoreEntry> OnAddScore;
    public static event Action<int, int, int> OnExpUpdated;
    public static event Action<int> OnScoreUpdated;
    public static event Action<int> OnTimeUpdated;
    

    public static void RaiseScore(ScoreEntry scoreEntry)
    {
        OnAddScore?.Invoke(scoreEntry);
    }

    public static void RaiseScoreUpdated(int newScore)
    {
        OnScoreUpdated?.Invoke(newScore);
    }

    public static void RaiseTimeUpdated(int newScore)
    {
        OnTimeUpdated?.Invoke(newScore);
    }

    public static void RaiseExpUpdated(int newScore,int maxScore,int newLevel)
    {
        OnExpUpdated?.Invoke(newScore,maxScore,newLevel);
    }
    
}
