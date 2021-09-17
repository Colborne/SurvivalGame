using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public string interaction;
    public InventoryItem item;
    public int outItem;
    InputManager inputManager;
    InteractableUI ui; 

    private void Awake() {
        inputManager = FindObjectOfType<InputManager>();
        ui = FindObjectOfType<InteractableUI>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject == GameManager.Instance.PM.gameObject)
        {
            ui.interactableText.text = "Press 'E' to " + interaction;
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
        if (other.gameObject == GameManager.Instance.PM.gameObject && inputManager.interactInput)
        {   
            if(GameManager.Instance.CheckInventoryForItem(item, 1, true))
                GameManager.Instance.PickUpItem(outItem, 1);
            inputManager.interactInput = false;
            //ui.interactableText.text = null;
            //ui.transform.GetChild(0).gameObject.SetActive(false);
            return;
        }
    }
}
