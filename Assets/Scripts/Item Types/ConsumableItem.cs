using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(menuName = "Items/Consumable Item")]
public class ConsumableItem : Item
{
    public string use;
    public string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(description).AppendLine();
        builder.Append("Use to: ").Append(use);
        
        return builder.ToString();
    }
}
