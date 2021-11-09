
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager : MonoBehaviour
{
    public SaveableObject[] allGameObjects;

    public void GatherData()
    {
        allGameObjects = FindObjectsOfType(typeof(SaveableObject)) as SaveableObject[];
    }

    public void SaveObjects()
    {
        GatherData();
        SaveLoad.SaveData(allGameObjects);
    }

    public void LoadObjects()
    {
        AllObjectData data = SaveLoad.LoadObjects();

        for(int i = 0; i < data.objectDatas.Length; i++)
        {
            Vector3 position = new Vector3(data.objectDatas[i].pos[0],data.objectDatas[i].pos[1],data.objectDatas[i].pos[2]);
            Quaternion rotation = Quaternion.Euler(new Vector3(data.objectDatas[i].rot[0],data.objectDatas[i].rot[1],data.objectDatas[i].rot[2]));
            Vector3 localScale = new Vector3(data.objectDatas[i].scale[0],data.objectDatas[i].scale[1],data.objectDatas[i].scale[2]);

            Debug.Log(data.objectDatas[i].localPath);
            GameObject Spawn = Instantiate(Resources.Load(data.objectDatas[i].localPath) as GameObject , position, rotation);
            Spawn.transform.localScale = localScale;

            if(Spawn.GetComponent<ObjectStats>())
                Spawn.GetComponent<ObjectStats>().currentHealth = data.objectDatas[i].amount;
            else if(Spawn.GetComponent<EnemyStats>())
                Spawn.GetComponent<EnemyStats>().currentHealth = data.objectDatas[i].amount;
            else if(Spawn.GetComponent<Pickup>())
            {
                Spawn.GetComponent<Pickup>().amount = data.objectDatas[i].amount;
                Spawn.GetComponent<Pickup>()._name = data.objectDatas[i]._name;
            }
                
        }
        
        GatherData();
    }
}