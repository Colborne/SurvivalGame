using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Collect.Items;

public class ItemPickup : Interactable
{
    public GameObject item;
    bool pickedUp = false;

    private void Update() {

        if(pickedUp)
            Destroy(gameObject);
    }
    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);
        PickUpItem(playerManager);
    }

    private void PickUpItem(PlayerManager playerManager)
    {
        pickedUp = true;
        PlayerInventory playerInventory;
        PlayerLocomotion playerLocomotion;
        AnimatorManager animatorManager;

        playerInventory = playerManager.GetComponent<PlayerInventory>();
        playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
        animatorManager = playerManager.GetComponent<AnimatorManager>();

        playerLocomotion.GetComponent<Rigidbody>().velocity = Vector3.zero;
        animatorManager.PlayTargetAnimation("Landing", true);
        GameObject inst = GameObject.Instantiate(item, Vector3.zero, Quaternion.identity) as GameObject;
        playerInventory.Inventory.Add(inst);
    }
}
