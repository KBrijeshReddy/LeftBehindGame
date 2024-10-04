using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance; void Awake() { instance = this; }

    [HideInInspector]
    public bool isActive;


    [Header("-----------------Preset-----------------")]
    [SerializeField]
    private InventoryStorage inventoryStorage;
    [SerializeField]
    private TMP_Text titleTMP;
    [SerializeField]
    private TMP_Text descriptionTMP;
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject upgradingPanel;
    [SerializeField]
    private GameObject slotsPanel;
    [SerializeField]
    private GameObject chestPanel;
    [SerializeField]
    private GameObject tradePanel;
    [SerializeField]
    private Button collectButton;
    [SerializeField]
    private List<UpgradingUI> upgradingUIs;
    [SerializeField]
    private PauseMenu pauseMenu;


    private InventoryManager inventory;
    private InventorySlot[] inventorySlots;
    private InventorySlot[] chestSlots;
    private List<Item> chestItems;
    private List<int> chestCounts;

    void Start()
    {
        isActive = false;
        inventoryPanel.SetActive(true);
        upgradingPanel.SetActive(false);
        GetComponent<Canvas>().enabled = false;
        inventory = InventoryManager.instance;
        inventorySlots = slotsPanel.GetComponentsInChildren<InventorySlot>();
        chestSlots = chestPanel.GetComponentsInChildren<InventorySlot>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            ToggleInventory(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToggleInventory(false);
        }
    }

    public void CollectItemsFromChest() {
        InventoryManager.instance.CollectItemsFromChest();
    }

    public void ToggleInventory(bool turnOn) {
        isActive = turnOn;
        pauseMenu.enabled = !turnOn;

        if (turnOn) {
            UpdateUI();

            GetComponent<Canvas>().enabled = true;
            chestPanel.SetActive(inventory.nearChest);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            inventory.selected = null;

            CallUpdateTraderUI();
        } else
        {
            GetComponent<Canvas>().enabled = false;
            chestPanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            inventory.selected = null;
            Deselect();
        }

        if (inventory.nearUpgrader) {
            inventoryPanel.SetActive(false);
            upgradingPanel.SetActive(turnOn);
            foreach (var upgradingUI in upgradingUIs) {
                upgradingUI.UpdateUpgradingUI();
            }
        } else
        {
            inventoryPanel.SetActive(true);
            upgradingPanel.SetActive(false);
        }
    }

    public void CallUpdateTraderUI() {
        if (inventory.nearTrader) {
            tradePanel.SetActive(true);
            TradingManager.instance.UpdateTraderUI();
        } else
        {
            tradePanel.SetActive(false);
        }
    }

    public void UpdateUI(bool afterCollection = false) {
        Debug.Log("Updating inventory UI");

        for (int i = 0; i < inventoryStorage.counts.Count; i++)
        {
            if (inventoryStorage.counts[i] == 0) {
                inventorySlots[i].RemoveItem();
            } else
            {
                inventorySlots[i].ChangeItem(inventoryStorage.items[i], inventoryStorage.counts[i]);
            }
        }

        if (inventory.nearChest) {
            // Debug.Log(inventory.GetChest().counts.Count);
            
            for (int i = 0; i < inventory.GetChest().counts.Count; i++)
            {
                if (inventory.GetChest().items[i] == null) {
                    chestSlots[i].RemoveItem();
                } else
                {
                    chestSlots[i].ChangeItem(inventory.GetChest().items[i], inventory.GetChest().counts[i]);
                }
            }
        } else if (afterCollection)
        {
            foreach (var item in chestSlots) {
                item.RemoveItem();
            }
        }

        collectButton.interactable = inventory.nearChest;
    }

    public void Select(Item item) {
        inventory.selected = item;
        ChangeDescription(item);
        CallUpdateTraderUI();
    }

    public void Deselect() {
        inventory.selected = null;
        ChangeDescription(null);
        CallUpdateTraderUI();
    }

    public void ChangeDescription(Item item) {
        if (item == null) {
            titleTMP.text = "";
            descriptionTMP.text = "";
        } else
        {
            titleTMP.text = item.itemName;
            descriptionTMP.text = item.description;
        }
    }
}
