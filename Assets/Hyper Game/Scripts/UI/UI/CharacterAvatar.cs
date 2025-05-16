using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterAvatar : MonoBehaviour
{
    
    private int level = 3;
    private int exp = 0;
    private int maxExp = 0;
    
    [SerializeField] private TextMeshProUGUI level_ui;
    [SerializeField] private Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
    public void ApplyDataFromGameManager()
    {
        level = GameManager.Instance.playerData.level;
        exp = GameManager.Instance.playerData.exp;
        maxExp = GameManager.Instance.playerData.GetMaxExp();  
        RefreshUI();
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
        slider.value = sliderValue;
    }
}
