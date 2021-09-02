using UnityEngine;

namespace Collect.Items {

    /// <summary>
    /// Defines all different item types
    /// </summary>
    public enum ItemType {
        Head,
        Chest,
        Back,
        Legs,
        Boots,
        RightHand,
        LeftHand,
        Accessory
    };

    /// <summary>
    /// Component for exposing `ItemType` on an object.
    /// GameObject's with a `DraggableItemType` can be
    /// can be placed in `Slot`'s (`SlotWithType`), that
    /// match their `ItemType`.
    /// </summary>
    [AddComponentMenu("Collect/Items/Draggable Item Type")]
    public class DraggableItemType : MonoBehaviour {

        [Tooltip("Type of this item")]
        public ItemType ItemType;
        
    }
}
