using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Requirement", menuName = "Inventory/Upgrade Requirement")]
public class UpgradeRequirements : ScriptableObject
{
    public int levelTransition;
    public Sprite initialIcon;
    public List<Item> items;
    public List<int> counts;
    public string description;
}
