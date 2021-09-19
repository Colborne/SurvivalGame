using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] GameObject CraftingWindow;
    [SerializeField] Craftable[] Craftables;
    public int iteration = 0;

    private void Update() 
    {
        if (Craftables[iteration].timer > Craftables[iteration].craftingTime && 
            GameManager.Instance.CraftingCheck(Craftables[iteration].GetComponent<CraftingRecipe>().items, Craftables[iteration].GetComponent<CraftingRecipe>().amountRequired))
        {
            GameManager.Instance.Craft(Craftables[iteration].GetComponent<CraftingRecipe>().items, Craftables[iteration].GetComponent<CraftingRecipe>().amountRequired);
            GameManager.Instance.PickUpItem(Craftables[iteration].item.itemID, Craftables[iteration].amount);
            Craftables[iteration].timer = 0f;
            
        }     
    }
}