using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipmentSlotUI : MonoBehaviour
{
    UIManager uiManager;
    public Image icon;
    EquipmentItem equip;

    private void Awake() {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void AddItem(EquipmentItem newEquip)
    {
        equip = newEquip;
        icon.sprite = equip.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearItem()
    {
        equip = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }
}
