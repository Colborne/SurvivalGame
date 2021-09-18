using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    [Header("Weapon Settings")]
    public bool isTwoHanded;
    public bool isOffhandable;
    public bool canCharge;

    [Header("Attack Animations")]
    public string Attack;
    public string Heavy_Attack;
    public string Left_Attack;

    [Header("Stats")]
    public int baseStamina;
    public float heavyAttackMultiplier;
    public float attackDamage;
    public float attackSpeed;
    
    [Header("If Ranged")]
    public GameObject projectile;
}