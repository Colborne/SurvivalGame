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

    private void  Update() 
    {
        if(clicked && GameManager.Instance.CraftingCheck(GetComponent<CraftingRecipe>().items, GetComponent<CraftingRecipe>().amountRequired))
            timer += 1f * Time.deltaTime;
        
        fill.fillAmount = timer / craftingTime;
    } 
}
