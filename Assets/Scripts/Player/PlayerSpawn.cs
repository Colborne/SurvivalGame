using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", 8f);
    }

    void Spawn()
    {
        GameObject spawner = GameObject.Find("Spawn Location(Clone)");
        transform.position = spawner.transform.position;
    }

}
