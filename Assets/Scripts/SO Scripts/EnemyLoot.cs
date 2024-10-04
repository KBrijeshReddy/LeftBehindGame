using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Loot", menuName = "General/Enemy Loot")]
public class EnemyLoot : ScriptableObject
{
    public List<Item> items;
    public List<int> counts;

}
