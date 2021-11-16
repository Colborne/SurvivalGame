using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingObject : MonoBehaviour
{
    public float timeSinceInitialization;
    public float timer;
    public float growthTime;
    public GameObject grown;
    public bool load = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!load)
            timer = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceInitialization = Time.timeSinceLevelLoad - timer;
        if(timeSinceInitialization > growthTime){
            Instantiate(grown, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
