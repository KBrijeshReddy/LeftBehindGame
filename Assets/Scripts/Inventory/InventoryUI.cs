using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance; void Awake() { instance = this; }
    public static bool isActive;


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

        if (Input.GetKeyDown(KeyCode.Escape) && !PauseMenu.pauseMenuOpen) {
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
            chestPanel.SetActive(InventoryManager.nearInteractors["chest"]);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            InventoryManager.selected = null;

            CallUpdateTraderUI();
        } else
        {
            GetComponent<Canvas>().enabled = false;
            chestPanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            InventoryManager.selected = null;
            Deselect();
        }

        if (InventoryManager.nearInteractors["upgrader"]) {
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
        if (InventoryManager.nearInteractors["trader"]) {
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

        if (InventoryManager.nearInteractors["chest"]) {
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

        collectButton.interactable = InventoryManager.nearInteractors["chest"];
    }

    public void Select(Item item) {
        InventoryManager.selected = item;
        ChangeDescription(item);
        CallUpdateTraderUI();
    }

    public void Deselect() {
        InventoryManager.selected = null;
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
