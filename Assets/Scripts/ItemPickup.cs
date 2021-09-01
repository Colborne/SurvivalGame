using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);
        PickUpItem(playerManager);
    }

    private void PickUpItem(PlayerManager playerManager)
    {
        PlayerInventory playerInventory;
        PlayerLocomotion playerLocomotion;
        AnimatorManager animatorManager;

        playerInventory = playerManager.GetComponent<PlayerInventory>();
        playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
        animatorManager = playerManager.GetComponent<AnimatorManager>();

        playerLocomotion.GetComponent<Rigidbody>().velocity = Vector3.zero;
        animatorManager.PlayTargetAnimation("Landing", true);
        playerInventory.Inventory.Add(item);
        playerManager.itemInteractableUIGameObject.GetComponentInChildren<Text>().text = item.itemName;
        playerManager.itemInteractableUIGameObject.SetActive(true);
        Destroy(gameObject);
    }
}
