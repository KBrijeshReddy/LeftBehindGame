using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; void Awake() { instance = this; }
    public static Dictionary<string, bool> nearInteractors;
    public static int chestID;
    public static Item selected;


    [Header("-------------Scene-specific-------------")]
    [SerializeField]
    private ChestsInLevel chestsInLevel;

    [Header("-----------------Preset-----------------")]
    [SerializeField]
    private InventoryStorage inventoryStorage;
    [SerializeField]
    private List<UpgradableItem> upgradableItems;
    [SerializeField]
    private List<DialogueTexts> allDialogueTexts;
    [SerializeField]
    private List<ChestsInLevel> allChestsInLevels;


    void Start() {
        //EditorUtility.SetDirty(chestsInLevel);

        nearInteractors = new Dictionary<string, bool> {
            {"trader", false},
            {"chest", false},
            {"upgrader", false}
        };

        selected = null;

        if (SceneNameHolder.scene == "tutorial")
        ResetValues();
    }

    public void ResetValues() {
        Debug.Log("Resetting...");
        inventoryStorage.items.Clear();
        inventoryStorage.counts.Clear();

        foreach (ChestsInLevel level in allChestsInLevels) {
            level.ResetChests();
        }

        foreach (var upgradableItem in upgradableItems) {
            upgradableItem.currentLevel = 0;
        }

        foreach (var dialogueTexts in allDialogueTexts) {
            dialogueTexts.exhausted = false;
        }
    }

    public void Increase(Item item, int count) {
        if (inventoryStorage.items.Contains(item)) {
            inventoryStorage.counts[inventoryStorage.items.IndexOf(item)] += count;
        } else
        {
            bool found = false;
            for (int i = 0; i < inventoryStorage.items.Count; i++)
            {
                if (inventoryStorage.counts[i] == 0) {
                    inventoryStorage.items[i] = item;
                    inventoryStorage.counts[i] = count;
                    found = true;
                    break;
                }
            }
            if (!found) {
                inventoryStorage.items.Add(item);
                inventoryStorage.counts.Add(count);
            }
        }
    }

    public void Decrease(Item item, int count) {
        if (!inventoryStorage.items.Contains(item)) {
            Debug.Log("Item " + item.name + " doesn't exist in inventory");
        }

        int index = inventoryStorage.items.IndexOf(item);
        if (count == inventoryStorage.counts[index]) {
            inventoryStorage.counts[index] = 0;
            inventoryStorage.items[index] = null;
            Debug.Log("Removed " + item.name + " from inventory");
            InventoryUI.instance.Deselect();
        } else if (count < inventoryStorage.counts[index])
        {
            inventoryStorage.counts[index] -= count;
            Debug.Log("Reduced count of " + item.name + " by " + count);
        } else
        {
            Debug.Log("Not possible to decrease by more than existing amount");
        }
    }

    public void CollectItemsFromChest() {
        for (int i = 0; i < GetChest().counts.Count; i++) {
            Increase(GetChest().items[i], GetChest().counts[i]);
        }

        Debug.Log("Moved items from chest to inventory");
        chestsInLevel.wasCollected[chestID] = true;
        nearInteractors["chest"] = false;
        InventoryUI.instance.UpdateUI(true);
    }

    public Chest GetChest() {
        // foreach (var count in chestsInLevel.chests[chestID].counts) {
        //     Debug.Log(count);
        // }
        return chestsInLevel.chests[chestID];
    }

    public bool ChangeChestID(int id, bool active) {
        chestID = id;
        nearInteractors["chest"] = (active && !chestsInLevel.wasCollected[chestID]);
        return nearInteractors["chest"];
    }
}
