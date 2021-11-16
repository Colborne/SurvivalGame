using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class ObjectData
{
    public float[] pos;
    public float[] rot;
    public float[] scale;
    public string localPath;
    public int amount;
    public string _name;
    public float[] timer;

    public int[] slot;
    public int[] id;
    public int[] count;

    public ObjectData(SaveableObject so) 
    {
        string temp = "";
        if(so.folder == SaveableObject.itemType.Altars) temp = "Altars/";
        else if(so.folder == SaveableObject.itemType.Buildable) temp = "Buildable/";
        else if(so.folder == SaveableObject.itemType.Consumable) temp = "Consumable/";
        else if(so.folder == SaveableObject.itemType.Equipment) temp = "Equipment/";
        else if(so.folder == SaveableObject.itemType.Mobs) temp = "Mobs/";
        else if(so.folder == SaveableObject.itemType.Ores) temp = "Ores/";
        else if(so.folder == SaveableObject.itemType.Others) temp = "Others/";
        else if(so.folder == SaveableObject.itemType.ResourceItems) temp = "ResourceItems/";
        else if(so.folder == SaveableObject.itemType.Trees) temp = "Trees/";
        else if(so.folder == SaveableObject.itemType.Weapons) temp = "Weapons/";

        localPath = "Items/" + temp + so.AssetPath + " (World)";

        pos = new float[3];
        rot = new float[3];
        scale = new float[3];

        pos[0] = so.transform.position.x;
        pos[1] = so.transform.position.y;
        pos[2] = so.transform.position.z;

        rot[0] = so.transform.eulerAngles.x;
        rot[1] = so.transform.eulerAngles.y;
        rot[2] = so.transform.eulerAngles.z;
        
        scale[0] = so.transform.localScale.x;
        scale[1] = so.transform.localScale.y;
        scale[2] = so.transform.localScale.z;      

        if(so.GetComponent<ObjectStats>())//!(ReferenceEquals(so.objectStats, null) ? false : (so.objectStats ? false : true)))
            amount = so.GetComponent<ObjectStats>().currentHealth;
        else if(so.GetComponent<EnemyStats>())
            amount = so.GetComponent<EnemyStats>().currentHealth;
        else if(so.GetComponent<Pickup>())
        {
            amount = so.GetComponent<Pickup>().amount;
            _name = so.AssetPath.Split('/').Where(x => !string.IsNullOrWhiteSpace(x)).LastOrDefault();
        }
        else if(so.GetComponent<ChestWindowManager>())
        {
            amount = so.GetComponent<ChestWindowManager>().iter; 
            so.GetComponent<ChestWindowManager>().Save();

            slot = new int[16];
            id = new int[16];
            count = new int[16];

            for(int i = 0; i < 16; i++)
            {
                slot[i] = so.GetComponent<ChestWindowManager>().chest[i].slot;
                id[i] = so.GetComponent<ChestWindowManager>().chest[i].itemID;
                count[i] = so.GetComponent<ChestWindowManager>().chest[i].currentAmount;

                Debug.Log(slot[i]);
                Debug.Log(id[i]);
                Debug.Log(count[i]);
            }
        }
        else if (so.GetComponent<Smelting>())
        {
            amount = so.GetComponent<Smelting>().iter;
            GameObject[] items = so.GetComponent<Smelting>().smelt.ToArray();
            id = new int[items.Length];
            for(int i = 0; i < items.Length; i++){
                id[i] = items[i].GetComponent<Pickup>().itemID;
                Debug.Log(id[i]);
            }
            
            count = new int[1];
            count[0] = so.GetComponent<Smelting>().min;
            timer = new float[1];
            timer[0] = so.GetComponent<Smelting>().timer;
        }
        else if (so.GetComponent<Cooking>())
        {
            amount = so.GetComponent<Cooking>().iter;
            count = new int[2];
            count[0] = so.GetComponent<Cooking>().min;
            count[1] = so.GetComponent<Cooking>().woodCount;
            timer = new float[2];
            timer[0] = so.GetComponent<Cooking>().timer;
            timer[0] = so.GetComponent<Cooking>().cookTime;
        }
    }
}

[System.Serializable]
public class AllObjectData
{
    public ObjectData[] objectDatas;
    public AllObjectData(ObjectData[] objectDatas)
    {
        this.objectDatas = objectDatas;
    }
}











/*
        allGameObjects = new string[om.allGameObjects.Length];
        pos = new float[3 * om.allGameObjects.Length];
        rot = new float[3 * om.allGameObjects.Length];
        scale = new float[3 * om.allGameObjects.Length];

        for(int i = 0; i < om.allGameObjects.Length; i++)
        {
            pos[3 * i] =     om.allGameObjects[i].gameObject.transform.position.x;
            pos[1 + 3 * i] = om.allGameObjects[i].gameObject.transform.position.y;
            pos[2 + 3 * i] = om.allGameObjects[i].gameObject.transform.position.z;

            rot[3 * i] =     om.allGameObjects[i].gameObject.transform.eulerAngles.x;
            rot[1 + 3 * i] = om.allGameObjects[i].gameObject.transform.eulerAngles.y;
            rot[2 + 3 * i] = om.allGameObjects[i].gameObject.transform.eulerAngles.z;
            
            scale[3 * i] =     om.allGameObjects[i].gameObject.transform.localScale.x;
            scale[1 + 3 * i] = om.allGameObjects[i].gameObject.transform.localScale.y;
            scale[2 + 3 * i] = om.allGameObjects[i].gameObject.transform.localScale.z;
        }
        */