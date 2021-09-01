using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public EquipmentWindowUI equipmentWindowUI;
    public GameObject HUD;
    public GameObject inventoryWindow;
    public GameObject equipmentScreenWindow;
    public GameObject inventorySlotPrefab;
    public Transform inventorySlotsParent;
    InventorySlot[] inventorySlots;
    
    private void Start() 
    {
        inventorySlots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        equipmentWindowUI.LoadLeftWeaponOnEquipmentScreen(playerInventory);
        equipmentWindowUI.LoadRightWeaponOnEquipmentScreen(playerInventory);
        equipmentWindowUI.LoadHelmetOnEquipmentScreen(playerInventory);
    }

    private void Update() 
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < playerInventory.Inventory.Count)
            {
                if(inventorySlots.Length < playerInventory.Inventory.Count)
                {
                    Instantiate(inventorySlotPrefab, inventorySlotsParent);
                    inventorySlots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>();
                }
                inventorySlots[i].AddItem(playerInventory.Inventory[i]);
            }
            else
            {
                inventorySlots[i].ClearInventorySlot();
            }
        }
    }

    public void OpenInventoryUI()
    {
        equipmentScreenWindow.SetActive(true);
        inventoryWindow.SetActive(true);
    }

    public void CloseAllInventoryWindows()
    {
        inventoryWindow.SetActive(false);
        equipmentScreenWindow.SetActive(false);
    }
}
