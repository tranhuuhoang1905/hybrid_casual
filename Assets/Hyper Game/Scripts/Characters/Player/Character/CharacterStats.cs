using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    public int level;
    public Attr baseStats;
    public Attr statsUpLevel;
    public Attr statsBonus;
    public Attr bonusStats;
    
    public Attr itemBonus;

    public CharacterStats(int startLevel)
    {
        level = startLevel;
        baseStats = new Attr(
            damage: 1,
            attack: 5,
            swordAttack: 3,
            attackSpeed: 1f,
            swordSpeed: 10f,
            moveSpeed: 5f,
            armor: 5,
            health: 100
        );
        
        statsUpLevel =new Attr(
            damage: 0,
            attack: 0,
            swordAttack: 0,
            attackSpeed: 0.3f,
            swordSpeed: 0.2f,
            moveSpeed: 0f,
            armor: 1,
            health: 25
        );
        bonusStats =new Attr(
            damage: 0,
            attack: 0,
            swordAttack: 0,
            attackSpeed: 0f,
            swordSpeed: 0f,
            moveSpeed: 0f,
            armor: 0,
            health: 0
        );
        itemBonus = new Attr();
    }

    public Attr TotalStats => baseStats + itemBonus;

    public void SetLevel(int newLevel)
    {

        int levelUp = Mathf.Max(0, newLevel - level);
        level = newLevel;
        baseStats += statsUpLevel * levelUp;
    }

    public void BonusMaxHealth(int value)
    {
        bonusStats.health += value;
    }

    public void BonusMoveSpeed(float value)
    {
        bonusStats.moveSpeed += value;
    }
    
    public void BonusAttack(int attack)
    {
        bonusStats.attack += attack;
    }

    public void BonusAttackSpeed(float value)
    {
        bonusStats.attackSpeed += value;
    }

    public void BonusSwordAttack(int attack)
    {
        bonusStats.swordAttack += attack;
    }

    public void BonusSwordSpeed(float value)
    {
        bonusStats.swordSpeed += value;
    }
}
