using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Resource Item")]
public class ResourceItem : Item
{
    public GameObject modelPrefab;
    public string resourceType;
}
