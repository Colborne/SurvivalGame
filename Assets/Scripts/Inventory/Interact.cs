using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public string interactionText;
    public InventoryItem item;
    public int interactionType; //0 = nothing, 1 = crafting, 2 = alter
    public int outItem;
    InputManager inputManager;
    InteractableUI ui; 
    AnimatorManager animatorManager;
    CraftingSystem craftingSystem;

    private void Awake() {
        animatorManager = FindObjectOfType<AnimatorManager>();
        inputManager = FindObjectOfType<InputManager>();
        ui = FindObjectOfType<InteractableUI>();
        craftingSystem = FindObjectOfType<CraftingSystem>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject == GameManager.Instance.PM.gameObject)
        {
            ui.interactableText.text = "Press 'E' to " + interactionText;
            ui.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject == GameManager.Instance.PM.gameObject)
        {
            ui.interactableText.text = null;
            ui.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(item != null && GameManager.Instance.CheckAmount(item) > 0)
            ui.interactableText.text = "Press 'E' to " + interactionText + " x" + GameManager.Instance.CheckAmount(item);
            
        if (other.gameObject == GameManager.Instance.PM.gameObject && inputManager.interactInput)
        {   
            if(interactionType == 1)
            {
                craftingSystem.WindowActive();
            }
            else if(interactionType == 2)
            {
                int convert = GameManager.Instance.ReplaceStack(item);
                if(convert > 0)
                {
                    GameManager.Instance.PickUpItem(outItem, convert);
                    animatorManager.PlayTargetAnimation("Runecrafting", true);
                }
            }
            
            inputManager.interactInput = false;
            return;
        }
    }
}


