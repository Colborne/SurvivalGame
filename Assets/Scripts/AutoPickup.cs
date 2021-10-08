using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPickup : MonoBehaviour
{
    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.GetComponent<Pickup>()) 
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position , 1 * Time.deltaTime); //use this for auto pickup
        }
    }
}