using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

using UnityEngine.SceneManagement;

public class WinPopup : MonoBehaviour
{
    [SerializeField] private GameObject winUI;
    
    [SerializeField] private GameObject reward_content;
    [SerializeField] private GameObject reward_element;
    [SerializeField] private GameObject reward_item_content;
    [SerializeField] private GameObject reward_item_element;

    [SerializeField] private Sprite iconHealth;
    [SerializeField] private Sprite iconAttack;
    [SerializeField] private Sprite iconArmor;
    [SerializeField] private Sprite iconAttackSpeed;
    [SerializeField] private Sprite iconMoveSpeed;
    [SerializeField] private Attr attrMapBonus;
    
    [System.Serializable]
    public struct RewardEntry
    {
        public string label;
        public Sprite icon;
        public float value;
    }
    private bool flagReward = true;
    private List<RewardEntry> rewardList;
    
    private CharacterStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GameManager.Instance.playerData.stats;
        updateListContentReward();
    }

    void OnEnable()
    {
        GameEvents.OnWinEvent += ShowWinUI;
    }

    void OnDisable()
    {
        GameEvents.OnWinEvent -= ShowWinUI;
    }

    public void ShowWinUI(bool status)
    {
        if (winUI) 
        {
            winUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    
    void updateListContentReward()
    {
        rewardList = new List<RewardEntry>()
    {
        new RewardEntry { label = "Maximum health:", icon = iconHealth, value = attrMapBonus.health },
        new RewardEntry { label = "Attack:",          icon = iconAttack, value = attrMapBonus.attack },
        new RewardEntry { label = "Armor:",           icon = iconArmor, value = attrMapBonus.armor },
        new RewardEntry { label = "Attack Speed:",    icon = iconAttackSpeed, value = attrMapBonus.attackSpeed },
        new RewardEntry { label = "Move Speed:",      icon = iconMoveSpeed, value = attrMapBonus.moveSpeed }
    };

    foreach (var reward in rewardList)
    {
        if (reward.value > 0f)
        {
            AddRewardRow(reward.label, reward.value);
            AddRewardIconRow(reward.icon, reward.value);
        }
    }
    }
    void AddRewardRow(string label, float value)
    {
        GameObject element = Instantiate(reward_element, reward_content.transform);

        
        var titleText = element.GetComponent<TextMeshProUGUI>();
        var valueText = element.transform.Find("Value")?.GetComponent<TextMeshProUGUI>();
        if (titleText != null) titleText.text = label;
        if (valueText != null) valueText.text = "+" + value.ToString();
        element.SetActive(true);

    }

    void AddRewardIconRow(Sprite label, float value)
    {
        
        GameObject elementAvatar = Instantiate(reward_item_element, reward_item_content.transform);
        var icon = elementAvatar.transform.Find("Image")?.GetComponent<Image>(); // <-- lấy ảnh
        icon.sprite = label;
        elementAvatar.SetActive(true);
    }

    public void OnRewardButtonPressed()
    {
        if (!flagReward) return;
        flagReward= false;
        
        AudioManager.Instance.PlayButtonClick();
        if (GameManager.Instance.playerData.UpgradeMapBonusStat(attrMapBonus))
        {
            Time.timeScale = 1f;
            loadNextMap();

        }
    }
    void loadNextMap()
    {
        int mapId = GameManager.Instance.GetMapId();
        string nextMapName = "Map" + (mapId+1);
        if (IsSceneInBuild(nextMapName))
        {
            SceneManager.LoadScene(nextMapName);
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy scene '{nextMapName}', chuyển về Main.");
            SceneManager.LoadScene("Main");
        }
    }
    
    bool IsSceneInBuild(string sceneName)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = Path.GetFileNameWithoutExtension(path);
            if (name == sceneName)
                return true;
        }
        return false;
    }
}
