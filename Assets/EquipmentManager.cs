using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collect.Containers;
using Collect.Items;
using Collect.Slots;

public class EquipmentManager : MonoBehaviour
{
    public StandardContainer equipment;
    public PlayerInventory playerInventory;

    private void Awake() {
        equipment = GetComponent<StandardContainer>();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }
    private void LateUpdate() 
    {
        if(equipment != null)
        {
            foreach(Slot slot in equipment.Slots) 
            {
                SlotWithType type = slot as SlotWithType;
                if(type.Type == ItemType.Head)
                {
                    if(slot.Item == null)
                    {
                        playerInventory.helmetSlotManager.helmetSlot.UnloadHelmetDestroy();
                        playerInventory.helmetSlotManager.LoadHelmetOnSlot(null);
                    }
                    else
                    {
                        playerInventory.helmetSlotManager.LoadHelmetOnSlot(slot.Item._item as HelmetItem);
                    }
                }
            }
        }
    }
}
