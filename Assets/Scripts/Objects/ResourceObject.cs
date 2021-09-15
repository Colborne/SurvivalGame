using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    public string resourceType;
    public int toolRequiredLevel;
    public GameObject fx;
    public GameObject drop;
    ObjectStats objectStats;
    private void Awake() {
        objectStats = GetComponent<ObjectStats>();
    }

    public void checkHit()
    {
        if(objectStats.currentHealth <= 0)
        {
            for(int i = 0; i < Random.Range(2f,15f); i++)
            {
                var _drop = Instantiate(drop, transform.position + new Vector3(Random.Range(-.5f,.5f),Random.Range(-.5f,.5f),Random.Range(-.5f,.5f)), Random.rotation);
                _drop.GetComponentInChildren<Rigidbody>().AddForce(transform.up * 20f);
            }
            Destroy(gameObject);
        }
    }
}