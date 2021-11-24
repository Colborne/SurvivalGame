using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StatsManager sm;
    public InventorySaveManager ism;

    private void Awake() {
        ism = FindObjectOfType<InventorySaveManager>();
    }

    public void SaveGame()
    {
        SaveLoad.SaveData(this);
    }

    public void Load()
    {
        PlayerData data = SaveLoad.LoadData();
        transform.position = new Vector3(data.pos[0],data.pos[1],data.pos[2]);
        transform.rotation = Quaternion.Euler(new Vector3(data.rot[0],data.rot[1],data.rot[2]));

        sm.healthLevel = data.healthLevel;
        sm.maxHealth = data.maxHealth;
        sm.currentHealth = data.currentHealth;
        sm.staminaLevel = data.staminaLevel;
        sm.maxStamina = data.maxStamina;
        sm.currentStamina = data.currentStamina;

        sm.healthBar.SetMaxHealth(data.maxHealth);
        sm.healthBar.SetCurrentHealth(data.currentHealth);

        sm.staminaBar.SetMaxStamina(data.maxStamina);
        sm.staminaBar.SetCurrentStamina(data.currentStamina);

        string itemType = "";

        for(int i = 0; i < 10; i++)
        {
            if(data.helmet[i]) itemType = "Helmet";
            else if(data.weapon[i]) itemType = "Weapon";
            else if(data.chest[i]) itemType = "Chest";
            else if(data.legs[i]) itemType = "Legs";
            else if(data.boots[i]) itemType = "Boots";
            else if(data.back[i]) itemType = "Back";
            else if(data.shield[i]) itemType = "Shield";
            else if(data.acc1[i]) itemType = "Accessory1";
            else if(data.acc2[i]) itemType = "Accessory2";
            else if(data.acc3[i]) itemType = "Accessory3";

            if(data.slot[i] != 0)
                GameManager.Instance.LoadEquipmentItem(data.slot[i], data.id[i], data.amount[i], itemType);  
        }
        
        for(int i = 0; i < 32; i++)
        {
            if(data.slot[i] + 10 != 0)
                GameManager.Instance.LoadInventoryItem(data.slot[i] + 10, data.id[i] + 10, data.amount[i] + 10);  
        }        
    }
}
