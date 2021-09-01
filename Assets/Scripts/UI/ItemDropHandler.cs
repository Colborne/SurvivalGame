using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{    
    InputManager inputManager;
    UIManager uiManager;
    InventorySlot item;
    public RectTransform invPanel;
    public RectTransform equipmentPanelLeftHand;
    public RectTransform equipmentPanelRightHand;
    public RectTransform equipmentPanelHelmet;
 

    private void Awake() {
        inputManager = FindObjectOfType<InputManager>();
        uiManager = FindObjectOfType<UIManager>();
        item = GetComponent<InventorySlot>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(!RectTransformUtility.RectangleContainsScreenPoint(invPanel, inputManager.mousePosition))
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(equipmentPanelLeftHand, inputManager.mousePosition))
            {
                item.EquipThisItem("Left");
            }
            else if(RectTransformUtility.RectangleContainsScreenPoint(equipmentPanelRightHand, inputManager.mousePosition))
            {
                item.EquipThisItem("Right");
            }
            else if(RectTransformUtility.RectangleContainsScreenPoint(equipmentPanelHelmet, inputManager.mousePosition))
            {
                item.EquipThisItem("Helmet");
            }
            else
            {
                item.onDrop(inputManager.mousePosition);
            }
        }
    }
}
