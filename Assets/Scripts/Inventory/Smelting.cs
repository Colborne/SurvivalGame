using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelting : MonoBehaviour
{
    public SmeltingRecipe[] recipes;
    public GameObject fx;

    [SerializeField]     
    Queue<GameObject> smelt = new Queue<GameObject>();
    InputManager inputManager;
    InteractableUI ui; 
    AnimatorManager animatorManager;
    int iter = -1;
    int min = 9999;
    float timer = 0f;
    private void Awake() {
        animatorManager = FindObjectOfType<AnimatorManager>();
        inputManager = FindObjectOfType<InputManager>();
        ui = FindObjectOfType<InteractableUI>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject == GameManager.Instance.PM.gameObject)
        {
            inventoryCheck();
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
            if(min > 10)
                min = 10;
            else 
                min -= smelt.Count;

            for(int i = 0; i < recipes[iter].items.Length; i++)
                GameManager.Instance.CheckInventoryForItem(recipes[iter].items[i], min, true);
            
            for(int i = 0; i < min; i++)
                smelt.Enqueue(recipes[iter].output);

            iter = -1;
            min = 9999;
            inventoryCheck();
        }
    }

    void Update()
    {
        if(smelt.Count > 0)
        {
            if(timer <= 15f)
            {
                timer += Time.deltaTime;
                fx.SetActive(true);
            }
            else
            {
                timer = 0;
                Instantiate(smelt.Peek(), transform.position + transform.right * 3, Quaternion.identity);
                smelt.Dequeue();
            }
        }
        else
        {
            fx.SetActive(false);
        }
    }

    public void inventoryCheck()
    {
        for(int i = 0; i < recipes.Length; i++) //Check through the recipes
        {
            int minAmount = 9999;
            for(int j = 0; j < recipes[i].items.Length; j++) //Check the first recipe items
            {
                if(GameManager.Instance.CheckAmount(recipes[i].items[j]) >= 1) //Check the amount for each item in that recipe
                {
                    if(GameManager.Instance.CheckAmount(recipes[i].items[j]) < minAmount)
                        minAmount = GameManager.Instance.CheckAmount(recipes[i].items[j]) - smelt.Count;
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
                ui.interactableText.text = "Press 'E' to Smelt " + recipes[i].output.GetComponent<Pickup>().name + " x" + minAmount;
                ui.transform.GetChild(0).gameObject.SetActive(true);
                if(iter < 0)
                    iter = i;
                min = minAmount;
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