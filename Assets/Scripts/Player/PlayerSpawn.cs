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

    public void Spawn()
    {
        if(File.Exists(Application.persistentDataPath + "/" + PersistentData.name + ".plyr"))
            player.Load();
        else
        {
            GameObject spawner = GameObject.Find("Spawn Location(Clone)");
            transform.position = spawner.transform.position;
        }
    }
}
