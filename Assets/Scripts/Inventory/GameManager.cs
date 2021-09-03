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

    public InventorySlot[] inventorySlots;
    public Canvas interfaceCanvas;
    public Transform draggables;

    public ingameEquipment[] equipment;

    // Main //
    void Awake()
    {
        GameManager.Instance = this;
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
        }

        if(type == "Shield" && shieldID != -1)
        {
            spawnedHelmet = Instantiate(equipment[shieldID].prefab, PM.leftHand);
        }

        if(type == "Helmet" && helmetID != -1)
        {
            spawnedHelmet = Instantiate(equipment[helmetID].prefab, PM.helmet);
        }
         
        if(type == "Chest" && chestID != -1)
        {
            spawnedHelmet = Instantiate(equipment[chestID].prefab, PM.chest);
        }
        
        if(type == "Legs" && legsID != -1)
        {
            spawnedWeapon = Instantiate(equipment[legsID].prefab, PM.legs);
        }
        
        if(type == "Boots" && bootsID != -1)
        {
            spawnedHelmet = Instantiate(equipment[bootsID].prefab, PM.boots);
        }
        
        if(type == "Back" && backID != -1)
        {
            spawnedWeapon = Instantiate(equipment[backID].prefab, PM.back);
        }
        
        if(type == "Accessory1" && accessory1ID != -1)
        {
            spawnedHelmet = Instantiate(equipment[accessory1ID].prefab, PM.body);
        }
        
        if(type == "Accessory2" && accessory2ID != -1)
        {
            spawnedWeapon = Instantiate(equipment[accessory2ID].prefab, PM.body);
        }
        
        if(type == "Accessory3" && accessory3ID != -1)
        {
            spawnedWeapon = Instantiate(equipment[accessory3ID].prefab, PM.body);
        }
    }

    public void PickupItem(int itemID)
    {
        bool foundSlot = false;

        for (int i = 2; i < inventorySlots.Length; i++)
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
    public void DropItem(InventoryItem item)
    {
        Instantiate(equipment[item.itemID].worldItem, PM.transform.position + new Vector3(1f, 0.5f, 0), Quaternion.identity);
        Destroy(item.gameObject);
    }
}
