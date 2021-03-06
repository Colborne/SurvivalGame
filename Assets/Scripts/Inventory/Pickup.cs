using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pickup : MonoBehaviour
{
    public int itemID;
    public int amount;
    public string _name;
    InputManager inputManager;
    InteractableUI ui; 
    public bool autoPickup = false;
    public bool playerDropped = false;

    private void Awake() {
        inputManager = FindObjectOfType<InputManager>();
        ui = FindObjectOfType<InteractableUI>();
        _name = GetComponent<SaveableObject>().AssetPath.Split('/').Where(x => !string.IsNullOrWhiteSpace(x)).LastOrDefault();
        Invoke("ActivatePickup", 1f);
    }

    void ActivatePickup()
    {
        if(!playerDropped)
            autoPickup = true;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject == GameManager.Instance.PM.gameObject)
        {
            if(amount > 1)
                ui.interactableText.text = "Press 'E' to pickup " + _name + " x" + amount;
            else
                ui.interactableText.text = "Press 'E' to pickup " + _name;
 
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
