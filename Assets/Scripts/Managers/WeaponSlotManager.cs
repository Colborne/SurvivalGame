using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{

    DamageCollider leftHandDamageCollider;
    DamageCollider rightHandDamageCollider;

    StatsManager stats;
    public WeaponItem weapon;
    Animator animator;
/*
    private void Awake() 
    {
        animator = GetComponent<Animator>();
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        stats = GetComponentInParent<StatsManager>();
        foreach(WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if(weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }
            else if(weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
        }
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if(weaponItem.isTwoHanded)
        {
            rightHandSlot.LoadWeaponModel(weaponItem);
            LoadRightWeaponDamageCollider();
            #region Handle Two Hand Idles
            if(weaponItem != null)
            {
                animator.CrossFade(weaponItem.Two_Hand_Idle, .2f);
            }
            else
            {
                animator.CrossFade("Both Arms Empty", .2f);
            }
            #endregion
        }
        else
        {
            if(isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                #region Handle Left Idles
                if(weaponItem != null)
                {
                    animator.CrossFade(weaponItem.Left_Hand_Idle, .2f);
                }
                else
                {
                    animator.CrossFade("Left Arm Empty", .2f);
                }
                #endregion
            }
            else
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                #region Handle Right Idles
                if(weaponItem != null)
                {
                    animator.CrossFade(weaponItem.Right_Hand_Idle, .2f);
                }
                else
                {
                    animator.CrossFade("Right Arm Empty", .2f);
                }
                #endregion
            }
        }
    }
    #region Damage Colliders
    public void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();   
    }

    public void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    public void OpenLeftDamageCollider()
    {  
        leftHandDamageCollider.EnableDamageCollider();
    }
    public void OpenRightDamageCollider()
    {  
        rightHandDamageCollider.EnableDamageCollider();
    }
    public void CloseLeftDamageCollider()
    {  
        leftHandDamageCollider.DisableDamageCollider();
    }
    public void CloseRightDamageCollider()
    {  
        rightHandDamageCollider.DisableDamageCollider();
    }
    #endregion

    public void DrainStaminaHeavy()
    {
        float multiplier = weapon.heavyAttackMultiplier;
        stats.UseStamina(Mathf.RoundToInt(weapon.baseStamina * multiplier));
    }
    public void DrainStaminaLight()
    {
        stats.UseStamina(Mathf.RoundToInt(weapon.baseStamina));
    }
    */
}
