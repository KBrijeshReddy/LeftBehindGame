using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Inventory/Level")]
public class ChestsInLevel : ScriptableObject
{
    public int level;
    public List<Chest> chests;
    public List<bool> wasCollected;

    public void ResetChests() {
        for (int i = 0; i < wasCollected.Count; i++) {
            wasCollected[i] = false;
        }
    }
}
