using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector]
    public Item item;
    [HideInInspector]
    public bool isEmpty = true;


    [Header("-----------------Preset-----------------")]
    [SerializeField]
    private TMP_Text countTMP;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private bool isChestSlot = false;
    [SerializeField]
    private bool isTraderSlot = false;


    private InventoryManager inventory;
    private InventoryUI inventoryUI;

    void Start() {
        inventory = InventoryManager.instance;
        inventoryUI = InventoryUI.instance;
    }

    public void ChangeItem(Item newItem, int newCount) {
        isEmpty = false;
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        countTMP.text = newCount.ToString();
    }

    public void RemoveItem() {
        if (!isEmpty) {
            item = null;
            icon.enabled = false;
            countTMP.text = "";
            isEmpty = true;
        }
    }

    public void SelectItem() {
        if (isEmpty) {
            Debug.Log("Slot is empty");
            inventoryUI.Deselect();
        } else if (isTraderSlot)
        {
            TradingManager.instance.InitiateTrade(item);
            inventoryUI.ChangeDescription(item);
        } else
        {
            if (inventory.nearTrader && item.isTradable) {
                TradingManager.instance.UpdateTraderUI();
            }
            
            if (inventory.selected == item) {
                Debug.Log("Deselecting " + item.itemName);
                inventoryUI.Deselect();
            } else
            {
                Debug.Log("Selecting " + item.itemName);
                inventoryUI.Select(item);
            }
        }
    }
}
