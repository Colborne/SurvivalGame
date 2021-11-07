using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerSpawn : MonoBehaviour
{
    Player player;
    private void Awake() 
    {
        player = GetComponent<Player>();
    }
    void Start()
    {
        Invoke("Spawn", 6f);
    }

    void Spawn()
    {
        if(File.Exists(Application.persistentDataPath + "/mako.plyr"))
            player.Load();
        else
        {
            GameObject spawner = GameObject.Find("Spawn Location(Clone)");
            transform.position = spawner.transform.position;
        }

    }

}
