using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(menuName = "Items/Resource Item")]
public class ResourceItem : Item
{
    public string resourceType;
    public string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(description).AppendLine();
        builder.Append("Resource Type: ").Append(resourceType);
        
        return builder.ToString();
    }
}
