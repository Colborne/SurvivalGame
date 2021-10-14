using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int itemID;
    public int amount;
    public string name;
    InputManager inputManager;
    InteractableUI ui; 
    bool autoPickup = false;

    private void Awake() {
        inputManager = FindObjectOfType<InputManager>();
        ui = FindObjectOfType<InteractableUI>();
        Invoke("ActivatePickup", 1f);
    }

    void ActivatePickup()
    {
        autoPickup = true;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject == GameManager.Instance.PM.gameObject)
        {
            if(amount > 1)
                ui.interactableText.text = "Press 'E' to pickup " + name + " x" + amount;
            else
                ui.interactableText.text = "Press 'E' to pickup " + name;
 
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
        if (other.gameObject == GameManager.Instance.PM.gameObject) 
        {
            if(inputManager.interactInput || autoPickup)
            {   
                inputManager.interactInput = false;
                GameManager.Instance.PickUpItem(itemID, amount);
                Destroy(gameObject);
                ui.interactableText.text = null;
                ui.transform.GetChild(0).gameObject.SetActive(false);
                return;
            }
        }
    }
}
