using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            InventoryItem newItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            InventorySlot OriginalSlot = newItem.originalSlot.GetComponent<InventorySlot>();

            // Revert OnDrag Changes
            newItem.canvasGroup.blocksRaycasts = true;

            if (newItem.inWeaponSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                OriginalSlot.GetComponent<TooltipTrigger>().header = null;
                OriginalSlot.GetComponent<TooltipTrigger>().content = null;
                GameManager.Instance.weaponID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedWeapon);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inHelmetSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                OriginalSlot.GetComponent<TooltipTrigger>().header = null;
                OriginalSlot.GetComponent<TooltipTrigger>().content = null;
                GameManager.Instance.helmetID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedHelmet);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inChestSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                OriginalSlot.GetComponent<TooltipTrigger>().header = null;
                OriginalSlot.GetComponent<TooltipTrigger>().content = null;
                GameManager.Instance.chestID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedChest);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inLegsSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                OriginalSlot.GetComponent<TooltipTrigger>().header = null;
                OriginalSlot.GetComponent<TooltipTrigger>().content = null;
                GameManager.Instance.legsID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedLegs);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inBootsSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                OriginalSlot.GetComponent<TooltipTrigger>().header = null;
                OriginalSlot.GetComponent<TooltipTrigger>().content = null;
                GameManager.Instance.bootsID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedLeftBoot);
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedRightBoot);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inBackSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                OriginalSlot.GetComponent<TooltipTrigger>().header = null;
                OriginalSlot.GetComponent<TooltipTrigger>().content = null;
                GameManager.Instance.backID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedBack);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inShieldSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                OriginalSlot.GetComponent<TooltipTrigger>().header = null;
                OriginalSlot.GetComponent<TooltipTrigger>().content = null;
                GameManager.Instance.shieldID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedShield);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inAccessory1Slot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                OriginalSlot.GetComponent<TooltipTrigger>().header = null;
                OriginalSlot.GetComponent<TooltipTrigger>().content = null;
                GameManager.Instance.accessory1ID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedAccessory1);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inAccessory2Slot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                OriginalSlot.GetComponent<TooltipTrigger>().header = null;
                OriginalSlot.GetComponent<TooltipTrigger>().content = null;
                GameManager.Instance.accessory2ID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedAccessory2);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inAccessory3Slot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                OriginalSlot.GetComponent<TooltipTrigger>().header = null;
                OriginalSlot.GetComponent<TooltipTrigger>().content = null;
                GameManager.Instance.accessory3ID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedAccessory3);
                GameManager.Instance.DropItem(newItem);
            }
            else
            {
                if(OriginalSlot.currentItem.currentAmount <= 1)
                    GameManager.Instance.DropItem(newItem);
                else
                    GameManager.Instance.DropItem(newItem, newItem.currentAmount);

                if(OriginalSlot.GetComponentInChildren<InventoryItem>() == null)
                {
                    OriginalSlot.currentItem = null;
                    OriginalSlot.isFull = false;
                    OriginalSlot.GetComponent<TooltipTrigger>().header = null;
                    OriginalSlot.GetComponent<TooltipTrigger>().content = null;
                }
                else
                {
                    OriginalSlot.isFull = true;
                }
            }
        }
    }

}
