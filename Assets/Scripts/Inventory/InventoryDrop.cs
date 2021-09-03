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
                GameManager.Instance.weaponID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedWeapon);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inHelmetSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.helmetID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedHelmet);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inChestSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.helmetID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedHelmet);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inHelmetSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.helmetID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedHelmet);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inHelmetSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.helmetID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedHelmet);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inHelmetSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.helmetID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedHelmet);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inHelmetSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.helmetID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedHelmet);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inHelmetSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.helmetID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedHelmet);
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inHelmetSlot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.helmetID = -1;
                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedHelmet);
                GameManager.Instance.DropItem(newItem);
            }
            else
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.DropItem(newItem);
            }
        }
    }

}
