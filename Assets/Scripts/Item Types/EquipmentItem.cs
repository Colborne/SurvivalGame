using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{
    [Header("Equipment Information")]
    public int baseDefense;
    public string resistanceType;
    public int specialDefense;

    [Header("Bonuses")]
    public float jumpBonus = 1f;
    public float baseSpeedBonus = 1f;
    public float swimSpeedBonus = 1f;
}
