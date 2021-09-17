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
        if(GameManager.Instance.CheckAmount(item) > 0)
            ui.interactableText.text = "Press 'E' to " + interaction + " x" + GameManager.Instance.CheckAmount(item);
            
        if (other.gameObject == GameManager.Instance.PM.gameObject && inputManager.interactInput)
        {   
            int convert = GameManager.Instance.ReplaceStack(item);

            if(convert > 0)
                GameManager.Instance.PickUpItem(outItem, convert);
  
            inputManager.interactInput = false;
            return;
        }
    }
}
