using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySaveManager : MonoBehaviour
{
    [System.Serializable]
	public class SaveItem {
        public int itemID;
        public int currentAmount;
        public int slot;
	}

    public SaveItem[] inv;
    public SaveEquipment[] eq;

    private void Start() 
    {
        eq = new SaveEquipment[10];
        inv = new SaveItem[32];

        for(int i = 0; i < eq.Length; i++)
            eq[i] = new SaveEquipment();

        for(int i = 0; i < inv.Length; i++)
            inv[i] = new SaveItem();
    }

    public void Save() 
    {
        for(int i = 0; i < eq.Length; i++)
        {
            eq[i].helmet = GameManager.Instance.inventorySlots[i].helmetSlot;
            eq[i].weapon = GameManager.Instance.inventorySlots[i].weaponSlot;
            eq[i].chest = GameManager.Instance.inventorySlots[i].chestSlot;
            eq[i].legs = GameManager.Instance.inventorySlots[i].legsSlot;
            eq[i].boots = GameManager.Instance.inventorySlots[i].bootsSlot;
            eq[i].back = GameManager.Instance.inventorySlots[i].backSlot;
            eq[i].shield = GameManager.Instance.inventorySlots[i].shieldSlot;
            eq[i].acc1 = GameManager.Instance.inventorySlots[i].accessory1Slot;
            eq[i].acc2 = GameManager.Instance.inventorySlots[i].accessory2Slot;
            eq[i].acc3 = GameManager.Instance.inventorySlots[i].accessory3Slot;
            
        }

        for(int i = 0; i < inv.Length; i++)
        {
            if(GameManager.Instance.inventorySlots[i+10].currentItem != null)
            {
                inv[i].slot = i+10;
                inv[i].itemID = GameManager.Instance.inventorySlots[i+10].currentItem.itemID;
                inv[i].currentAmount = GameManager.Instance.inventorySlots[i+10].currentItem.currentAmount;
            }
        }
    }
}