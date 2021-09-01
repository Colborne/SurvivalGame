using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentWindowUI : MonoBehaviour
{
    public HandEquipmentSlotUI rightHandEquipmentSlotUI;
    public HandEquipmentSlotUI leftHandEquipmentSlotUI;
    public HelmetEquipmentSlotUI helmetEquipmentSlotUI;

    public void LoadRightWeaponOnEquipmentScreen(PlayerInventory playerInventory)
    {
        rightHandEquipmentSlotUI.AddItem(playerInventory.rightWeapon);
    }
    
    public void LoadLeftWeaponOnEquipmentScreen(PlayerInventory playerInventory)
    {
        leftHandEquipmentSlotUI.AddItem(playerInventory.leftWeapon);
    }

    public void LoadHelmetOnEquipmentScreen(PlayerInventory playerInventory)
    {
        helmetEquipmentSlotUI.AddItem(playerInventory.helmet);
    }
}
