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