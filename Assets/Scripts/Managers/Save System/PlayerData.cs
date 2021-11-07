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

    [Header("Inventory")]
    public int[] slot;
    public int[] id;
    public int[] amount;
    public bool[] helmet;
    public bool[] weapon;
    public bool[] chest;
    public bool[] legs;
    public bool[] boots;
    public bool[] back;
    public bool[] shield;
    public bool[] acc1;
    public bool[] acc2;
    public bool[] acc3;
    
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

        slot = new int[42];
        id = new int[42];
        amount = new int[42];

        helmet = new bool[10];
        weapon = new bool[10];
        chest = new bool[10];
        legs = new bool[10];
        boots = new bool[10];
        back = new bool[10];
        shield = new bool[10];
        acc1 = new bool[10];
        acc2 = new bool[10];
        acc3 = new bool[10];

        for(int i = 0; i < player.ism.eq.Length; i++)
        {
            helmet[i] = player.ism.eq[i].helmet;
            weapon[i] = player.ism.eq[i].weapon;
            chest[i] = player.ism.eq[i].chest;
            legs[i] = player.ism.eq[i].legs;
            boots[i] = player.ism.eq[i].boots;
            back[i] = player.ism.eq[i].back;
            shield[i] = player.ism.eq[i].shield;
            acc1[i] = player.ism.eq[i].acc1;
            acc2[i] = player.ism.eq[i].acc2;
            acc3[i] = player.ism.eq[i].acc3;
        }

        for(int i = 0; i < player.ism.inv.Length; i++)
        {
            slot[i] = player.ism.inv[i].slot;
            id[i] = player.ism.inv[i].itemID;
            amount[i] = player.ism.inv[i].currentAmount;
        }
    }
}
