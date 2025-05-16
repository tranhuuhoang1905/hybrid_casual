using UnityEngine;
using UnityEngine.UI;
using System.Collections; // <--- Bổ sung dòng này


public class Character : MonoBehaviour
{
    public static Character Instance { get; private set; } // Singleton
    public int level = 3;
    public int exp = 0;
    private int health = 100;
    private int maxHealth = 100;
    public CharacterStats stats;
    protected SliderBar healthBar;
    private CharacterMovement characterMovement;
    private CharacterSwordHandler characterSwordHandler;
    private CharacterFireHandler characterFireHandler;
    private IHitEffect hitEffect;
    [SerializeField] private GameObject levelUpEffect;
    GameManager gameManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        healthBar = GetComponentInChildren<SliderBar>();
        characterMovement = GetComponent<CharacterMovement>();
        characterSwordHandler = GetComponent<CharacterSwordHandler>();
        characterFireHandler= GetComponent<CharacterFireHandler>();
        hitEffect = GetComponent<IHitEffect>();
        
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        Debug.Log("check start ApplyDataFromGameManager");
        ApplyDataFromGameManager();
        
        RefreashHealth();
        StartCoroutine(WaitForSwordManagerAndLoadWeapon());
    }

    IEnumerator WaitForSwordManagerAndLoadWeapon()
    {
        yield return new WaitUntil(() => characterSwordHandler != null);
        yield return new WaitForSeconds(0.2f); // Thêm delay nếu cần
        LoadWeapon();
    }
    void LoadWeapon(){
        
        int playerType = gameManager.GetPlayerType();
        
        switch (playerType)
        {
            case 1:
                AddFireLevel(1);
                break;
            case 2:
                AddSwordLevel(1);
                break;
            
            default:
                break;
        }
    }

    private void OnEnable()
    {
        GameEvents.OnRefreshPlayerData += ApplyDataFromGameManager;
    }

    private void OnDisable()
    {
        GameEvents.OnRefreshPlayerData -= ApplyDataFromGameManager;
    }

    public void AddHealth(int amount)
    {
        health = Mathf.Min(health + amount,maxHealth);
        
        GameEvents.ShowFloatingText(transform.position, amount,FloatingType.AddBlood);
        RefreashHealth();
        
    }
    public void AddSwordLevel(int amount)
    {
        characterSwordHandler.LevelUp(amount); 
    }
    public void AddFireLevel(int amount)
    {
        characterFireHandler.LevelUp(amount); 
    }

    private IEnumerator DelayShowLevelUpPopup()
    {
        yield return new WaitForSeconds(2f); // ⏳ Delay 1 giây
        GameEvents.LevelUp();
    }

    private void RefreashHealth()
    {
        maxHealth = stats.TotalStats.health;
        healthBar.UpdateSliderBar(health, maxHealth);
    }

    public int GetDamage()
    {        
        return stats.TotalStats.attack;
    }

    public bool GetIsAOE()
    {
        return gameManager.playerData.GetIsAOE();
    }

    public void SetIsAOE(bool status)
    {
        gameManager.playerData.SetIsAOE(status);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateSliderBar(health, maxHealth);
        GameEvents.ShowFloatingText(transform.position, damageAmount,FloatingType.ExceptBlood);
        if (hitEffect != null)
        {
            hitEffect.ApplyHitEffect();
        }
        if (health <= 0)
        {
            Die();
        }
    }
    public Attr GetCharacterStats(){
        
        return stats.TotalStats;
    }
    public CharacterWeaponHandler GetCharacterSwordHandle(){
        
        return characterSwordHandler;
    }
    public CharacterWeaponHandler GetCharacterFireHandler(){
        
        return characterFireHandler;
    }
    private void Die()
    {
        characterMovement.Die();
        GameEvents.GameOver();
    }
    
    public void ApplyDataFromGameManager()
    {
        level = gameManager.playerData.level;

        exp = gameManager.playerData.exp;
        stats = gameManager.playerData.stats;
        Debug.Log($"chekc ApplyDataFromGameManager level:{level}");
        
        health = stats.TotalStats.health;

        RefreashHealth();
        // StatsRefresh.Refresh(stats.TotalStats);
    }

    public void SetLevel(int value)
    {
        level  = value;
    }

    public void SetExp(int value)
    {
        exp  = value;
    }
    
}
