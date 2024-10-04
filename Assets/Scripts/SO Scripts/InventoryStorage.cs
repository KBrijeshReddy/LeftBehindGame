using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Storage", menuName = "Inventory/Storage")]
public class InventoryStorage : ScriptableObject
{
    public List<Item> items;
    public List<int> counts;

    public int GetCount(Item item) {
        if (items.Contains(item)) {
            return counts[items.IndexOf(item)];
        } else
        {
            return 0;
        }
    }
}
