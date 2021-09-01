using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    AnimatorManager animatorManager;
    SoundManager soundManager;
    WeaponSlotManager weaponSlotManager;
    StatsManager statsManager;
    private void Awake() 
    {
        animatorManager = GetComponent<AnimatorManager>();
        soundManager = GetComponent<SoundManager>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
        statsManager = GetComponent<StatsManager>();
    }

    public void HandleLightAttack(WeaponItem weaponItem)
    {
        weaponSlotManager.weapon = weaponItem;
        if(statsManager.currentStamina > weaponSlotManager.weapon.baseStamina)
        {
            animatorManager.PlayTargetAnimation(weaponItem.OH_Light_Attack, true);
            animatorManager.animator.SetBool("isAttacking", true);
            soundManager.PlaySound("Sounds/handleSmallLeather2");
        }
    }
    public void HandleHeavyAttack(WeaponItem weaponItem)
    {
        weaponSlotManager.weapon = weaponItem;
        if(statsManager.currentStamina > weaponSlotManager.weapon.baseStamina * weaponSlotManager.weapon.heavyAttackMultiplier)
        {
            animatorManager.PlayTargetAnimation(weaponItem.OH_Heavy_Attack, true);
            animatorManager.animator.SetBool("isAttacking", true);
            soundManager.PlaySound("Sounds/drawKnife2");
        }
    }
    public void HandleLeftAction(WeaponItem weaponItem)
    {
        weaponSlotManager.weapon = weaponItem;
        if(statsManager.currentStamina > weaponSlotManager.weapon.baseStamina)
        {
            animatorManager.PlayTargetAnimation(weaponItem.OH_Left_Attack, true);
            animatorManager.animator.SetBool("isAttacking", true);
            soundManager.PlaySound("Sounds/drawKnife2");
        }
    }
}
