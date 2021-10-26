using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    public Recipe[] recipes;
    public GameObject fx;
    public InventoryItem coal;
    public int coalCount;
    public bool cooking = false;
    InputManager inputManager;
    InteractableUI ui; 
    int iter = -1;
    int min = 9999;
    public float timer = 0f;
    public float cookTime = 0f;
    private void Awake() {
        inputManager = FindObjectOfType<InputManager>();
        ui = FindObjectOfType<InteractableUI>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject == GameManager.Instance.PM.gameObject)
        {
            CoalCheck();
            if(coalCount > 0)
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
            if (coalCount > 0 && !cooking)
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
                    CoalCheck();
            }
            
            if(iter < 0 && coalCount < 10)
            {
                if(inputManager.interactInput)
                {
                    inputManager.interactInput = false;
                    GameManager.Instance.CheckInventoryForItem(coal, min, true);
                    coalCount += min;
                }       
            }

        } 
    }

    void Update()
    {
        if(coalCount > 0)
        {
            if(timer <= 60f)
            {
                timer += Time.deltaTime;
                fx.SetActive(true);
            }
            else
            {
                timer = 0;
                coalCount--;
            }

            if(cooking)
            {
                if(cookTime <= 10f)
                    cookTime += Time.deltaTime;          
                else
                {
                    cookTime = 0;
                    Instantiate(recipes[iter].cooked, transform.position + transform.up * 3, Quaternion.identity);
                    iter = -1;
                    cooking = false;
                }
            }
        }
        else
            fx.SetActive(false);    
    }

    public void CoalCheck()
    {
        //Coal Check
        int minAmount = 9999;
        if(GameManager.Instance.CheckAmount(coal) >= 1) //Check the amount for each item in that recipe
            minAmount = GameManager.Instance.CheckAmount(coal);
        else
            minAmount = 9999;
             
        if(minAmount < 9999)
        {
            if(minAmount > 10)
                minAmount = 10;
            else if(minAmount < 0)
                minAmount = 0;

            if(10 - coalCount < minAmount)
                minAmount = 10 - coalCount;
                
            ui.interactableText.text = "Press 'E' to Add Coal x" + minAmount;
            ui.transform.GetChild(0).gameObject.SetActive(true);
            min = minAmount;
        }
        else
        {
            ui.interactableText.text = null;
            ui.transform.GetChild(0).gameObject.SetActive(false);
            min = 0;
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
                ui.interactableText.text = "Press 'E' to Add " + recipes[i].raw.item.itemName;
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