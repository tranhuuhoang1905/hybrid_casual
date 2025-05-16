using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttrPopup : MonoBehaviour
{
    // Start is called before the first frame update
    private int applyPoints = 0;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI attack;
    [SerializeField] private TextMeshProUGUI armor;
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI attack_speed;
    [SerializeField] private TextMeshProUGUI bonus_attr_points;

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
