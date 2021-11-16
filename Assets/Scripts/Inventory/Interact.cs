using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
        public enum Type {
            None,
            Crafting,
            Runecrafting,
            Smithing,
            Smelting,
            Alchemy,
            Sharpening,
            Pickup,
            Chest
    }

    [SerializeField] private Type interactionType;
    public string interactionText;
    public InventoryItem item; //Make this an array for smelting/Sharpening  (Also requires an array of amounts of each) or can add however many coal/ore is allowed then make furnace do the rest
    public int outItem; //Make this an array to match ^ 
    [Range(1,3)]
	public int level;
    InputManager inputManager;
    InteractableUI ui; 
    AnimatorManager animatorManager;
    CraftingSystem craftingSystem; //multiple crafting systems based on level? Visibility based on level? All are on, only show level available
    CraftingSystem smithingSystem;
    CraftingSystem crafting2System;
    CraftingSystem smithing2System;
    CraftingSystem crafting3System;
    CraftingSystem smithing3System;
    CraftingSystem alchemySystem;
    CraftingSystem alchemy2System;
    ChestManager chestManager;
    CraftingSystem temp;

    private void Awake() 
    {
        animatorManager = FindObjectOfType<AnimatorManager>();
        inputManager = FindObjectOfType<InputManager>();
        ui = FindObjectOfType<InteractableUI>();
        craftingSystem = GameObject.Find("CraftingSystem").GetComponent<CraftingSystem>();
        smithingSystem = GameObject.Find("SmithingSystem").GetComponent<CraftingSystem>();
        alchemySystem = GameObject.Find("AlchemySystem").GetComponent<CraftingSystem>();
        crafting2System = GameObject.Find("Crafting2System").GetComponent<CraftingSystem>();
        smithing2System = GameObject.Find("Smithing2System").GetComponent<CraftingSystem>();
        alchemy2System = GameObject.Find("Alchemy2System").GetComponent<CraftingSystem>();
        crafting3System = GameObject.Find("Crafting3System").GetComponent<CraftingSystem>();
        smithing3System = GameObject.Find("Smithing3System").GetComponent<CraftingSystem>();
        chestManager = GameObject.Find("ChestManager").GetComponent<ChestManager>();
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
            
        if (other.gameObject == GameManager.Instance.PM.gameObject && (inputManager.interactInput)) // || (inputManager.inventoryInput && temp.CraftingWindow.active)))
        {   
            if(interactionType == Type.Crafting)
            {
                if(level == 1)
                    craftingSystem.WindowActive();
                else if(level == 2)
                    crafting2System.WindowActive();
                else if(level == 3)
                    crafting3System.WindowActive();
            }
            else if(interactionType == Type.Smithing)
            {
                if(level == 1)
                    smithingSystem.WindowActive();
                else if(level == 2)
                    smithing2System.WindowActive();
                else if(level == 3)
                    smithing3System.WindowActive();
            }
            else if(interactionType == Type.Alchemy)
            {
                if(level == 1)
                    alchemySystem.WindowActive();
                else if(level == 2)
                    alchemy2System.WindowActive();
            }
            else if(interactionType == Type.Runecrafting)
            {
                int convert = GameManager.Instance.ReplaceStack(item);
                if(convert > 0)
                {
                    GameManager.Instance.PickUpItem(outItem, convert);
                    animatorManager.PlayTargetAnimation("Runecrafting", true);
                }
            }
            else if(interactionType == Type.Sharpening)
            {
                int convert = GameManager.Instance.ReplaceStack(item);
                if(convert > 0)
                {
                    GameManager.Instance.PickUpItem(outItem, convert);
                    animatorManager.PlayTargetAnimation("Landing", true);
                }
            }
            else if(interactionType == Type.Pickup)
            {
                var _drop = Instantiate(GameManager.Instance.equipment[outItem].worldItem, transform.position + new Vector3(Random.Range(-.5f,.5f),Random.Range(.5f,1f),Random.Range(-.5f,.5f)), Random.rotation);
                _drop.GetComponentInChildren<Rigidbody>().AddForce(transform.up * 20f);
                animatorManager.PlayTargetAnimation("Landing", true);
                Destroy(gameObject);
            }
            else if(interactionType == Type.Chest)
            {
                chestManager.WindowActive(GetComponent<ChestWindowManager>().window);
            }
            
            inputManager.interactInput = false;
            return;
        }
    }
}