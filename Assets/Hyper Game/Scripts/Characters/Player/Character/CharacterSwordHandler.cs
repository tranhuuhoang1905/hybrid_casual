using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwordHandler : CharacterWeaponHandler
{
    private SwordSystem swordSystem;
    
    void Start()
    {
        swordSystem = GetComponent<SwordSystem>();
        UpdateInventory();
    }
    public override void LevelUp(int addLevel)
    {
        level += addLevel;
        UpdateInventory();
    }

    private void UpdateInventory()
    {
        if (!swordSystem) return;
        swordSystem.UpdateSwordInventory(level);
    }
    
}
