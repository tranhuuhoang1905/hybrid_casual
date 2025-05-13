using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor; // Thêm thư viện UnityEditor để dùng ExitPlaymode()
#endif

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private string sceneToLoad; // Biến lưu tên scene cần load
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    void Start() 
    {
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode(); // Thoát hẳn Unity Editor khi nhấn Exit
        #else
            Application.Quit(); // Thoát game khi đã build
        #endif
    }

    public void RestartGame(){
        Destroy(GameManager.Instance.gameObject);
        Destroy(AudioManager.Instance.gameObject);
        Destroy(GameController.Instance.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    
}
