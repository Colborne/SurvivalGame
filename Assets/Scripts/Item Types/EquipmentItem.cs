using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{
    [Header("Equipment Information")]
    public int baseDefense;
    public string resistanceType;
    public int specialDefense;

    [Header("Attack Bonuses")]
    public float strengthBonus = 0f;
    public float rangeBonus = 0f;
    public float mageBonus = 0f;

    [Header("Movement Bonuses")]
    public float jumpBonus = 1f;
    public float baseSpeedBonus = 1f;
    public float swimSpeedBonus = 1f;
}
