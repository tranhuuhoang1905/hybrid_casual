using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; // ✅ Cần thêm dòng này
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;
    // [SerializeField] private TextMeshProUGUI timeFinalText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject mainUI;
    private BlinkEffect blinkEffect;
    private float sliderValue = 0f;
    private Dictionary<WareType, string> wareNames = new Dictionary<WareType, string>
    {
        { WareType.War, "Next Ware" },
        { WareType.Final, "The final battle" }
    };

    [SerializeField] private Slider slider;
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
    void Start(){
        
        HideAllPopup();
        
    }
    void Update(){
        slider.value = sliderValue;
    }


    public void HideAllPopup()
    {
        if (winUI) winUI.SetActive(false);
        
        if (mainUI) mainUI.SetActive(true);

        Time.timeScale = 1f;
    }

    void OnEnable()
    {
        ScoreEvent.OnScoreUpdated += UpdateScoreUI;
        ScoreEvent.OnExpUpdated += UpdateExpBarUI;
        GameEvents.OnGameOver += ShowWinUI;
    }

    void OnDisable()
    {
        ScoreEvent.OnScoreUpdated -= UpdateScoreUI;
        ScoreEvent.OnExpUpdated -= UpdateExpBarUI;
        GameEvents.OnGameOver -= ShowWinUI;
    }

    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void UpdateExpBarUI(int currentValue, int maxValue, int newLevel)
    {
        levelText.text = newLevel.ToString();
        float fExpPercentage = 0f;
        if (maxValue != 0)
        {
            fExpPercentage = (float)currentValue / maxValue; // Ép kiểu float
            fExpPercentage = Mathf.Round(fExpPercentage * 100f) / 100f;
        }
        sliderValue = fExpPercentage;
    }
    public void OnExitButtonReStartGame()
    {
        HideAllPopup();
        GameController.Instance.RestartGame();
    }
    public void OnExitButtonQuitPressed()
    {
        GameController.Instance.Quit();
    }

    public void ShowWinUI()
    {
        if (winUI) 
        {
            winUI.SetActive(true);
            TextMeshProUGUI scoreFinalText = winUI.transform.Find("Popup/ScorePanel/Score/ScoreValue")?.GetComponent<TextMeshProUGUI>();
            if(scoreFinalText) scoreFinalText.text = scoreText.text;
        }
        Time.timeScale = 0f;
    }

    
}
