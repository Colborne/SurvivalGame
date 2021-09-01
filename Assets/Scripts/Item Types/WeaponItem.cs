using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    [Header("Item Settings")]
    public bool isUnarmed;
    public bool isTwoHanded;
    public bool hasSpecial;


    [Header("Idle Animations")]
    public string Right_Hand_Idle;
    public string Left_Hand_Idle;
    public string Two_Hand_Idle;

    [Header("One Handed Attack Animations")]
    public string OH_Light_Attack;
    public string OH_Heavy_Attack;
    public string OH_Left_Attack;

    [Header("Stamina Costs")]
    public int baseStamina;

    [Header("Damage")]
    public float heavyAttackMultiplier;

}
