using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    PlayerInventory playerInventory;
    WeaponSlotManager weaponSlotManager;
    HelmetSlotManager helmetSlotManager;
    UIManager uiManager;
    public Image icon;
    public Item invItem;
    [SerializeField] private ItemPickup itemPickup; 
    [SerializeField] private Transform player; 

private void Awake() {
    playerInventory = FindObjectOfType<PlayerInventory>();
    uiManager = FindObjectOfType<UIManager>();
    weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
    helmetSlotManager = FindObjectOfType<HelmetSlotManager>();
}
    public void AddItem(Item newItem) 
    {
        invItem = newItem;
        icon.sprite = invItem.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearInventorySlot()
    {
        invItem = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    public void EquipThisItem(string type)
    {   
        if(invItem is WeaponItem)
        {
            if(type == "Right")
            {
                if((invItem as WeaponItem).isTwoHanded)
                {
                    if(!playerInventory.rightWeapon.isUnarmed)
                        playerInventory.Inventory.Add(playerInventory.rightWeapon);
                    if(!playerInventory.leftWeapon.isUnarmed)
                        playerInventory.Inventory.Add(playerInventory.leftWeapon);
                    
                    playerInventory.rightWeapon = (WeaponItem)invItem;
                    playerInventory.leftWeapon = playerInventory.unarmed;
                    playerInventory.Inventory.Remove(invItem);

                }
                else
                {
                    if(!playerInventory.rightWeapon.isUnarmed)
                        playerInventory.Inventory.Add(playerInventory.rightWeapon);
                    playerInventory.rightWeapon = (WeaponItem)invItem;
                    playerInventory.Inventory.Remove(invItem);
                }
            }
            else if (type == "Left")
            {
                if(!(invItem as WeaponItem).isTwoHanded)
                {
                    if(!playerInventory.rightWeapon.isTwoHanded)
                    {
                        if(!playerInventory.leftWeapon.isUnarmed)
                        {
                            playerInventory.Inventory.Add(playerInventory.leftWeapon);
                        }
                        playerInventory.leftWeapon = (WeaponItem)invItem;
                        playerInventory.Inventory.Remove(invItem);
                    }
                }
            }

            weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
            weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon, true);

            uiManager.equipmentWindowUI.LoadLeftWeaponOnEquipmentScreen(playerInventory);
            uiManager.equipmentWindowUI.LoadRightWeaponOnEquipmentScreen(playerInventory);
        }
        else if(invItem is HelmetItem)
        {
            if(type == "Helmet")
            {
                if(playerInventory.helmet != null)
                    playerInventory.Inventory.Add(playerInventory.helmet);
                playerInventory.helmet = (HelmetItem)invItem;
                playerInventory.Inventory.Remove(invItem); 

                helmetSlotManager.LoadHelmetOnSlot(playerInventory.helmet);

                uiManager.equipmentWindowUI.LoadHelmetOnEquipmentScreen(playerInventory);
            }
        }
    }

    public void onDrop(Vector2 pos)
    {
        itemPickup.item = invItem;
        itemPickup.interactableText = "Press 'E' to Pickup '" + invItem.itemName + "'";
        Instantiate(itemPickup, player.position, player.rotation);
        playerInventory.Inventory.Remove(invItem);
    }
}
