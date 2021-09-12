﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    [Header("Weapon Settings")]
    public bool isUnarmed;
    public bool isTwoHanded;
    public bool isOffhandable;
    public bool hasSpecial;
    public bool canCharge;

    [Header("Idle Animations")]
    public string Right_Hand_Idle;
    public string Left_Hand_Idle;
    public string Two_Hand_Idle;

    [Header("One Handed Attack Animations")]
    public string OH_Light_Attack;
    public string OH_Heavy_Attack;
    public string OH_Left_Attack;

    [Header("Speacialty Attack Animations")]
    public string Ranged_Attack;
    public string Magic_Attack;

    [Header("If Ranged")]
    public GameObject projectile;

    [Header("Stamina Costs")]
    public int baseStamina;
    public int manaCost;

    [Header("Damage")]
    public float heavyAttackMultiplier;
    public float attackSpeed;
}