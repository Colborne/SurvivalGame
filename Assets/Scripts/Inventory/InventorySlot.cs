using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryItem currentItem;

    public bool helmetSlot = false;
    public bool chestSlot = false;
    public bool legsSlot = false;
    public bool bootsSlot = false;
    public bool backSlot = false;
    public bool accessory1Slot = false;
    public bool accessory2Slot = false;
    public bool accessory3Slot = false;
    public bool weaponSlot = false;
    public bool shieldSlot = false;
    public bool isFull = false;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            InventoryItem newItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            InventorySlot OriginalSlot = newItem.originalSlot.GetComponent<InventorySlot>();

            // Original Slot != This Slot //
            if (newItem.originalSlot != this.transform)
            {
                // Slot == Full //
                if (isFull)
                {
                    // Dragging Into Weapon Slot From Normal Slot
                    if (weaponSlot && newItem.equipType == (int)InventoryItem.equipment.Weapon && OriginalSlot.currentItem.equipType == (int)InventoryItem.equipment.Weapon)
                    {
                        Debug.Log("Dropped Item: Full Weapon Slot");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inWeaponSlot
                        currentItem.inWeaponSlot = true;
                        OriginalSlot.currentItem.inWeaponSlot = false;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Weapon
                        GameManager.Instance.weaponID = currentItem.itemID;
                        GameManager.Instance.SpawnItem("Weapon", GameManager.Instance.spawnedWeapon);                   
                    }
                    // Dragging Into Helmet Slot From Normal Slot
                    else if (helmetSlot && newItem.equipType == (int)InventoryItem.equipment.Helmet && OriginalSlot.currentItem.equipType == (int)InventoryItem.equipment.Helmet)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inHelmetSlot
                        currentItem.inHelmetSlot = false;
                        OriginalSlot.currentItem.inHelmetSlot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.helmetID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Helmet", GameManager.Instance.spawnedHelmet);
                    }
                    // Dragging Into Shield Slot From Normal Slot
                    else if (shieldSlot && newItem.equipType == (int)InventoryItem.equipment.Shield && OriginalSlot.currentItem.equipType == (int)InventoryItem.equipment.Shield)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inShieldSlot
                        currentItem.inShieldSlot = false;
                        OriginalSlot.currentItem.inShieldSlot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.shieldID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Shield", GameManager.Instance.spawnedShield);
                    }
                    // Dragging Into Chest Slot From Normal Slot
                    else if (chestSlot && newItem.equipType == (int)InventoryItem.equipment.Chest && OriginalSlot.currentItem.equipType == (int)InventoryItem.equipment.Chest)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inChestSlot
                        currentItem.inChestSlot = false;
                        OriginalSlot.currentItem.inChestSlot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.chestID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Chest", GameManager.Instance.spawnedChest);
                    }
                    // Dragging Into Legs Slot From Normal Slot
                    else if (legsSlot && newItem.equipType == (int)InventoryItem.equipment.Legs && OriginalSlot.currentItem.equipType == (int)InventoryItem.equipment.Legs)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inLegsSlot
                        currentItem.inLegsSlot = false;
                        OriginalSlot.currentItem.inLegsSlot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.legsID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Legs", GameManager.Instance.spawnedLegs);
                    }
                    // Dragging Into Boots Slot From Normal Slot
                    else if (bootsSlot && newItem.equipType == (int)InventoryItem.equipment.Boots && OriginalSlot.currentItem.equipType == (int)InventoryItem.equipment.Boots)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inBootsSlot
                        currentItem.inBootsSlot = false;
                        OriginalSlot.currentItem.inBootsSlot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.bootsID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Boots", GameManager.Instance.spawnedBoots);
                    }
                    // Dragging Into Back Slot From Normal Slot
                    else if (backSlot && newItem.equipType == (int)InventoryItem.equipment.Back && OriginalSlot.currentItem.equipType == (int)InventoryItem.equipment.Back)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inBackSlot
                        currentItem.inBackSlot = false;
                        OriginalSlot.currentItem.inBackSlot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.backID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Back", GameManager.Instance.spawnedBack);
                    }
                    // Dragging Into Accessory Slot 1 From Normal Slot
                    else if (accessory1Slot && newItem.equipType == (int)InventoryItem.equipment.Accessory 
                        && OriginalSlot.currentItem.equipType == (int)InventoryItem.equipment.Accessory)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inAccessory3Slot
                        currentItem.inAccessory1Slot = false;
                        OriginalSlot.currentItem.inAccessory1Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.accessory1ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Accessory1", GameManager.Instance.spawnedAccessory1);
                    }
                    // Dragging Into Accessory Slot 2 From Normal Slot
                    else if (accessory2Slot && newItem.equipType == (int)InventoryItem.equipment.Accessory 
                        && OriginalSlot.currentItem.equipType == (int)InventoryItem.equipment.Accessory)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inAccessory2Slot
                        currentItem.inAccessory2Slot = false;
                        OriginalSlot.currentItem.inAccessory2Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.accessory2ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Accessory2", GameManager.Instance.spawnedAccessory2);
                    }
                    // Dragging Into Accessory Slot 3 From Normal Slot
                    else if (accessory3Slot && newItem.equipType == (int)InventoryItem.equipment.Accessory 
                        && OriginalSlot.currentItem.equipType == (int)InventoryItem.equipment.Accessory)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inAccessory3Slot
                        currentItem.inAccessory3Slot = false;
                        OriginalSlot.currentItem.inAccessory3Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.accessory3ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Accessory3", GameManager.Instance.spawnedAccessory3);
                    }
                    // Dragging Into Normal Slot From Weapon Slot
                    else if (!weaponSlot && currentItem.equipType == (int)InventoryItem.equipment.Weapon && newItem.equipType == (int)InventoryItem.equipment.Weapon 
                        && newItem.inWeaponSlot)
                    {
                        Debug.Log("Dropped Item: Full Weapon Slot [Other]");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inWeaponSlot
                        currentItem.inWeaponSlot = false;
                        OriginalSlot.currentItem.inWeaponSlot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Weapon
                        GameManager.Instance.weaponID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Weapon", GameManager.Instance.spawnedWeapon);
                    }
                    // Dragging Into Normal Slot From Helmet Slot
                    else if (!helmetSlot && currentItem.equipType == (int)InventoryItem.equipment.Helmet && newItem.equipType == (int)InventoryItem.equipment.Helmet 
                        && newItem.inHelmetSlot)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot [Other]");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inHelmetSlot
                        currentItem.inHelmetSlot = false;
                        OriginalSlot.currentItem.inHelmetSlot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.helmetID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Helmet", GameManager.Instance.spawnedHelmet);
                    }
                    // Dragging Into Normal Slot From Chest Slot
                    else if (!chestSlot && currentItem.equipType == (int)InventoryItem.equipment.Chest && newItem.equipType == (int)InventoryItem.equipment.Chest 
                        && newItem.inChestSlot)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot [Other]");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inHelmetSlot
                        currentItem.inChestSlot = false;
                        OriginalSlot.currentItem.inChestSlot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.chestID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Chest", GameManager.Instance.spawnedChest);
                    }
                    // Dragging Into Normal Slot From Legs Slot
                    else if (!legsSlot && currentItem.equipType == (int)InventoryItem.equipment.Legs && newItem.equipType == (int)InventoryItem.equipment.Legs 
                        && newItem.inLegsSlot)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot [Other]");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inHelmetSlot
                        currentItem.inLegsSlot = false;
                        OriginalSlot.currentItem.inLegsSlot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.legsID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Legs", GameManager.Instance.spawnedLegs);
                    }
                    // Dragging Into Normal Slot From Boots Slot
                    else if (!bootsSlot && currentItem.equipType == (int)InventoryItem.equipment.Boots && newItem.equipType == (int)InventoryItem.equipment.Boots 
                        && newItem.inBootsSlot)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot [Other]");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inHelmetSlot
                        currentItem.inBootsSlot = false;
                        OriginalSlot.currentItem.inBootsSlot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.bootsID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Boots", GameManager.Instance.spawnedBoots);
                    }
                    // Dragging Into Normal Slot From Back Slot
                    else if (!backSlot && currentItem.equipType == (int)InventoryItem.equipment.Back && newItem.equipType == (int)InventoryItem.equipment.Back 
                        && newItem.inBackSlot)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot [Other]");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inHelmetSlot
                        currentItem.inBackSlot = false;
                        OriginalSlot.currentItem.inBackSlot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.backID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Back", GameManager.Instance.spawnedBack);
                    }
                    // Dragging Into Normal Slot From Accessory 1 Slot
                    else if (!accessory1Slot && currentItem.equipType == (int)InventoryItem.equipment.Accessory && newItem.equipType == (int)InventoryItem.equipment.Accessory 
                        && newItem.inAccessory1Slot)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot [Other]");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inHelmetSlot
                        currentItem.inAccessory1Slot = false;
                        OriginalSlot.currentItem.inAccessory1Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.accessory1ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Accessory1", GameManager.Instance.spawnedAccessory1);
                    }
                    // Dragging Into Normal Slot From Accessory 1 Slot
                    else if (!accessory2Slot && currentItem.equipType == (int)InventoryItem.equipment.Accessory && newItem.equipType == (int)InventoryItem.equipment.Accessory 
                        && newItem.inAccessory2Slot)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot [Other]");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inHelmetSlot
                        currentItem.inAccessory2Slot = false;
                        OriginalSlot.currentItem.inAccessory2Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.accessory2ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Accessory2", GameManager.Instance.spawnedAccessory2);
                    }
                    // Dragging Into Normal Slot From Accessory 1 Slot
                    else if (!accessory3Slot && currentItem.equipType == (int)InventoryItem.equipment.Accessory && newItem.equipType == (int)InventoryItem.equipment.Accessory 
                        && newItem.inAccessory3Slot)
                    {
                        Debug.Log("Dropped Item: Full Helmet Slot [Other]");

                        Swap(newItem, OriginalSlot);

                        // Swapping InventoryItem:inHelmetSlot
                        currentItem.inAccessory3Slot = false;
                        OriginalSlot.currentItem.inAccessory3Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Helmet
                        GameManager.Instance.accessory3ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnItem("Accessory3", GameManager.Instance.spawnedAccessory3);
                    }
                    

                    // Slot Swapping In Inventory
                    else if (!OriginalSlot.currentItem.inHelmetSlot 
                    && !OriginalSlot.currentItem.inWeaponSlot
                    && !OriginalSlot.currentItem.inChestSlot 
                    && !OriginalSlot.currentItem.inLegsSlot
                    && !OriginalSlot.currentItem.inBootsSlot 
                    && !OriginalSlot.currentItem.inBackSlot
                    && !OriginalSlot.currentItem.inAccessory1Slot 
                    && !OriginalSlot.currentItem.inAccessory2Slot
                    && !OriginalSlot.currentItem.inAccessory3Slot 
                    && !OriginalSlot.currentItem.inShieldSlot 
                    && !currentItem.inWeaponSlot 
                    && !currentItem.inHelmetSlot
                    && !currentItem.inChestSlot 
                    && !currentItem.inLegsSlot
                    && !currentItem.inBootsSlot 
                    && !currentItem.inBackSlot
                    && !currentItem.inAccessory1Slot 
                    && !currentItem.inAccessory2Slot
                    && !currentItem.inAccessory3Slot 
                    && !currentItem.inShieldSlot )
                    {
                        //Stack
                        if(OriginalSlot.currentItem.itemID == newItem.itemID)
                        {
                            // Filling This Slot
                            int newAmount = currentItem.currentAmount + newItem.currentAmount;
                            if(newAmount <= currentItem.MaxAmount)
                            {
                                currentItem.currentAmount += newItem.currentAmount;
                                eventData.pointerDrag.transform.SetParent(gameObject.transform);
                                currentItem.originalSlot = this.transform;
                            }
                            else
                            {
                                currentItem.currentAmount = currentItem.MaxAmount;
                                int remainder = newAmount - currentItem.currentAmount;
                                GameManager.Instance.PickUpItem(currentItem.itemID, remainder);
                            }

                            if(OriginalSlot.GetComponentInChildren<InventoryItem>() == null)
                            {
                                // Emptying Original Slot
                                OriginalSlot.isFull = false;
                                OriginalSlot.currentItem = null;
                            }
                            Destroy(newItem.gameObject);
                        }
                        else
                        {
                            Swap(newItem, OriginalSlot);

                            // Swapping Parent
                            eventData.pointerDrag.transform.SetParent(gameObject.transform);
                            OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                            OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;
                        }
                    }
                    // Returning To Original Slot
                    else
                    {
                        Debug.Log("Dropped Item: Returning to original slot");
                    }
                }
                // Slot != Full //
                else if (!isFull)
                {
                    #region Moving Into
                    // Moving Into Weapon Slot
                    if (weaponSlot && newItem.equipType == (int)InventoryItem.equipment.Weapon)
                    {
                        Debug.Log("Dropped Item: Empty Weapon Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inWeaponSlot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Weapon
                        GameManager.Instance.weaponID = currentItem.itemID;
                        GameManager.Instance.SpawnItem("Weapon", GameManager.Instance.spawnedWeapon);
                    }
                    // Moving Into Helmet Slot
                    else if (helmetSlot && newItem.equipType == (int)InventoryItem.equipment.Helmet)
                    {
                        Debug.Log("Dropped Item: Empty Helmet Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inHelmetSlot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Helmet
                        GameManager.Instance.helmetID = currentItem.itemID;
                        GameManager.Instance.SpawnItem("Helmet", GameManager.Instance.spawnedHelmet);
                    }
                    // Moving Into Chest Slot
                    else if (chestSlot && newItem.equipType == (int)InventoryItem.equipment.Chest)
                    {
                        Debug.Log("Dropped Item: Empty Chest Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inChestSlot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Chest
                        GameManager.Instance.chestID = currentItem.itemID;
                        GameManager.Instance.SpawnItem("Chest", GameManager.Instance.spawnedChest);
                    }
                    // Moving Into Legs Slot
                    else if (legsSlot && newItem.equipType == (int)InventoryItem.equipment.Legs)
                    {
                        Debug.Log("Dropped Item: Empty Legs Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inLegsSlot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Legs
                        GameManager.Instance.legsID = currentItem.itemID;
                        GameManager.Instance.SpawnItem("Legs", GameManager.Instance.spawnedLegs);
                    }
                    // Moving Into Boots Slot
                    else if (bootsSlot && newItem.equipType == (int)InventoryItem.equipment.Boots)
                    {
                        Debug.Log("Dropped Item: Empty Boots Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inBootsSlot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Boots
                        GameManager.Instance.bootsID = currentItem.itemID;
                        GameManager.Instance.SpawnItem("Boots", GameManager.Instance.spawnedBoots);
                    }
                    // Moving Into Back Slot
                    else if (backSlot && newItem.equipType == (int)InventoryItem.equipment.Back)
                    {
                        Debug.Log("Dropped Item: Empty Back Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inBackSlot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Back
                        GameManager.Instance.backID = currentItem.itemID;
                        GameManager.Instance.SpawnItem("Back", GameManager.Instance.spawnedBack);
                    }
                    // Moving Into Shield Slot
                    else if (shieldSlot && newItem.equipType == (int)InventoryItem.equipment.Shield)
                    {
                        Debug.Log("Dropped Item: Empty Shield Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inShieldSlot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Shield
                        GameManager.Instance.shieldID = currentItem.itemID;
                        GameManager.Instance.SpawnItem("Shield", GameManager.Instance.spawnedShield);
                    }
                    // Moving Into Accessory 1 Slot
                    else if (accessory1Slot && newItem.equipType == (int)InventoryItem.equipment.Accessory)
                    {
                        Debug.Log("Dropped Item: Empty Accesory1 Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inAccessory1Slot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Accesory1
                        GameManager.Instance.accessory1ID = currentItem.itemID;
                        GameManager.Instance.SpawnItem("Accessory1", GameManager.Instance.spawnedAccessory1);
                    }
                    // Moving Into Accessory 2 Slot
                    else if (accessory2Slot && newItem.equipType == (int)InventoryItem.equipment.Accessory)
                    {
                        Debug.Log("Dropped Item: Empty Accesory2 Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inAccessory2Slot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Accesory2
                        GameManager.Instance.accessory2ID = currentItem.itemID;
                        GameManager.Instance.SpawnItem("Accessory2", GameManager.Instance.spawnedAccessory2);
                    }
                    // Moving Into Accessory 3 Slot
                    else if (accessory3Slot && newItem.equipType == (int)InventoryItem.equipment.Accessory)
                    {
                        Debug.Log("Dropped Item: Empty Accesory3 Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inAccessory3Slot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Accesory3
                        GameManager.Instance.accessory3ID = currentItem.itemID;
                        GameManager.Instance.SpawnItem("Accessory3", GameManager.Instance.spawnedAccessory3);
                    }
                    #endregion

                    #region Moving Out
                    else if (OriginalSlot.weaponSlot == true && newItem.inWeaponSlot == true && !helmetSlot && !chestSlot && !legsSlot && !bootsSlot && !backSlot && !shieldSlot && !accessory1Slot && !accessory2Slot && !accessory3Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inWeaponSlot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Weapon
                        GameManager.Instance.weaponID = -1;
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedWeapon);
                    }
                    else if (OriginalSlot.helmetSlot == true && newItem.inHelmetSlot == true && !weaponSlot && !chestSlot && !legsSlot && !bootsSlot && !backSlot && !shieldSlot&& !accessory1Slot && !accessory2Slot && !accessory3Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inHelmetSlot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Helmet
                        GameManager.Instance.helmetID = -1;
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedHelmet);
                    }
                    else if (OriginalSlot.chestSlot == true && newItem.inChestSlot == true && !helmetSlot && !weaponSlot && !legsSlot && !bootsSlot && !backSlot && !shieldSlot && !accessory1Slot && !accessory2Slot && !accessory3Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inChestSlot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Chest
                        GameManager.Instance.chestID = -1;
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedChest);
                    }
                    else if (OriginalSlot.legsSlot == true && newItem.inLegsSlot == true && !helmetSlot && !chestSlot && !weaponSlot && !bootsSlot && !backSlot && !shieldSlot&& !accessory1Slot && !accessory2Slot && !accessory3Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inLegsSlot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Legs
                        GameManager.Instance.legsID = -1;
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedLegs);
                    }
                    else if (OriginalSlot.bootsSlot == true && newItem.inBootsSlot == true && !helmetSlot && !chestSlot && !legsSlot && !weaponSlot && !backSlot && !shieldSlot&& !accessory1Slot && !accessory2Slot && !accessory3Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inBootsSlot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Boots
                        GameManager.Instance.bootsID = -1;
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedBoots);
                    }
                    else if (OriginalSlot.backSlot == true && newItem.inBackSlot == true && !helmetSlot && !chestSlot && !legsSlot && !bootsSlot && !weaponSlot && !shieldSlot && !accessory1Slot && !accessory2Slot && !accessory3Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inBackSlot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Back
                        GameManager.Instance.backID = -1;
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedBack);
                    }
                    else if (OriginalSlot.shieldSlot == true && newItem.inShieldSlot == true && !helmetSlot && !chestSlot && !legsSlot && !bootsSlot && !backSlot && !weaponSlot && !accessory1Slot && !accessory2Slot && !accessory3Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inShieldSlot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Shield
                        GameManager.Instance.shieldID = -1;
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedShield);
                    }
                    else if (OriginalSlot.accessory1Slot == true && newItem.inAccessory1Slot == true && !helmetSlot && !chestSlot && !legsSlot && !bootsSlot && !backSlot && !shieldSlot && !weaponSlot && !accessory2Slot && !accessory3Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inAccessory1Slot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Accessory1
                        GameManager.Instance.accessory1ID = -1;
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedAccessory1);
                    }
                    else if (OriginalSlot.accessory2Slot == true && newItem.inAccessory2Slot == true && !helmetSlot && !chestSlot && !legsSlot && !bootsSlot && !backSlot && !shieldSlot && !accessory1Slot && !weaponSlot && !accessory3Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inAccessory2Slot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Accessory2
                        GameManager.Instance.accessory2ID = -1;
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedAccessory2);
                    }
                    else if (OriginalSlot.accessory3Slot == true && newItem.inAccessory3Slot == true && !helmetSlot && !chestSlot && !legsSlot && !bootsSlot && !backSlot && !shieldSlot && !accessory1Slot && !weaponSlot && !weaponSlot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inAccessory3Slot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Accessory3
                        GameManager.Instance.accessory3ID = -1;
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedAccessory3);
                    }

                    #endregion
                    // Moving To Empty Slot
                    else if (!OriginalSlot.helmetSlot && !helmetSlot && !OriginalSlot.weaponSlot && !weaponSlot
                          && !OriginalSlot.shieldSlot && !shieldSlot && !OriginalSlot.chestSlot && !chestSlot
                          && !OriginalSlot.legsSlot && !legsSlot && !OriginalSlot.bootsSlot && !bootsSlot
                          && !OriginalSlot.backSlot && !backSlot && !OriginalSlot.accessory1Slot && !accessory1Slot
                          && !OriginalSlot.accessory2Slot && !accessory2Slot && !OriginalSlot.accessory3Slot && !accessory3Slot)
                    {
                        if(OriginalSlot.GetComponentInChildren<InventoryItem>() == null)
                        {
                            // Emptying Original Slot
                            OriginalSlot.isFull = false;
                            OriginalSlot.currentItem = null;
                        }

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;
                    }
                    // Returning to Original Slot
                    else
                    {
                        Debug.Log("Dropped Item: Returning to original slot");
                    }
                }
            }
        }
    }
        public void Swap(InventoryItem newItem, InventorySlot OriginalSlot) {
             // Swapping InventoryItem:currentItem 
            InventoryItem swapCurrent = currentItem;
            currentItem = newItem;
            OriginalSlot.currentItem = swapCurrent;
        }
}
