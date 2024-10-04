using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemTrade", menuName = "Inventory/ItemTrade")]
public class TradeItems : ScriptableObject
{
    public List<Item> items;
    public List<int> counts;
}
