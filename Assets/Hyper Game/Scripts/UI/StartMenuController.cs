using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    
    [SerializeField] private AudioClip StartScenebackgroundMusic; // Nhạc nền mặc định
    public SceneSignal sceneSignal;


    void Start()
    {
        if (StartScenebackgroundMusic != null)
        {
            AudioManager.Instance.PlayMusic(StartScenebackgroundMusic);
        }
    }
            
    public void OnStartButtonPressed()
    {
        sceneSignal.LoadScene("BattleSelectScene");
        
    }

    public void OnExitButtonPressed()
    {
        GameController.Instance.Quit();
    }
}