﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int itemID;
    InputManager inputManager;

    private void Awake() {
        inputManager = FindObjectOfType<InputManager>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == GameManager.Instance.PM.gameObject && inputManager.interactInput)
        {

            GameManager.Instance.PickupItem(itemID);
            Destroy(gameObject);
            
        }
    }
}
