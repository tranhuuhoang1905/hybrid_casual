using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static event Action OnGameOver;
    public static event Action OnLevelUp;
    public static event Action<Vector3, int,FloatingType> OnShowFloatingText;
    public static event Action<int> OnNomalWareSpawn;
    public static event Action<int> OnWarWareSpawn;
    public static event Action<int,WareType> OnNextWarWare;
    public static event Action OnFinalWareSpawn;
    public static event Action<TutorialType> OnShowTutorialGame;

    public static void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public static void LevelUp()
    {
        OnLevelUp?.Invoke();
    }
    
    public static void ShowFloatingText(Vector3 position,int value,FloatingType type)
    {
        OnShowFloatingText?.Invoke(position,value,type);
    }

    public static void NomalWareSpawn(int wareId)
    {
        OnNomalWareSpawn?.Invoke(wareId);
    }

    public static void WarWareSpawn(int wareId)
    {
        OnWarWareSpawn?.Invoke(wareId);
    }

    public static void NextWarWare(int wareId,WareType wareType)
    {
        OnNextWarWare?.Invoke(wareId,wareType);
    }
    
    public static void FinalWareSpawn()
    {
        OnFinalWareSpawn?.Invoke();
    }

    public static void ShowTutorialGame(TutorialType type)
    {
        OnShowTutorialGame?.Invoke(type);
        
    }
}
