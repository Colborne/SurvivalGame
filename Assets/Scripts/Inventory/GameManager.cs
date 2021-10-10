using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class ingameEquipment
{
    public string name = "Equipment";
    public GameObject prefab;
    public GameObject inventoryItem;
    public GameObject worldItem;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int weaponID = -1;
    public int shieldID = -1;
    public int helmetID = -1;
    public int chestID = -1;
    public int legsID = -1;
    public int bootsID = -1;
    public int backID = -1;
    public int accessory1ID = -1;
    public int accessory2ID = -1;
    public int accessory3ID = -1;

    public GameObject spawnedWeapon;
    public GameObject spawnedShield;
    public GameObject spawnedHelmet;
    public GameObject spawnedChest;
    public GameObject spawnedLegs;
    public GameObject spawnedBoots;
    public GameObject spawnedBack;
    public GameObject spawnedAccessory1;
    public GameObject spawnedAccessory2;
    public GameObject spawnedAccessory3;

    public PlayerManager PM;
    public EquipmentManager equipmentManager;
    StatsManager statsManager;

    public InventorySlot[] inventorySlots;
    public Canvas interfaceCanvas;
    public Transform draggables;

    public ingameEquipment[] equipment;

    // Main //
    void Awake()
    {
        GameManager.Instance = this;
        equipmentManager = FindObjectOfType<EquipmentManager>();
        statsManager = FindObjectOfType<StatsManager>();
    }

    public void DestroyItem(GameObject item)
    {
        if (item != null)
        {
            Destroy(item);
        }
    }

    public void SpawnItem(string type, GameObject item)
    {
        DestroyItem(item);

        if(type == "Weapon" && weaponID != -1)
        {
            spawnedWeapon = Instantiate(equipment[weaponID].prefab, PM.rightHand);
            equipmentManager.rightWeapon = spawnedWeapon.GetComponent<weaponItemLoader>().item;
            equipmentManager.LoadWeaponOnSlot(equipmentManager.rightWeapon, false);
        }

        if(type == "Shield" && shieldID != -1)
        {
            spawnedShield = Instantiate(equipment[shieldID].prefab, PM.leftHand);
            equipmentManager.leftWeapon = spawnedShield.GetComponent<weaponItemLoader>().item;
            equipmentManager.LoadWeaponOnSlot(equipmentManager.leftWeapon, true);
        }

        if(type == "Helmet" && helmetID != -1)
        {
            spawnedHelmet = Instantiate(equipment[helmetID].prefab, PM.helmet);
        }
         
        if(type == "Chest" && chestID != -1)
        {
            spawnedChest = Instantiate(equipment[chestID].prefab, PM.chest);
        }
        
        if(type == "Legs" && legsID != -1)
        {
            spawnedLegs = Instantiate(equipment[legsID].prefab, PM.legs);
        }
        
        if(type == "Boots" && bootsID != -1)
        {
            spawnedBoots = Instantiate(equipment[bootsID].prefab, PM.boots);
        }
        
        if(type == "Back" && backID != -1)
        {
            spawnedBack = Instantiate(equipment[backID].prefab, PM.back);
        }
        
        if(type == "Accessory1" && accessory1ID != -1)
        {
            spawnedAccessory1 = Instantiate(equipment[accessory1ID].prefab, PM.body);
        }
        
        if(type == "Accessory2" && accessory2ID != -1)
        {
            spawnedAccessory2 = Instantiate(equipment[accessory2ID].prefab, PM.body);
        }
        
        if(type == "Accessory3" && accessory3ID != -1)
        {
            spawnedAccessory3 = Instantiate(equipment[accessory3ID].prefab, PM.body);
        }
    }

    public void PickupItem(int itemID)
    {
        bool foundSlot = false;

        for (int i = 10; i < inventorySlots.Length; i++)
        {
            if (!inventorySlots[i].isFull)
            {
                GameObject GO = Instantiate(equipment[itemID].inventoryItem, inventorySlots[i].gameObject.transform);
                inventorySlots[i].currentItem = GO.GetComponent<InventoryItem>();
                inventorySlots[i].isFull = true;
                foundSlot = true;
                
                inventorySlots[i].GetComponent<TooltipTrigger>().header =  inventorySlots[i].currentItem.item.itemName;
                inventorySlots[i].GetComponent<TooltipTrigger>().content =  inventorySlots[i].currentItem.item.GetTooltipInfoText();       
                break;
            }
        }

        if(foundSlot == false)
        {
            Debug.Log("noslot");
            Instantiate(equipment[itemID].worldItem, PM.transform.position + new Vector3(0, 10, 0), Quaternion.identity);
        }
    }


    #region PickUp Stack
    public void PickUpItem(int itemID, int quantityIncrease)
    {
        // Searches for identical item ID in inventory //
        for (int i = 10; i < inventorySlots.Length; i++)
        {    
            //Found a full slot and the id matches
            if (inventorySlots[i].isFull && inventorySlots[i].currentItem.itemID == itemID)
            {
                Debug.Log(i + ": Current Amount: " + inventorySlots[i].currentItem.currentAmount + " this " + quantityIncrease);
                int newAmount = inventorySlots[i].currentItem.currentAmount + quantityIncrease;
                if(inventorySlots[i].currentItem.MaxAmount >= newAmount)
                {
                    Debug.Log(inventorySlots[i].currentItem.MaxAmount + " >= " + newAmount);

                    inventorySlots[i].currentItem.currentAmount += quantityIncrease;

                    inventorySlots[i].GetComponent<TooltipTrigger>().header =  inventorySlots[i].currentItem.item.itemName;
                    inventorySlots[i].GetComponent<TooltipTrigger>().content =  inventorySlots[i].currentItem.item.GetTooltipInfoText();
                    return;
                }
                else
                {
                    Debug.Log(inventorySlots[i].currentItem.MaxAmount + " < " + newAmount);
                    inventorySlots[i].currentItem.currentAmount = inventorySlots[i].currentItem.MaxAmount;

                    inventorySlots[i].GetComponent<TooltipTrigger>().header =  inventorySlots[i].currentItem.item.itemName;
                    inventorySlots[i].GetComponent<TooltipTrigger>().content =  inventorySlots[i].currentItem.item.GetTooltipInfoText();

                    quantityIncrease = newAmount - inventorySlots[i].currentItem.MaxAmount;
                    Debug.Log("Remainder that should be rounded over: " + quantityIncrease);          
                }
            }
            //Found a full slot but the id does not match
            else if (inventorySlots[i].isFull && inventorySlots[i].currentItem.itemID != itemID)
            {
                Debug.Log("Can't Place here");
            }
            //Found an empty slot
            else if (!inventorySlots[i].isFull)
            { 
                Debug.Log("Empty Slot");   
                GameObject GO = Instantiate(equipment[itemID].inventoryItem, inventorySlots[i].gameObject.transform);
                GO.GetComponent<InventoryItem>().originalSlot = inventorySlots[i].transform;
                inventorySlots[i].currentItem = GO.GetComponent<InventoryItem>();
                inventorySlots[i].isFull = true;
                inventorySlots[i].currentItem.currentAmount = quantityIncrease;

                inventorySlots[i].GetComponent<TooltipTrigger>().header =  inventorySlots[i].currentItem.item.itemName;
                inventorySlots[i].GetComponent<TooltipTrigger>().content =  inventorySlots[i].currentItem.item.GetTooltipInfoText();

                if(inventorySlots[i].currentItem.MaxAmount >= inventorySlots[i].currentItem.currentAmount)
                    return;
                else
                {
                    Debug.Log(inventorySlots[i].currentItem.MaxAmount + " < " + quantityIncrease);
                    inventorySlots[i].currentItem.currentAmount = inventorySlots[i].currentItem.MaxAmount;

                    quantityIncrease -= inventorySlots[i].currentItem.MaxAmount;
                    Debug.Log("Remainder that should be rounded over: " + quantityIncrease);      
                }
            }
            //No Slots Left
            else
            {
                Debug.Log("noslot");
                var obj = Instantiate(equipment[itemID].worldItem, PM.transform.position + new Vector3(0, 10, 0), Quaternion.identity);
                obj.GetComponent<Pickup>().amount = quantityIncrease;
                return;
            }
        }
    }
    #endregion

    public void ClearItem(InventoryItem item)
    {
        Destroy(item.gameObject);
    }

    public void DropItem(InventoryItem item)
    {
        Instantiate(equipment[item.itemID].worldItem, PM.transform.position + PM.transform.forward * 3, Quaternion.identity);
        Destroy(item.gameObject);
    }

    public void DropItem(InventoryItem item, int _amount)
    {
        var _newItem = Instantiate(equipment[item.itemID].worldItem, PM.transform.position + PM.transform.forward * 3, Quaternion.identity);
        _newItem.GetComponent<Pickup>().amount = _amount;
        Destroy(item.gameObject);
    }

    #region PickUp Stack
    public void StackRounding(int itemID, int quantityIncrease, Transform originalSlot)
    {
        // Searches for identical item ID in inventory //
        for (int i = 10; i < inventorySlots.Length; i++)
        {    
            if (!inventorySlots[i].isFull)
            { 
                GameObject GO = Instantiate(equipment[itemID].inventoryItem, inventorySlots[i].gameObject.transform);
                GO.GetComponent<InventoryItem>().originalSlot = inventorySlots[i].transform;
                inventorySlots[i].currentItem = GO.GetComponent<InventoryItem>();
                inventorySlots[i].isFull = true;
                inventorySlots[i].currentItem.currentAmount = quantityIncrease;
                return;
            }
        }
    }
    #endregion

    public bool CheckInventoryForItem(InventoryItem item, int amount, bool remove)
    {
        int amountFound = 0;
        List<int> foundSlots = new List<int>();

        for (int i = 10; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].isFull && inventorySlots[i].currentItem.itemID == item.itemID)
            {
                foundSlots.Add(i);
                amountFound += inventorySlots[i].currentItem.currentAmount; 
            }
            
            if(amountFound >= amount)
                break;     
        }

        if(amountFound < amount)
            return false;

        foreach(int i in foundSlots)
        {
            amountFound -= inventorySlots[i].currentItem.currentAmount;
            
            if(remove)
            {
                if(inventorySlots[i].currentItem.currentAmount > amount)
                    inventorySlots[i].currentItem.currentAmount -= amount;
                else
                    inventorySlots[i].currentItem.currentAmount = 0;

                if(inventorySlots[i].currentItem.currentAmount == 0)
                {
                    inventorySlots[i].currentItem = null;
                    inventorySlots[i].isFull = false;
                    inventorySlots[i].GetComponent<TooltipTrigger>().header = null;
                    inventorySlots[i].GetComponent<TooltipTrigger>().content = null;
                    Destroy(inventorySlots[i].transform.GetChild(0).gameObject);
                }   
            }

            if(amountFound <= 0)
                return true;
        }
        return false;
    }

    public int ReplaceStack(InventoryItem item)
    {
        for (int i = 10; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].isFull && inventorySlots[i].currentItem.itemID == item.itemID)
            {
                int amount = inventorySlots[i].currentItem.currentAmount;
                inventorySlots[i].currentItem = null;
                inventorySlots[i].isFull = false;
                inventorySlots[i].GetComponent<TooltipTrigger>().header = null;
                inventorySlots[i].GetComponent<TooltipTrigger>().content = null;
                Destroy(inventorySlots[i].transform.GetChild(0).gameObject);
                return amount;
            }
        }
        return 0;
    }

    public bool CraftingCheck(InventoryItem[] items, int[] amounts) 
    {
        //Checks Entire Inventory For Possible Craftable Items
        for(int x = 0; x < items.Length; x++)
        {
            int amountFound = 0;
            for (int i = 10; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].isFull && inventorySlots[i].currentItem.itemID == items[x].itemID)
                    amountFound += inventorySlots[i].currentItem.currentAmount; 
            }

            if(amountFound < amounts[x])
                return false;     
        }
        return true;
    }

    public int CheckAmount(InventoryItem item)
    {
        for (int i = 10; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].isFull && inventorySlots[i].currentItem.itemID == item.itemID)
            {
                return inventorySlots[i].currentItem.currentAmount;
            }
        }
        return 0;
    }

    public void Craft(InventoryItem[] items, int[] amounts) 
    {
        //Checks Entire Inventory For Possible Craftable Items
        for(int x = 0; x < items.Length; x++)
        {
            CheckInventoryForItem(items[x], amounts[x], true);
        }
    }

    public bool CheckIfEmpty()
    {
        for (int i = 10; i < inventorySlots.Length; i++)
        {
            if (!inventorySlots[i].isFull)
            {
                return true;
            }
        }
        return false;
    }
}