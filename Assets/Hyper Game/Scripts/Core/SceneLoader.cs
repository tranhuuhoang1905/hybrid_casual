using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor; // Thêm thư viện UnityEditor để dùng ExitPlaymode()
#endif

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    private string sceneToLoad; // Biến lưu tên scene cần load
    public SceneSignal sceneSignal;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // DontDestroyOnLoad(gameObject);
    }
    void OnEnable()
    {
        sceneSignal.OnLoadScene.AddListener(LoadScene);
    }

    void OnDisable()
    {
        sceneSignal.OnLoadScene.RemoveListener(LoadScene);
    }

    void Start() 
    {
    }


    public void LoadScene(string sceneName)
    {
        if (sceneName == "BattleScene")
        {
            sceneToLoad = sceneName; // Lưu scene cần load
            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    

    public IEnumerator LoadTargetScene()
    {
        yield return new WaitForSeconds(0.5f); // Giả lập thời gian load UI

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false; // Ngăn scene mới tự động kích hoạt

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingController.Instance.UpdateProgress(progress); // 🔥 Cập nhật progress bar

            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f); // Giả lập thời gian chờ
                operation.allowSceneActivation = true; // 🔥 Kích hoạt scene mới
                yield return new WaitForSeconds(0.1f);
            }

            yield return null;
        }
    }

    
    
}
