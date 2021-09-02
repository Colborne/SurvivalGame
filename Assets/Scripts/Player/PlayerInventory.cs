using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collect.Containers;
using Collect.Items;

public class PlayerInventory : MonoBehaviour
{

    public WeaponSlotManager weaponSlotManager;
    public HelmetSlotManager helmetSlotManager;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public WeaponItem unarmed;
    public HelmetItem helmet;
    public Container Inventory;
    public GameObject ContainerObject;

    private void Awake() 
    {
        Inventory = ContainerObject.GetComponent<Container>();
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        helmetSlotManager = GetComponentInChildren<HelmetSlotManager>();
    }

    private void Start() {
        if(rightWeapon == null)
            rightWeapon = unarmed;
        if(leftWeapon == null)
            leftWeapon = unarmed;

        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        helmetSlotManager.LoadHelmetOnSlot(helmet);
    }
}
