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

    public InventorySlot[] inventorySlots;
    public Canvas interfaceCanvas;
    public Transform draggables;

    public ingameEquipment[] equipment;

    // Main //
    void Awake()
    {
        GameManager.Instance = this;
        equipmentManager = FindObjectOfType<EquipmentManager>();
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
            GameObject GO = Instantiate(equipment[itemID].inventoryItem, inventorySlots[i].gameObject.transform);
            //Found an empty slot
            if (!inventorySlots[i].isFull)
            {
                Debug.Log("Empty Slot");   
                inventorySlots[i].currentItem = GO.GetComponent<InventoryItem>();
                inventorySlots[i].isFull = true;
                inventorySlots[i].currentItem.currentAmount = quantityIncrease;
                break;
            }
            //Found a full slot and the id matches
            else if (inventorySlots[i].isFull && inventorySlots[i].currentItem.itemID == itemID)
            {
                int newAmount = inventorySlots[i].currentItem.currentAmount + quantityIncrease;
                if(inventorySlots[i].currentItem.MaxAmount >= newAmount)
                {
                    Debug.Log(inventorySlots[i].currentItem.MaxAmount + " >= " + newAmount);
                    inventorySlots[i].currentItem.currentAmount += quantityIncrease;
                    //inventorySlots[i].currentItem = GO.GetComponent<InventoryItem>();
                    break;
                }
                else
                {
                    Debug.Log(inventorySlots[i].currentItem.MaxAmount + " < " + newAmount);
                    inventorySlots[i].currentItem.currentAmount = inventorySlots[i].currentItem.MaxAmount;

                    int remainder = newAmount - inventorySlots[i].currentItem.MaxAmount;
                    Debug.Log("Remainder that should be rounded over: " + remainder);
                    //try{GameManager.Instance.PickUpItem(itemID, remainder);}catch{}
                    break;              
                }
            }
            //Found a full slot but the id does not match
            else if (inventorySlots[i].isFull && inventorySlots[i].currentItem.itemID != itemID)
            {
                Debug.Log("Can't Place here");
            }
            //No Slots Left
            else
            {
                Debug.Log("noslot");
                Instantiate(equipment[itemID].worldItem, PM.transform.position + new Vector3(0, 10, 0), Quaternion.identity);
            }
        }
    }
    #endregion

    public void DropItem(InventoryItem item)
    {
        Instantiate(equipment[item.itemID].worldItem, PM.transform.position + new Vector3(1f, 0.5f, 0), Quaternion.identity);
        Destroy(item.gameObject);
    }

    public void DropItem(InventoryItem item, int _amount)
    {
        var _newItem = Instantiate(equipment[item.itemID].worldItem, PM.transform.position + new Vector3(1f, 0.5f, 0), Quaternion.identity);
        _newItem.GetComponent<Pickup>().amount = _amount;
        Destroy(item.gameObject);
    }
}
