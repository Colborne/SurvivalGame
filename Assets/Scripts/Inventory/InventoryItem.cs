using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    public enum equipment
    {
        None,
        Helmet,
        Chest,
        Legs,
        Boots,
        Weapon,
        Shield,
        Back,
        Accessory
    }

    RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    public Transform originalSlot;
    InputManager inputManager;
    StatsManager stats;

    public bool inHelmetSlot = false;
    public bool inWeaponSlot = false;
    public bool inChestSlot = false;
    public bool inLegsSlot = false;
    public bool inBootsSlot = false;
    public bool inBackSlot = false;
    public bool inShieldSlot = false;
    public bool inAccessory1Slot = false;
    public bool inAccessory2Slot = false;
    public bool inAccessory3Slot = false;
    public int itemID;
    public equipment equipType;
    [Range(1,100)]
    public int MaxAmount = 1;
    public int currentAmount = 1;
    public Text textAmount;
    public Item item;
    public float totalWeight = 0;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        textAmount = GetComponentInChildren<Text>();
        inputManager = FindObjectOfType<InputManager>();
        stats = FindObjectOfType<StatsManager>();
    }

    public void Update() 
    {
        totalWeight = item.weight * currentAmount;
        if(textAmount != null)
        {
            if(currentAmount > 1)
                textAmount.text = currentAmount.ToString();
            else
                textAmount.text = "";
        }
    }

    public void WeightCalculation() 
    {
        totalWeight = item.weight * currentAmount;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GameManager.Instance.interfaceCanvas.scaleFactor;
        gameObject.transform.SetParent(GameManager.Instance.draggables);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(currentAmount > 1 && inputManager.modifierInput)
        {
            InventoryItem newItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            InventorySlot OriginalSlot = newItem.originalSlot.GetComponent<InventorySlot>();
            InventoryItem _item  = Instantiate(newItem, transform.position, transform.rotation);
            
            _item.currentAmount = (int)Mathf.Floor(currentAmount/2);
            _item.transform.SetParent(newItem.originalSlot);
            OriginalSlot.currentItem = _item;
            OriginalSlot.currentItem.rectTransform.localScale = new Vector3(1f, 1f, 1f);

            currentAmount -= _item.currentAmount;
        }
 
        canvasGroup.blocksRaycasts = false;
        rectTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        originalSlot = transform.parent.transform;  
    } 
    public void OnEndDrag(PointerEventData eventData)
    {
        // Revert State //
        canvasGroup.blocksRaycasts = true;
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
        if (transform.parent == GameManager.Instance.draggables)
        {
            if(originalSlot.GetComponentInChildren<InventoryItem>() != null)
            {   
                int amount = originalSlot.GetComponentInChildren<InventoryItem>().currentAmount;
                Debug.Log("Adding " + amount + " to " + currentAmount);
                currentAmount += amount;
                Destroy(originalSlot.GetComponentInChildren<InventoryItem>().gameObject);    
            }
            transform.SetParent(originalSlot);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !eventData.dragging)
        {
            InventorySlot currentSlot = transform.parent.GetComponent<InventorySlot>();

            // If this item is in the weapon slot
            if (currentSlot.weaponSlot)
            {
                for (int i = 10; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;
                        currentSlot.GetComponent<TooltipTrigger>().header = null;
                        currentSlot.GetComponent<TooltipTrigger>().content = null;

                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;                    
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().header = this.item.itemName;
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();

                        // Changing 
                        inWeaponSlot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedWeapon);
                        GameManager.Instance.weaponID = -1;
                        break;
                    }
                }
            }
            // If this item is in the helmet slot
            else if (currentSlot.helmetSlot)
            {
                for (int i = 10; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;
                        currentSlot.GetComponent<TooltipTrigger>().header = null;
                        currentSlot.GetComponent<TooltipTrigger>().content = null;

                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;                    
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().header = this.item.itemName;
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();

                        // Changing 
                        inHelmetSlot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedHelmet);
                        GameManager.Instance.helmetID = -1;
                        break;
                    }
                }
            }
            // If this item is in the chest slot
            else if (currentSlot.chestSlot)
            {
                for (int i = 10; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;
                        currentSlot.GetComponent<TooltipTrigger>().header = null;
                        currentSlot.GetComponent<TooltipTrigger>().content = null;

                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;                    
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().header = this.item.itemName;
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();

                        // Changing 
                        inChestSlot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedChest);
                        GameManager.Instance.chestID = -1;
                        break;
                    }
                }
            }
            // If this item is in the legs slot
            else if (currentSlot.legsSlot)
            {
                for (int i = 10; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;
                        currentSlot.GetComponent<TooltipTrigger>().header = null;
                        currentSlot.GetComponent<TooltipTrigger>().content = null;

                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;                    
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().header = this.item.itemName;
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();

                        // Changing 
                        inLegsSlot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedLegs);
                        GameManager.Instance.legsID = -1;
                        break;
                    }
                }
            }
            // If this item is in the boots slot
            else if (currentSlot.bootsSlot)
            {
                for (int i = 10; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;
                        currentSlot.GetComponent<TooltipTrigger>().header = null;
                        currentSlot.GetComponent<TooltipTrigger>().content = null;
                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;                    
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().header = this.item.itemName;
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();

                        // Changing 
                        inBootsSlot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedRightBoot);
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedLeftBoot);
                        GameManager.Instance.bootsID = -1;
                        break;
                    }
                }
            }
            // If this item is in the back slot
            else if (currentSlot.backSlot)
            {
                for (int i = 10; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;
                        currentSlot.GetComponent<TooltipTrigger>().header = null;
                        currentSlot.GetComponent<TooltipTrigger>().content = null;
                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;                    
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().header = this.item.itemName;
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();

                        // Changing 
                        inBackSlot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedBack);
                        GameManager.Instance.backID = -1;
                        break;
                    }
                }
            }
            // If this item is in the shield slot
            else if (currentSlot.shieldSlot)
            {
                for (int i = 10; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;
                        currentSlot.GetComponent<TooltipTrigger>().header = null;
                        currentSlot.GetComponent<TooltipTrigger>().content = null;
                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;                    
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().header = this.item.itemName;
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();

                        // Changing 
                        inShieldSlot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedShield);
                        GameManager.Instance.shieldID = -1;
                        break;
                    }
                }
            }
            // If this item is in the accessory1 slot
            else if (currentSlot.accessory1Slot)
            {
                for (int i = 10; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;
                        currentSlot.GetComponent<TooltipTrigger>().header = null;
                        currentSlot.GetComponent<TooltipTrigger>().content = null;
                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;                    
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().header = this.item.itemName;
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();

                        // Changing 
                        inAccessory1Slot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedAccessory1);
                        GameManager.Instance.accessory1ID = -1;
                        break;
                    }
                }
            }
            // If this item is in the accessory2 slot
            else if (currentSlot.accessory2Slot)
            {
                for (int i = 10; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;
                        currentSlot.GetComponent<TooltipTrigger>().header = null;
                        currentSlot.GetComponent<TooltipTrigger>().content = null;
                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;                    
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().header = this.item.itemName;
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();

                        // Changing 
                        inAccessory2Slot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedAccessory2);
                        GameManager.Instance.accessory2ID = -1;
                        break;
                    }
                }
            }
            // If this item is in the accessory 3 slot
            else if (currentSlot.accessory3Slot)
            {
                for (int i = 10; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;
                        currentSlot.GetComponent<TooltipTrigger>().header = null;
                        currentSlot.GetComponent<TooltipTrigger>().content = null;
                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;                    
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().header = this.item.itemName;
                        GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();

                        // Changing 
                        inAccessory3Slot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyItem(GameManager.Instance.spawnedAccessory3);
                        GameManager.Instance.accessory3ID = -1;
                        break;
                    }
                }
            }
            else if (!currentSlot.weaponSlot && equipType == equipment.Weapon)
            {            
                if(currentSlot.currentItem.item is WeaponItem  && (currentSlot.currentItem.item as WeaponItem).isTwoHanded && GameManager.Instance.inventorySlots[4].isFull) 
                {
                    if(GameManager.Instance.CheckIfEmpty())
                    {
                        for (int i = 10; i < GameManager.Instance.inventorySlots.Length; i++)
                        {
                            Debug.Log("Searching for Slot...");
                            if (GameManager.Instance.inventorySlots[i].isFull == false)
                            {
                                Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                                // Occupying New Slot
                                GameManager.Instance.inventorySlots[i].currentItem = GameManager.Instance.inventorySlots[4].currentItem;
                                GameManager.Instance.inventorySlots[i].isFull = true;                    
                                GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().header = GameManager.Instance.inventorySlots[4].currentItem.item.itemName;
                                GameManager.Instance.inventorySlots[i].GetComponent<TooltipTrigger>().content = GameManager.Instance.inventorySlots[4].currentItem.item.GetTooltipInfoText();

                                // Changing 
                                inShieldSlot = false;
                                GameManager.Instance.inventorySlots[4].currentItem.transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                                GameManager.Instance.DestroyItem(GameManager.Instance.spawnedShield);

                                // Emptying Previous Slot
                                GameManager.Instance.inventorySlots[4].isFull = false;
                                GameManager.Instance.inventorySlots[4].currentItem = null;
                                GameManager.Instance.inventorySlots[4].GetComponent<TooltipTrigger>().header = null;
                                GameManager.Instance.inventorySlots[4].GetComponent<TooltipTrigger>().content = null;
                                break;
                            }
                        }
                    }
                    else
                        return;       
                }
                if (GameManager.Instance.inventorySlots[3].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[3].currentItem;
                    currentSlot.currentItem.inWeaponSlot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);
                    currentSlot.GetComponent<TooltipTrigger>().header = currentSlot.currentItem.item.itemName;
                    currentSlot.GetComponent<TooltipTrigger>().content = currentSlot.currentItem.item.GetTooltipInfoText();

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[3].currentItem = this;
                    GameManager.Instance.inventorySlots[3].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[3].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inWeaponSlot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[3].transform);
                    GameManager.Instance.weaponID = itemID;
                    GameManager.Instance.SpawnItem("Weapon", GameManager.Instance.spawnedWeapon);
                } 
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;
                    currentSlot.GetComponent<TooltipTrigger>().header = null;
                    currentSlot.GetComponent<TooltipTrigger>().content = null;
                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[3].currentItem = this;
                    GameManager.Instance.inventorySlots[3].isFull = true;
                    GameManager.Instance.inventorySlots[3].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[3].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inWeaponSlot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[3].transform);
                    GameManager.Instance.weaponID = itemID;
                    GameManager.Instance.SpawnItem("Weapon", GameManager.Instance.spawnedWeapon);
                }
            }
            else if (!currentSlot.helmetSlot && equipType == equipment.Helmet)
            {
                if (GameManager.Instance.inventorySlots[1].isFull) //ALL OF THESE NEED TO CHANGE TO THE CORRECT NUMBER
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[1].currentItem;
                    currentSlot.currentItem.inHelmetSlot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);
                    currentSlot.GetComponent<TooltipTrigger>().header = currentSlot.currentItem.item.itemName;
                    currentSlot.GetComponent<TooltipTrigger>().content = currentSlot.currentItem.item.GetTooltipInfoText();

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[1].currentItem = this;
                    GameManager.Instance.inventorySlots[1].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[1].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inHelmetSlot = true;
                    transform.SetParent(GameManager.Instance.inventorySlots[1].transform);
                    GameManager.Instance.helmetID = itemID;
                    GameManager.Instance.SpawnItem("Helmet", GameManager.Instance.spawnedHelmet);
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;
                    currentSlot.GetComponent<TooltipTrigger>().header = null;
                    currentSlot.GetComponent<TooltipTrigger>().content = null;
                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[1].currentItem = this;
                    GameManager.Instance.inventorySlots[1].isFull = true;
                    GameManager.Instance.inventorySlots[1].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[1].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inHelmetSlot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[1].transform);
                    GameManager.Instance.helmetID = itemID;
                    GameManager.Instance.SpawnItem("Helmet", GameManager.Instance.spawnedHelmet);
                }
            }
            else if (!currentSlot.chestSlot && equipType == equipment.Chest)
            {
                if (GameManager.Instance.inventorySlots[2].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[2].currentItem;
                    currentSlot.currentItem.inChestSlot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);

                    currentSlot.GetComponent<TooltipTrigger>().header = currentSlot.currentItem.item.itemName;
                    currentSlot.GetComponent<TooltipTrigger>().content = currentSlot.currentItem.item.GetTooltipInfoText();

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[2].currentItem = this;
                    GameManager.Instance.inventorySlots[2].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[2].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inChestSlot = true;
                    transform.SetParent(GameManager.Instance.inventorySlots[2].transform);
                    GameManager.Instance.chestID = itemID;
                    GameManager.Instance.SpawnItem("Chest", GameManager.Instance.spawnedChest);
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;
                    currentSlot.GetComponent<TooltipTrigger>().header = null;
                    currentSlot.GetComponent<TooltipTrigger>().content = null;
                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[2].currentItem = this;
                    GameManager.Instance.inventorySlots[2].isFull = true;
                    GameManager.Instance.inventorySlots[2].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[2].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inChestSlot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[2].transform);
                    GameManager.Instance.chestID = itemID;
                    GameManager.Instance.SpawnItem("Chest", GameManager.Instance.spawnedChest);
                }
            }
            else if (!currentSlot.legsSlot && equipType == equipment.Legs)
            {
                if (GameManager.Instance.inventorySlots[5].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[5].currentItem;
                    currentSlot.currentItem.inLegsSlot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);
                    currentSlot.GetComponent<TooltipTrigger>().header = currentSlot.currentItem.item.itemName;
                    currentSlot.GetComponent<TooltipTrigger>().content = currentSlot.currentItem.item.GetTooltipInfoText();

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[5].currentItem = this;
                    GameManager.Instance.inventorySlots[5].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[5].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inLegsSlot = true;
                    transform.SetParent(GameManager.Instance.inventorySlots[5].transform);
                    GameManager.Instance.legsID = itemID;
                    GameManager.Instance.SpawnItem("Legs", GameManager.Instance.spawnedLegs);
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;
                    currentSlot.GetComponent<TooltipTrigger>().header = null;
                    currentSlot.GetComponent<TooltipTrigger>().content = null;
                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[5].currentItem = this;
                    GameManager.Instance.inventorySlots[5].isFull = true;
                    GameManager.Instance.inventorySlots[5].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[5].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inLegsSlot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[5].transform);
                    GameManager.Instance.legsID = itemID;
                    GameManager.Instance.SpawnItem("Legs", GameManager.Instance.spawnedLegs);
                }
            }
            else if (!currentSlot.bootsSlot && equipType == equipment.Boots)
            {
                if (GameManager.Instance.inventorySlots[6].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[6].currentItem;
                    currentSlot.currentItem.inBootsSlot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);

                    currentSlot.GetComponent<TooltipTrigger>().header = currentSlot.currentItem.item.itemName;
                    currentSlot.GetComponent<TooltipTrigger>().content = currentSlot.currentItem.item.GetTooltipInfoText();

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[6].currentItem = this;
                    GameManager.Instance.inventorySlots[6].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[6].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inBootsSlot = true;
                    transform.SetParent(GameManager.Instance.inventorySlots[6].transform);
                    GameManager.Instance.bootsID = itemID;
                    GameManager.Instance.SpawnItem("Boots", GameManager.Instance.spawnedRightBoot);
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;
                    currentSlot.GetComponent<TooltipTrigger>().header = null;
                    currentSlot.GetComponent<TooltipTrigger>().content = null;
                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[6].currentItem = this;
                    GameManager.Instance.inventorySlots[6].isFull = true;
                    GameManager.Instance.inventorySlots[6].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[6].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inBootsSlot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[6].transform);
                    GameManager.Instance.bootsID = itemID;
                    GameManager.Instance.SpawnItem("Boots", GameManager.Instance.spawnedRightBoot);
                }
            }
            else if (!currentSlot.backSlot && equipType == equipment.Back)
            {
                if (GameManager.Instance.inventorySlots[0].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[0].currentItem;
                    currentSlot.currentItem.inBackSlot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);
                    currentSlot.GetComponent<TooltipTrigger>().header = currentSlot.currentItem.item.itemName;
                    currentSlot.GetComponent<TooltipTrigger>().content = currentSlot.currentItem.item.GetTooltipInfoText();

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[0].currentItem = this;
                    GameManager.Instance.inventorySlots[0].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[0].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inBackSlot = true;
                    transform.SetParent(GameManager.Instance.inventorySlots[0].transform);
                    GameManager.Instance.backID = itemID;
                    GameManager.Instance.SpawnItem("Back", GameManager.Instance.spawnedBack);
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;
                    currentSlot.GetComponent<TooltipTrigger>().header = null;
                    currentSlot.GetComponent<TooltipTrigger>().content = null;
                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[0].currentItem = this;
                    GameManager.Instance.inventorySlots[0].isFull = true;
                    GameManager.Instance.inventorySlots[0].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[0].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inBackSlot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[0].transform);
                    GameManager.Instance.backID = itemID;
                    GameManager.Instance.SpawnItem("Back", GameManager.Instance.spawnedBack);
                }
            }
            else if (!currentSlot.shieldSlot && equipType == equipment.Shield) 
            {
                if(GameManager.Instance.inventorySlots[3].isFull && (GameManager.Instance.inventorySlots[3].currentItem.item as WeaponItem).isTwoHanded)
                    return;
                if (GameManager.Instance.inventorySlots[4].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[4].currentItem;
                    currentSlot.currentItem.inShieldSlot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);

                    currentSlot.GetComponent<TooltipTrigger>().header = currentSlot.currentItem.item.itemName;
                    currentSlot.GetComponent<TooltipTrigger>().content = currentSlot.currentItem.item.GetTooltipInfoText();

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[4].currentItem = this;
                    GameManager.Instance.inventorySlots[4].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[4].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inShieldSlot = true;
                    transform.SetParent(GameManager.Instance.inventorySlots[4].transform);
                    GameManager.Instance.shieldID = itemID;
                    GameManager.Instance.SpawnItem("Shield", GameManager.Instance.spawnedShield);
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;
                    currentSlot.GetComponent<TooltipTrigger>().header = null;
                    currentSlot.GetComponent<TooltipTrigger>().content = null;
                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[4].currentItem = this;
                    GameManager.Instance.inventorySlots[4].isFull = true;
                    GameManager.Instance.inventorySlots[4].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[4].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inShieldSlot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[4].transform);
                    GameManager.Instance.shieldID = itemID;
                    GameManager.Instance.SpawnItem("Shield", GameManager.Instance.spawnedShield);
                }       
            }
            else if (!currentSlot.accessory1Slot && equipType == equipment.Accessory)
            {
                if (GameManager.Instance.inventorySlots[7].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[7].currentItem;
                    currentSlot.currentItem.inAccessory1Slot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);
                    currentSlot.GetComponent<TooltipTrigger>().header = currentSlot.currentItem.item.itemName;
                    currentSlot.GetComponent<TooltipTrigger>().content = currentSlot.currentItem.item.GetTooltipInfoText();

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[7].currentItem = this;
                    GameManager.Instance.inventorySlots[7].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[7].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inAccessory1Slot = true;
                    transform.SetParent(GameManager.Instance.inventorySlots[7].transform);
                    GameManager.Instance.accessory1ID = itemID;
                    GameManager.Instance.SpawnItem("Accessory1", GameManager.Instance.spawnedAccessory1);
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;
                    currentSlot.GetComponent<TooltipTrigger>().header = null;
                    currentSlot.GetComponent<TooltipTrigger>().content = null;
                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[7].currentItem = this;
                    GameManager.Instance.inventorySlots[7].isFull = true;
                    GameManager.Instance.inventorySlots[7].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[7].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inAccessory1Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[7].transform);
                    GameManager.Instance.accessory1ID = itemID;
                    GameManager.Instance.SpawnItem("Accessory1", GameManager.Instance.spawnedAccessory1);
                }
            }
            else if (!currentSlot.accessory2Slot && equipType == equipment.Accessory)
            {
                if (GameManager.Instance.inventorySlots[8].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[8].currentItem;
                    currentSlot.currentItem.inAccessory2Slot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);
                    currentSlot.GetComponent<TooltipTrigger>().header = currentSlot.currentItem.item.itemName;
                    currentSlot.GetComponent<TooltipTrigger>().content = currentSlot.currentItem.item.GetTooltipInfoText();

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[8].currentItem = this;
                    GameManager.Instance.inventorySlots[8].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[8].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inAccessory2Slot = true;
                    transform.SetParent(GameManager.Instance.inventorySlots[8].transform);
                    GameManager.Instance.accessory2ID = itemID;
                    GameManager.Instance.SpawnItem("Accessory2", GameManager.Instance.spawnedAccessory2);
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;
                    currentSlot.GetComponent<TooltipTrigger>().header = null;
                    currentSlot.GetComponent<TooltipTrigger>().content = null;
                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[8].currentItem = this;
                    GameManager.Instance.inventorySlots[8].isFull = true;
                    GameManager.Instance.inventorySlots[8].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[8].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inAccessory2Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[8].transform);
                    GameManager.Instance.accessory2ID = itemID;
                    GameManager.Instance.SpawnItem("Accessory2", GameManager.Instance.spawnedAccessory2);
                }
            }
            else if (!currentSlot.accessory3Slot && equipType == equipment.Accessory)
            {
                if (GameManager.Instance.inventorySlots[9].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[9].currentItem;
                    currentSlot.currentItem.inAccessory3Slot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);
                    currentSlot.GetComponent<TooltipTrigger>().header = currentSlot.currentItem.item.itemName;
                    currentSlot.GetComponent<TooltipTrigger>().content = currentSlot.currentItem.item.GetTooltipInfoText();

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[9].currentItem = this;
                    GameManager.Instance.inventorySlots[9].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[9].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inAccessory3Slot = true;
                    transform.SetParent(GameManager.Instance.inventorySlots[9].transform);
                    GameManager.Instance.accessory3ID = itemID;
                    GameManager.Instance.SpawnItem("Accessory3", GameManager.Instance.spawnedAccessory3);
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;
                    currentSlot.GetComponent<TooltipTrigger>().header = null;
                    currentSlot.GetComponent<TooltipTrigger>().content = null;

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[9].currentItem = this;
                    GameManager.Instance.inventorySlots[9].isFull = true;
                    GameManager.Instance.inventorySlots[9].GetComponent<TooltipTrigger>().header = this.item.itemName;
                    GameManager.Instance.inventorySlots[9].GetComponent<TooltipTrigger>().content = this.item.GetTooltipInfoText();
                    inAccessory3Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[9].transform);
                    GameManager.Instance.accessory3ID = itemID;
                    GameManager.Instance.SpawnItem("Accessory3", GameManager.Instance.spawnedAccessory3);
                }
            }
            else if(this.item is ConsumableItem)
            {
                //Uses
                if((item as ConsumableItem ).use == ConsumableItem.Use.heal)
                    stats.Heal((item as ConsumableItem ).amount);

                currentSlot.currentItem.currentAmount -= 1;

                if(currentSlot.currentItem.currentAmount == 0)
                {
                    //Remove Item
                    currentSlot.isFull = false;
                    currentSlot.currentItem = null;
                    currentSlot.GetComponent<TooltipTrigger>().header = null;
                    currentSlot.GetComponent<TooltipTrigger>().content = null;
                    Destroy(gameObject);
                }
            }
        }
    }
}
