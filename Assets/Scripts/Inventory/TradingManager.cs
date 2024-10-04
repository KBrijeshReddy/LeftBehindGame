using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TradingManager : MonoBehaviour
{
    public static TradingManager instance; void Awake() { instance = this; }

    [SerializeField]
    private TMP_Text infoTMP;
    [SerializeField]
    private Button tradeButton;
    [SerializeField]
    private GameObject tradePanel;

    private InventorySlot[] traderSlots;
    private TradeItems tradeItems;
    private int importIndex;

    void Start()
    {
        traderSlots = tradePanel.GetComponentsInChildren<InventorySlot>();
    }

    public void UpdateTraderUI() {
        Debug.Log("Updating trader UI");

        if (InventoryManager.selected == null || !InventoryManager.selected.isTradable) {
            infoTMP.text = "(Select a tradable item to begin trading)";
            tradeButton.interactable = false;

            for (int i = 0; i < traderSlots.Length; i++)
            {
                traderSlots[i].RemoveItem();
            }
        } else
        {
            Item exportItem = InventoryManager.selected;
            tradeItems = exportItem.importItems;

            for (int i = 0; i < traderSlots.Length; i++)
            {
                traderSlots[i].ChangeItem(tradeItems.items[i], tradeItems.counts[i]); // Check TradeItems.cs for more information
            }

            InitiateTrade(null);
        }
    }

    public void InitiateTrade(Item item) {
        if (item == null) {
            infoTMP.text = "(Select an item to trade for)";
            tradeButton.interactable = false;
        } else
        {
            importIndex = tradeItems.items.IndexOf(item);

            infoTMP.text = "Trade 1 " + InventoryManager.selected.itemName + " for " + tradeItems.counts[importIndex] + " " + tradeItems.items[importIndex].itemName + "?";
            tradeButton.interactable = true;
        }
    }

    public void Trade() {
        Debug.Log("Trading 1 " + InventoryManager.selected.itemName + " for " + tradeItems.counts[importIndex] + " " + tradeItems.items[importIndex].itemName);
        InventoryManager.instance.Increase(tradeItems.items[importIndex], tradeItems.counts[importIndex]);
        InventoryManager.instance.Decrease(InventoryManager.selected, 1);
        InventoryUI.instance.UpdateUI();
        UpdateTraderUI();
    }
}
