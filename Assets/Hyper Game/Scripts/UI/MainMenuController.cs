using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    private int level = 3;
    private int exp = 0;
    private int maxExp = 0;
    
    [SerializeField] private AudioClip StartScenebackgroundMusic; // Nhạc nền mặc định
    // public SceneSignal sceneSignal;

    [SerializeField] private GameObject AttrUI;
    [SerializeField] private TextMeshProUGUI level_ui;
    [SerializeField] private Slider slider;
    private CharacterStats stats;

    void Start()
    {
        if (StartScenebackgroundMusic != null)
        {
            AudioManager.Instance.PlayMusic(StartScenebackgroundMusic);
        }
        GameManager.Instance.SetMapId(0);
        
        Debug.Log($"Check mapid: {GameManager.Instance.GetMapId()}");
        ApplyDataFromGameManager();
        RefreshUI();
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
        level_ui.text = level.ToString();
        float sliderValue = 1f;
        if (maxExp != 0)
        {
            sliderValue = (float)exp / maxExp; // Ép kiểu float
            sliderValue = Mathf.Round(sliderValue * 100f) / 100f;
        }
        Debug.Log($"check sliderValue{sliderValue}");
        slider.value = sliderValue;
    }

    public void ApplyDataFromGameManager()
    {
        level = GameManager.Instance.playerData.level;
        exp = GameManager.Instance.playerData.exp;
        maxExp = GameManager.Instance.playerData.GetMaxExp();
        stats = GameManager.Instance.playerData.stats;
    }
}
