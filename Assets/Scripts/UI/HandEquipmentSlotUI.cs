using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HandEquipmentSlotUI : MonoBehaviour
{
    UIManager uiManager;
    public Image icon;
    WeaponItem weapon;
    private void Awake() {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void AddItem(WeaponItem newWeapon)
    {
        weapon = newWeapon;
        icon.sprite = weapon.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearItem()
    {
        weapon = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }
}
