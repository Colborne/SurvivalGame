using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    GameManager gameManager;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public DamageCollider leftHandDamageCollider;
    public DamageCollider rightHandDamageCollider;

    StatsManager stats;
    Animator animator;

    private void Awake() 
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        stats = GetComponentInParent<StatsManager>();
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if(weaponItem.isTwoHanded)
        {
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
        leftHandDamageCollider = gameManager.spawnedShield.GetComponentInChildren<DamageCollider>();   
    }

    public void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = gameManager.spawnedWeapon.GetComponentInChildren<DamageCollider>();
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
        float multiplier = rightWeapon.heavyAttackMultiplier;
        stats.UseStamina(Mathf.RoundToInt(rightWeapon.baseStamina * multiplier));
    }
    public void DrainStaminaLight()
    {
        stats.UseStamina(Mathf.RoundToInt(rightWeapon.baseStamina));
    }
}
