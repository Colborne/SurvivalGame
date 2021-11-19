using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingObject : ObjectStats
{
    public float timer;
    public float growthTime;
    public GameObject grown;
    public bool load = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!load)
            timer = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer > growthTime){
            Instantiate(grown, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {

    }

    void ShowDamage(string text)
    {

    }
}
