using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StatsManager sm;

    void Start()
    {

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
    }
}
