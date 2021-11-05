using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public float[] pos;
    public float[] rot;

    [Header("Health")]
    public int healthLevel = 1;
    public int maxHealth;
    public int currentHealth;

    [Header("Stamina")]
    public int staminaLevel = 1;
    public float maxStamina;
    public float currentStamina;

    public PlayerData(Player player) 
    {
        pos = new float[3];
        rot = new float[3];
        pos[0] = player.transform.position.x;
        pos[1] = player.transform.position.y;
        pos[2] = player.transform.position.z;
        rot[0] = player.transform.eulerAngles.x;
        rot[1] = player.transform.eulerAngles.y;
        rot[2] = player.transform.eulerAngles.z;

        healthLevel = player.sm.healthLevel;
        maxHealth = player.sm.maxHealth;
        currentHealth = player.sm.currentHealth;
        staminaLevel = player.sm.staminaLevel;
        maxStamina = player.sm.maxStamina;
        currentStamina = player.sm.currentStamina;
    }
}
