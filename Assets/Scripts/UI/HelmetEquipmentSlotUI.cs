using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HelmetEquipmentSlotUI : MonoBehaviour
{
    UIManager uiManager;
    public Image icon;
    HelmetItem helmetItem;

    private void Awake() {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void AddItem(HelmetItem newHelmet)
    {
        helmetItem = newHelmet;
        icon.sprite = helmetItem.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearItem()
    {
        helmetItem = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }
}
