using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollection : MonoBehaviour
{
    public int counter;
    private void OnTriggerEnter(Collider other) 
    {
        Destroy(other);
        counter++;
        Debug.Log(counter);
    }
}
