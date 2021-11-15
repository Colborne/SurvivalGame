using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ChestWindowManager : MonoBehaviour
{

    [System.Serializable]
	public class SaveItem {
        public int itemID;
        public int currentAmount;
        public int slot;
	}

    public SaveItem[] chest;

    public int iter;
    public GameObject window;
    public ChestManager cm;
    public bool load = false;

    private void Awake() 
    {
        cm = FindObjectOfType<ChestManager>();
        if(!load)
            iter = cm.chests.Count; 

        window = Instantiate(cm.chestWindow);
        window.transform.SetParent(GameObject.Find("Player UI").transform, false);
        cm.chests.Add(window);
        window.SetActive(false);
    }

    public void Save() 
    {
        chest = new SaveItem[16];

        for(int i = 0; i < chest.Length; i++)
            chest[i] = new SaveItem(); 

        for(int i = 0; i < 16; i++)
        {
            if(window.transform.GetChild(0).GetChild(i).GetComponent<InventorySlot>().currentItem != null)
            {
                chest[i].slot = i;
                chest[i].itemID = window.transform.GetChild(0).GetChild(i).GetComponent<InventorySlot>().currentItem.itemID;
                chest[i].currentAmount = window.transform.GetChild(0).GetChild(i).GetComponent<InventorySlot>().currentItem.currentAmount;
            }
        }
    }

    public void LoadInventoryItem(int slot, int itemID, int amount)
    {
        GameObject GO = Instantiate(GameManager.Instance.equipment[itemID].inventoryItem, window.transform.GetChild(0).GetChild(slot).gameObject.transform);
        GO.GetComponent<InventoryItem>().originalSlot =  window.transform.GetChild(0).GetChild(slot).gameObject.transform;
        window.transform.GetChild(0).GetChild(slot).GetComponent<InventorySlot>().currentItem = GO.GetComponent<InventoryItem>();
        window.transform.GetChild(0).GetChild(slot).GetComponent<InventorySlot>().isFull = true;
        window.transform.GetChild(0).GetChild(slot).GetComponent<InventorySlot>().currentItem.currentAmount = amount;

        window.transform.GetChild(0).GetChild(slot).GetComponent<InventorySlot>().GetComponent<TooltipTrigger>().header =  window.transform.GetChild(0).GetChild(slot).GetComponent<InventorySlot>().currentItem.item.itemName;
        window.transform.GetChild(0).GetChild(slot).GetComponent<InventorySlot>().GetComponent<TooltipTrigger>().content =  window.transform.GetChild(0).GetChild(slot).GetComponent<InventorySlot>().currentItem.item.GetTooltipInfoText();
    }
}