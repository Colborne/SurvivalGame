using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public GameObject chestWindow;

    [SerializeField] public List<GameObject> chests;

    private void Awake() 
    {
        chests = new List<GameObject>();
    }

    public void WindowActive(int iter)
    {
        FindObjectOfType<InputManager>().inventoryFlag = !FindObjectOfType<InputManager>().inventoryFlag;
        chests[iter].SetActive(!chests[iter].active);
        FindObjectOfType<InputManager>().InventoryWindow.SetActive(!FindObjectOfType<InputManager>().InventoryWindow.active);
        FindObjectOfType<InputManager>().TooltipCanvas.SetActive(!FindObjectOfType<InputManager>().TooltipCanvas.active);
    }
}
