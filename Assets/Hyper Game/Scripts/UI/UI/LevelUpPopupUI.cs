using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelUpPopupUI : MonoBehaviour
{
    public GameObject popupPanel; // Panel hiển thị
    private Character character;
    private CharacterWeaponHandler characterSwordHandler;
    private CharacterWeaponHandler characterFireHandler;
    [SerializeField] private GameObject FireLevelUp;
    [SerializeField] private GameObject SwordLevelUp;

    void Start()
    {
        popupPanel.SetActive(false); // Ẩn popup khi game bắt đầu
        character = FindObjectOfType<Character>(); // Tìm Character trong Scene
        if (character)
        {
            characterFireHandler = character.GetCharacterFireHandler();
            characterSwordHandler = character.GetCharacterSwordHandle();
        }

    }

    private void OnEnable()
    {
        GameEvents.OnLevelUp += ShowPopup;
    }

    private void OnDisable()
    {
        GameEvents.OnLevelUp -= ShowPopup;
    }


    private void ShowPopup()
    {
        if (characterFireHandler && FireLevelUp)
        {
            float fireLevel =  characterFireHandler.GetLevel();
            float fireMaxLevel = characterFireHandler.GetMaxLevel();
            FireLevelUp.SetActive(fireLevel<fireMaxLevel);
        }
        if (characterSwordHandler && SwordLevelUp)
        {
            float swordLevel =  characterSwordHandler.GetLevel();
            float swordMaxLevel = characterSwordHandler.GetMaxLevel();
            SwordLevelUp.SetActive(swordLevel<swordMaxLevel);
        }
        if (popupPanel) popupPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Skip()
    {
        if (popupPanel) popupPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void AddFireLevel()
    {
        character.AddFireLevel(1);
        Skip();
    }
    public void AddSwordLevel()
    {
        character.AddSwordLevel(1);
        Skip();
    }
    public void AddMaxHealth(int value)
    {
        character.stats.BonusMaxHealth(value);
        StatsRefresh.Refresh(character.stats.TotalStats);
        Skip();
    }
    
    public void AddSpeedUp(float value)
    {
        character.stats.BonusMoveSpeed(value);
        StatsRefresh.Refresh(character.stats.TotalStats);
        Skip();
    }
    public void AddAttack(int value)
    {
        character.stats.BonusAttack(value);
        StatsRefresh.Refresh(character.stats.TotalStats);
        Skip();
    }
    public void AddAttackSpeed(float value)
    {
        character.stats.BonusAttackSpeed(value);
        StatsRefresh.Refresh(character.stats.TotalStats);
        Skip();
    }
    public void AddSwordAttack(int value)
    {
        character.stats.BonusSwordAttack(value);
        StatsRefresh.Refresh(character.stats.TotalStats);
        Skip();
    }
    public void AddSwordSpeed(float value)
    {
        character.stats.BonusSwordSpeed(value);
        StatsRefresh.Refresh(character.stats.TotalStats);
        Skip();
    }

}
