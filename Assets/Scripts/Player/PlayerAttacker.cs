using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttacker : MonoBehaviour
{
    AnimatorManager animatorManager;
    SoundManager soundManager;
    EquipmentManager equipmentManager;
    StatsManager statsManager;
    
    InputManager input;
    private void Awake() 
    {
        animatorManager = GetComponent<AnimatorManager>();
        soundManager = GetComponent<SoundManager>();
        equipmentManager = GetComponent<EquipmentManager>();
        statsManager = GetComponent<StatsManager>();
        input = FindObjectOfType<InputManager>();
    }

    public void HandleLightAttack(WeaponItem weaponItem)
    {
        equipmentManager.rightWeapon = weaponItem;
        if(!String.IsNullOrEmpty(weaponItem.Attack))
        {
            if(statsManager.currentStamina > equipmentManager.rightWeapon.baseStamina)
            {
                animatorManager.PlayTargetAnimation(weaponItem.Attack, true);
                animatorManager.animator.SetBool("isAttacking", true);
                soundManager.PlaySound("Sounds/handleSmallLeather2");
            }
        }
    }
    public void HandleHeavyAttack(WeaponItem weaponItem)
    {
        equipmentManager.rightWeapon = weaponItem;
        if(!String.IsNullOrEmpty(weaponItem.Heavy_Attack))
        {
            if(statsManager.currentStamina > equipmentManager.rightWeapon.baseStamina * equipmentManager.rightWeapon.heavyAttackMultiplier)
            {
                animatorManager.PlayTargetAnimation(weaponItem.Heavy_Attack, true);
                animatorManager.animator.SetBool("isAttacking", true);
                soundManager.PlaySound("Sounds/drawKnife2");
            }
        }
    }
    public void HandleLeftAction(WeaponItem weaponItem)
    {
        equipmentManager.leftWeapon = weaponItem;
        if(statsManager.currentStamina > equipmentManager.leftWeapon.baseStamina)
        {
            if(!String.IsNullOrEmpty(weaponItem.Left_Attack))
            {
                animatorManager.PlayTargetAnimation(weaponItem.Left_Attack, true);
                animatorManager.animator.SetBool("isAttacking", true);
                soundManager.PlaySound("Sounds/drawKnife2");
            }
        }
    }

    public void HandleEndBlock(WeaponItem weaponItem)
    {
        input.blockChargeTimer = 0f;
        equipmentManager.leftWeapon = weaponItem;
        animatorManager.PlayTargetAnimation(weaponItem.Left_Attack, true);
        animatorManager.animator.SetBool("isAttacking", true);
    }

    public void HandleString(string input, string settable, bool state, string sound)
    {
        animatorManager.PlayTargetAnimation(input, true);
        animatorManager.animator.SetBool(settable, state);
        soundManager.PlaySound(sound);
    }

    public void HandleRangedAction(WeaponItem weaponItem)
    {
        equipmentManager.rightWeapon = weaponItem;
        if(!String.IsNullOrEmpty(weaponItem.Attack))
        {
            animatorManager.PlayTargetAnimation(weaponItem.Attack, true);
            animatorManager.animator.SetBool("isAttacking", true);
            soundManager.PlaySound("Sounds/drawKnife2");
            
            var proj = Instantiate(weaponItem.projectile, transform.position + transform.forward + new Vector3(0,2,0), transform.rotation);
            float dm = proj.GetComponentInChildren<Projectile>().dc.damageAmount * equipmentManager.rightWeapon.attackDamage;
            proj.GetComponentInChildren<Projectile>().dc.damageAmount = (int)Mathf.Floor(dm);
            proj.GetComponentInChildren<Projectile>().dc.range = dm/10f;  
        }
    }

    public void HandleChargeAction(WeaponItem weaponItem)
    {
        animatorManager.PlayTargetAnimation("Charging", true);
        animatorManager.animator.SetBool("isAttacking", true);    
    }

    public void HandleBlockAction(WeaponItem weaponItem)
    {
        animatorManager.PlayTargetAnimation("Block", true);
        animatorManager.animator.SetBool("isAttacking", true);  
    }
}
