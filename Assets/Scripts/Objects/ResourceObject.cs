using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    public string resourceType;
    public int toolRequiredLevel;
    public GameObject drop;

    ObjectStats objectStats;
    private void Awake() {
        objectStats = GetComponent<ObjectStats>();
    }

    public void checkHit()
    {
        if(objectStats.currentHealth <= 0)
        {
            Instantiate(drop, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}