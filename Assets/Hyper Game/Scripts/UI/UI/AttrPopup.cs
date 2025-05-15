using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttrPopup : MonoBehaviour
{
    // Start is called before the first frame update
    private int level = 3;
    private int exp = 0;
    private int maxExp = 0;
    private int applyPoints = 0;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI attack;
    [SerializeField] private TextMeshProUGUI armor;
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI attack_speed;
    [SerializeField] private TextMeshProUGUI bonus_attr_points;

    
    [SerializeField] private TextMeshProUGUI level_ui;
    [SerializeField] private Slider slider;
    private CharacterStats stats;
    void Start()
    {
        ApplyDataFromGameManager();
        RefreshUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ApplyDataFromGameManager()
    {
        applyPoints = GameManager.Instance.playerData.applyPoints;
        level = GameManager.Instance.playerData.level;
        exp = GameManager.Instance.playerData.exp;
        maxExp = GameManager.Instance.playerData.GetMaxExp();
        stats = GameManager.Instance.playerData.stats;
    }

    void RefreshUI()
    {
        health.text = stats.TotalStats.health.ToString();
        attack.text = stats.TotalStats.attack.ToString();
        armor.text = stats.TotalStats.armor.ToString();
        speed.text = stats.TotalStats.moveSpeed.ToString();
        attack_speed.text = stats.TotalStats.attackSpeed.ToString();
        bonus_attr_points.text = applyPoints.ToString();

        level_ui.text = level.ToString();
        float sliderValue = 1f;
        if (maxExp != 0)
        {
            sliderValue = (float)exp / maxExp; // Ép kiểu float
            sliderValue = Mathf.Round(sliderValue * 100f) / 100f;
        }
        slider.value = sliderValue;
    }

    public void OnClickUpgradeStat(string stat)
    {
        AudioManager.Instance.PlayButtonClick();
        if (GameManager.Instance.playerData.UpgradeStat(stat))
        {
            ApplyDataFromGameManager();
            RefreshUI();
        }else
        {
            Debug.Log("false");
        }
    }
}
