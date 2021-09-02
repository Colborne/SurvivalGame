using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public GameObject modelPrefab;
    [Header("Item Information")]
    public Sprite itemIcon;
    public string itemName;
    
}
