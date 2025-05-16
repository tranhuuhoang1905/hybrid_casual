using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    private int score = 0;
    
    [SerializeField] private AudioClip StartScenebackgroundMusic; // Nhạc nền mặc định
    // public SceneSignal sceneSignal;

    [SerializeField] private GameObject AttrUI;
    [SerializeField] private TextMeshProUGUI score_ui;
    GameManager gameManager;

    void Start()
    {
        if (StartScenebackgroundMusic != null)
        {
            AudioManager.Instance.PlayMusic(StartScenebackgroundMusic);
        }
        gameManager = GameManager.Instance;
        gameManager.ClearMapBonus();
        gameManager.SetMapId(0);

        
        ApplyDataFromGameManager();
        
    }

    private void OnEnable()
    {
        GameEvents.OnRefreshPlayerData += ApplyDataFromGameManager;
    }

    private void OnDisable()
    {
        GameEvents.OnRefreshPlayerData -= ApplyDataFromGameManager;
    }
            
    public void OnStartButtonPressed()
    {
        AudioManager.Instance.PlayButtonClick();
        SceneManager.LoadScene("Map1");
        // sceneSignal.LoadScene("BattleSelectScene");
    }

    public void OnExitButtonPressed()
    {
        AudioManager.Instance.PlayButtonClick();
        GameController.Instance.Quit();
    }

    public void OnAttrButtonPressed(bool active)
    {
        AudioManager.Instance.PlayButtonClick();
        if (AttrUI) AttrUI.SetActive(active);
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
