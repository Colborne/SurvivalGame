using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    WeaponSlotManager weaponSlotManager;
    HelmetSlotManager helmetSlotManager;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public WeaponItem unarmed;
    public HelmetItem helmet;

    public List<Item> Inventory;

    private void Awake() 
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        helmetSlotManager = GetComponentInChildren<HelmetSlotManager>();
    }

    private void Start() {
        rightWeapon = unarmed;
        leftWeapon = unarmed;
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        helmetSlotManager.LoadHelmetOnSlot(helmet);
    }
}
