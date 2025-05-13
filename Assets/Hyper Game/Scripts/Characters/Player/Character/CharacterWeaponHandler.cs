using UnityEngine;

public abstract class CharacterWeaponHandler : MonoBehaviour
{
    protected int level = 0; // Cấp độ vũ khí
    public int maxLevel = 7; // Cấp độ vũ khí

    public virtual void LevelUp(int addLevel)
    {
        if (level>=maxLevel ) return;
        level += addLevel;
    }

    
    public int GetLevel()
    {
        return level;
    }

    public int GetMaxLevel()
    {
        return maxLevel;
    }
}