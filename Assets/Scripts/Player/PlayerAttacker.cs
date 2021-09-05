using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    AnimatorManager animatorManager;
    SoundManager soundManager;
    EquipmentManager equipmentManager;
    StatsManager statsManager;
    private void Awake() 
    {
        animatorManager = GetComponent<AnimatorManager>();
        soundManager = GetComponent<SoundManager>();
        equipmentManager = GetComponent<EquipmentManager>();
        statsManager = GetComponent<StatsManager>();
    }

    public void HandleLightAttack(WeaponItem weaponItem)
    {
        equipmentManager.rightWeapon = weaponItem;
        if(statsManager.currentStamina > equipmentManager.rightWeapon.baseStamina)
        {
            animatorManager.PlayTargetAnimation(weaponItem.OH_Light_Attack, true);
            animatorManager.animator.SetBool("isAttacking", true);
            soundManager.PlaySound("Sounds/handleSmallLeather2");
        }
    }
    public void HandleHeavyAttack(WeaponItem weaponItem)
    {
        equipmentManager.rightWeapon = weaponItem;
        if(statsManager.currentStamina > equipmentManager.rightWeapon.baseStamina * equipmentManager.rightWeapon.heavyAttackMultiplier)
        {
            animatorManager.PlayTargetAnimation(weaponItem.OH_Heavy_Attack, true);
            animatorManager.animator.SetBool("isAttacking", true);
            soundManager.PlaySound("Sounds/drawKnife2");
        }
    }
    public void HandleLeftAction(WeaponItem weaponItem)
    {
        equipmentManager.leftWeapon = weaponItem;
        if(statsManager.currentStamina > equipmentManager.leftWeapon.baseStamina)
        {
            animatorManager.PlayTargetAnimation(weaponItem.OH_Left_Attack, true);
            animatorManager.animator.SetBool("isAttacking", true);
            soundManager.PlaySound("Sounds/drawKnife2");
        }
    }
}
