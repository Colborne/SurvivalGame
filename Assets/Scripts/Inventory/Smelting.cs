using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelting : MonoBehaviour
{
    public SmeltingRecipe[] recipes;
    public string interactionText;
    InputManager inputManager;
    InteractableUI ui; 
    AnimatorManager animatorManager;
    int iter = -1;
    int min = 9999;
    private void Awake() {
        animatorManager = FindObjectOfType<AnimatorManager>();
        inputManager = FindObjectOfType<InputManager>();
        ui = FindObjectOfType<InteractableUI>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject == GameManager.Instance.PM.gameObject)
        {
            for(int i = 0; i < recipes.Length; i++) //Check through the recipes
            {
                int minAmount = 9999;
                for(int j = 0; j < recipes[i].items.Length; j++) //Check the first recipe items
                {
                    if(GameManager.Instance.CheckAmount(recipes[i].items[j]) >= 1) //Check the amount for each item in that recipe
                    {
                        if(GameManager.Instance.CheckAmount(recipes[i].items[j]) < minAmount)
                            minAmount = GameManager.Instance.CheckAmount(recipes[i].items[j]);
                    }
                    else
                    {
                        iter = -1;
                        minAmount = 9999;
                        break;
                    }
                }
                
                if(minAmount < 9999)
                {
                    ui.interactableText.text = "Press 'E' to " + recipes[i].output.GetComponent<Pickup>().name + " x" + minAmount;
                    ui.transform.GetChild(0).gameObject.SetActive(true);
                    if(iter < 0)
                        iter = i;
                    min = minAmount;
                    break;
                }
            }
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
        if (other.gameObject == GameManager.Instance.PM.gameObject && inputManager.interactInput && iter >= 0)
        {
            for(int i = 0; i < recipes[iter].items.Length; i++)
                GameManager.Instance.CheckInventoryForItem(recipes[iter].items[i], min, true);

            GameObject smelt = Instantiate(recipes[iter].output, transform.position + transform.forward * 3, Quaternion.identity);
            smelt.GetComponent<Pickup>().amount = min;
            iter = -1;
            min = 9999;
            ui.interactableText.text = null;
            ui.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}

                    