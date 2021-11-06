using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableObject : MonoBehaviour
{
    public enum itemType
    {
        Altars,
        Buildable,
        Consumable,
        Equipment,
        Mobs,
        Ores,
        Others,
        ResourceItems,
        Trees,
        Weapons
    }
    
    public string AssetPath;
    public itemType folder;

}
