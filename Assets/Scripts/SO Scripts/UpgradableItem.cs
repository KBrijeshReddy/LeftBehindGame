using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgradable Item", menuName = "Inventory/Upgradable Item")]
public class UpgradableItem : ScriptableObject
{
   

    public int currentLevel;

    [SerializeField]
    private List<UpgradeRequirements> upgrades;
    [SerializeField]
    private List<float> damageValues;

    public void CompleteUpgrade() {
        currentLevel += 1;
        Debug.Log("Invoke an event");
        
    }

    public bool InFinalLevel() {
        return (currentLevel == upgrades[upgrades.Count - 1].levelTransition + 1);
    }

    public UpgradeRequirements GetItemRequirements() {
        return upgrades[currentLevel];
    }

    public float GetDamage() {
        return damageValues[currentLevel];
    }
}
