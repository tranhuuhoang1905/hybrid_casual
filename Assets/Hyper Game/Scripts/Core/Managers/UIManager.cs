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
    [SerializeField] private TextMeshProUGUI nextWare;
    [SerializeField] private TextMeshProUGUI nextWareName;
    [SerializeField] private TextMeshProUGUI timeText;
    // [SerializeField] private TextMeshProUGUI timeFinalText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject NotifyNextWare;
    private int nextWareTime = 10;
    private Coroutine nextWareCoroutine;
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
        // blinkEffect = NotifyNextWare.GetComponent<BlinkEffect>();
    }
    void Start(){
        
        HideAllPopup();
        // RestartNextWareCountdown();
        
    }
    void Update(){
        slider.value = sliderValue;
    }

    public void RestartNextWareCountdown()
    {
        // blinkEffect.StopBlinking();
        // Nếu coroutine đang chạy, dừng nó trước khi chạy lại
        // if (nextWareCoroutine != null)
        // {
        //     StopCoroutine(nextWareCoroutine);
        // }

        // nextWareCoroutine = StartCoroutine(NextWareCountdown()); // Khởi chạy lại
    }

    public void HideAllPopup()
    {
        if (winUI) winUI.SetActive(false);
        
        if (mainUI) mainUI.SetActive(true);

        Time.timeScale = 1f;
    }

    private IEnumerator NextWareCountdown()
    {
        while (nextWareTime > 0) // Chạy khi nextWareTime lớn hơn 0
        {
            yield return new WaitForSeconds(1f); // Đợi 1 giây
            nextWareTime--; // Giảm thời gian
            if (nextWareTime<=3)
            {
                blinkEffect.StartBlinking();
            }
            UpdateNextWareUI(nextWareTime); // Cập nhật giao diện
        }
    }
    void OnEnable()
    {
        ScoreEvent.OnScoreUpdated += UpdateScoreUI;
        ScoreEvent.OnTimeUpdated += UpdateTimeUI;
        ScoreEvent.OnExpUpdated += UpdateExpBarUI;
        GameEvents.OnNextWarWare += SetNextWareTime;
        GameEvents.OnGameOver += ShowWinUI;
    }

    void OnDisable()
    {
        ScoreEvent.OnScoreUpdated -= UpdateScoreUI;
        ScoreEvent.OnTimeUpdated -= UpdateTimeUI;
        ScoreEvent.OnExpUpdated -= UpdateExpBarUI;
        GameEvents.OnNextWarWare -= SetNextWareTime;
        GameEvents.OnGameOver -= ShowWinUI;
    }

    public void SetNextWareTime(int newTime, WareType wareType)
    {
        if (wareNames.TryGetValue(wareType, out string name))
        {
            nextWareName.text = name;
        }
        nextWareTime = newTime; 
        RestartNextWareCountdown();
        UpdateNextWareUI(nextWareTime); 
    }

    private void UpdateNextWareUI(int nextWareTime)
    {
        int minutes = Mathf.FloorToInt(nextWareTime / 60);
        int seconds = Mathf.FloorToInt(nextWareTime % 60);
        nextWare.text = string.Format("{0:0}:{1:00}", minutes, seconds);

    }

    

    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    private void UpdateTimeUI(int newTime)
    {
        int minutes = Mathf.FloorToInt(newTime / 60);
        int seconds = Mathf.FloorToInt(newTime % 60);
        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
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
            TextMeshProUGUI timeFinalText = winUI.transform.Find("Popup/Time_Panel/Time/TimeValue")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreFinalText = winUI.transform.Find("Popup/ScorePanel/Score/ScoreValue")?.GetComponent<TextMeshProUGUI>();
            if(scoreFinalText) scoreFinalText.text = scoreText.text;
            if(timeFinalText) timeFinalText.text = timeText.text;
        }
        Time.timeScale = 0f;
    }

    
}
