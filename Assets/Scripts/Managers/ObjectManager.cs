
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

            if(Spawn.GetComponent<GrowingObject>())
            {
                Spawn.GetComponent<GrowingObject>().load = true;
                Spawn.GetComponent<GrowingObject>().currentHealth = data.objectDatas[i].amount;
                Spawn.GetComponent<GrowingObject>().timer = data.objectDatas[i].timer[0];
            }
            else if(Spawn.GetComponent<ObjectStats>())
                Spawn.GetComponent<ObjectStats>().currentHealth = data.objectDatas[i].amount;
            else if(Spawn.GetComponent<EnemyStats>())
                Spawn.GetComponent<EnemyStats>().currentHealth = data.objectDatas[i].amount;
            else if(Spawn.GetComponent<Pickup>())
            {
                Spawn.GetComponent<Pickup>().amount = data.objectDatas[i].amount;
                Spawn.GetComponent<Pickup>()._name = data.objectDatas[i]._name;
            }
            else if (Spawn.GetComponent<ChestWindowManager>())
            {
                Spawn.GetComponent<ChestWindowManager>().iter = data.objectDatas[i].amount;  
                Spawn.GetComponent<ChestWindowManager>().load = true;
                for(int j = 0; j < 16; j++)
                {
                    if(data.objectDatas[i].count[j] != 0){
                        Spawn.GetComponent<ChestWindowManager>().LoadInventoryItem(data.objectDatas[i].slot[j], data.objectDatas[i].id[j], data.objectDatas[i].count[j]);  
                    }
                }
            }
            else if(Spawn.GetComponent<Smelting>())
            {
                Spawn.GetComponent<Smelting>().load = true;
                for(int j = 0; j < data.objectDatas[i].id.Length; j++)
                    Spawn.GetComponent<Smelting>().smelt.Enqueue(GameManager.Instance.equipment[data.objectDatas[i].id[j]].worldItem);  
                Spawn.GetComponent<Smelting>().min = data.objectDatas[i].count[0];  
                Spawn.GetComponent<Smelting>().timer = data.objectDatas[i].timer[0]; 
            }
            else if(Spawn.GetComponent<Cooking>())
            {
                Spawn.GetComponent<Cooking>().load = true;
                Spawn.GetComponent<Cooking>().iter = data.objectDatas[i].amount;  
                Spawn.GetComponent<Cooking>().min = data.objectDatas[i].count[0];
                Spawn.GetComponent<Cooking>().woodCount = data.objectDatas[i].count[1];  
                Spawn.GetComponent<Cooking>().timer = data.objectDatas[i].timer[0];  
                Spawn.GetComponent<Cooking>().cookTime = data.objectDatas[i].timer[1];
                if(Spawn.GetComponent<Cooking>().iter != -1)
                    Spawn.GetComponent<Cooking>().cooking = true;
            }
        }    
        GatherData();
    }
}
