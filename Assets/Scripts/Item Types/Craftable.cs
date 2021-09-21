using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text;

public class Craftable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public CraftingSystem craftingSystem;
    public CraftingRecipe recipe;
    public int iteration;
    public InventoryItem item;
    public int amount;
    public float timer = 0f;
    public float craftingTime;
    bool clicked;
    public Image fill;
    public TooltipTrigger tooltipTrigger;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            clicked = true;
            craftingSystem.iteration = iteration;
        }  
    }    

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            timer = 0f;
            clicked = false;
        }  
    }     

    private void Start()
    {
        if(amount > 1)
            tooltipTrigger.header = item.item.itemName + " x" + amount.ToString();
        else
            tooltipTrigger.header = item.item.itemName;
        StringBuilder builder = new StringBuilder();
        for(int i = 0; i< recipe.items.Length; i++)
        {
            builder.Append(recipe.items[i].item.itemName);
            builder.Append(" x");
            builder.Append(recipe.amountRequired[i].ToString()).AppendLine();
        }
        tooltipTrigger.content = builder.ToString();
    }
    private void Update() 
    {
        if(clicked && GameManager.Instance.CraftingCheck(GetComponent<CraftingRecipe>().items, GetComponent<CraftingRecipe>().amountRequired))
            timer += 1f * Time.deltaTime;
        
        fill.fillAmount = timer / craftingTime;
    } 
}
