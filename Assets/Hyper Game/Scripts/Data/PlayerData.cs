using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int level;
    public int exp;
    public int applyPoints;
    public CharacterStats stats;

    private static readonly int[] expToLevelUp = {
        0, 5, 10, 20, 30, 50, 65, 80, 100, 120, 245,
        175, 200, 230, 260, 290, 330, 370, 420, 460, 500
    };

    public PlayerData()
    {
        level = 1;
        exp = 0;
        applyPoints = 10;
        stats = new CharacterStats(1);
    }

    public void AddExp(int amount)
    {
        exp += amount;

        while (level < expToLevelUp.Length - 1 && exp >= GetMaxExp())
        {
            exp -= GetMaxExp();
            level++;
            applyPoints+= 5;
            // stats.SetLevel(level);
        }

        exp = Mathf.Clamp(exp, 0, GetMaxExp());
    }

    public int GetMaxExp()
    {
        return expToLevelUp[Mathf.Clamp(level, 0, expToLevelUp.Length - 1)];
    }

    public bool UpgradeStat(string statType)
    {
        if (applyPoints <= 0) return false;

        switch (statType)
        {
            case "health":
                stats.bonusStats.health += stats.statsUpLevel.health;
                break;
            case "attack":
                stats.bonusStats.attack += stats.statsUpLevel.attack;
                break;
            case "armor":
                stats.bonusStats.armor += stats.statsUpLevel.armor;
                break;
            case "speed":
                stats.bonusStats.moveSpeed += stats.statsUpLevel.moveSpeed;
                break;
            case "attackSpeed":
                stats.bonusStats.attackSpeed += stats.statsUpLevel.attackSpeed;
                break;
            default:
                return false;
        }
        Debug.Log($"Check UpgradeStat stats.bonusStats.health: {stats.bonusStats.health}");
        applyPoints--;
        return true;
    }

    public bool UpgradeMapBonusStat(Attr attrMapBonus)
    {
        stats.MapBonusBonusAdd(attrMapBonus);
        return true;
    }
}
