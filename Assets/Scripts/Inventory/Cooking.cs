using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    public Recipe[] recipes;
    public GameObject fx;
    public InventoryItem wood;
    public int woodCount;
    public bool cooking = false;
    InputManager inputManager;
    InteractableUI ui; 
    public int iter;    
    public int min;
    public float timer;
    public float cookTime;
    public bool load = false;
    private void Awake() {
        inputManager = FindObjectOfType<InputManager>();
        ui = FindObjectOfType<InteractableUI>();
        if(!load){
            iter = -1;
            min = 9999;
            timer = 0f;
            cookTime = 0f;
            cooking = false;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject == GameManager.Instance.PM.gameObject)
        {
            WoodCheck();
            if(woodCount > 0)
                RecipeCheck();              
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
        if (other.gameObject == GameManager.Instance.PM.gameObject)
        {
            if (woodCount > 0 && !cooking)
            {
                RecipeCheck();
                if(iter >= 0)
                {
                    if(inputManager.interactInput)
                    {
                        inputManager.interactInput = false;
                        GameManager.Instance.CheckInventoryForItem(recipes[iter].raw, 1, true);
                        cooking = true;
                    }
                }
                else
                    WoodCheck();
            }
            
            if(iter < 0 && woodCount < 10)
            {
                if(inputManager.interactInput)
                {
                    inputManager.interactInput = false;
                    GameManager.Instance.CheckInventoryForItem(wood, 1, true);
                    woodCount += 1;
                    
                }       
            }

        } 
    }

    void Update()
    {
        if(woodCount > 0)
        {
            if(timer <= 60f)
            {
                timer += Time.deltaTime;
                fx.SetActive(true);
            }
            else
            {
                timer = 0;
                woodCount--;
            }

            if(cooking)
            {
                transform.GetChild(iter + 1).gameObject.SetActive(true);
                if(cookTime <= 10f)
                    cookTime += Time.deltaTime;          
                else
                {
                    cookTime = 0;
                    Instantiate(recipes[iter].cooked, transform.position + transform.up * 3, Quaternion.identity);
                    transform.GetChild(iter + 1).gameObject.SetActive(false);
                    iter = -1;
                    cooking = false;               
                }
            }
        }
        else
            fx.SetActive(false);    
    }

    public void WoodCheck()
    {
        if(woodCount < 10)
        {
            ui.interactableText.text = "Press 'E' to Add Wood (" + woodCount + "/10)";
            ui.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            ui.interactableText.text = null;
            ui.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void RecipeCheck()
    {
        for(int i = 0; i < recipes.Length; i++) //Check through the recipes
        {
            if(GameManager.Instance.CheckAmount(recipes[i].raw) >= 1) //Check the amount for each item in that recipe
                iter = i;
            else
                iter = -1;
 
            if(iter >= 0)
            {
                ui.interactableText.text = "Press 'E' to Cook " + recipes[i].raw.item.itemName;
                ui.transform.GetChild(0).gameObject.SetActive(true);
                break;
            }
            else
            {
                ui.interactableText.text = null;
                ui.transform.GetChild(0).gameObject.SetActive(false);   
            }
        }
    }
}