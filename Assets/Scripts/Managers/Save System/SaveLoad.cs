using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoad 
{
    public static void SaveData(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Mako.plyr";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData charData = new PlayerData(player);

        formatter.Serialize(stream, charData);
        stream.Close();
    }
    public static void SaveData(SaveableObject[] allGameObjects)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Mako.objs";

        FileStream stream = new FileStream(path, FileMode.Create);
        
        ObjectData[] objData = new ObjectData[allGameObjects.Length];
        for(int i = 0; i < allGameObjects.Length; i++)
            objData[i] = new ObjectData(allGameObjects[i]);
        
        AllObjectData data = new AllObjectData(objData);
        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static void SaveData(World world)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Mako.seed";

        FileStream stream = new FileStream(path, FileMode.Create);

        WorldData worldData = new WorldData(world);

        formatter.Serialize(stream, worldData);
        stream.Close();
    }
    
    public static PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/Mako.plyr";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }

    public static WorldData LoadWorld()
    {
        string path = Application.persistentDataPath + "/mako.seed";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            WorldData data = formatter.Deserialize(stream) as WorldData;

            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }

    public static AllObjectData LoadObjects()
    {
        string path = Application.persistentDataPath + "/Mako.objs";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            AllObjectData data = formatter.Deserialize(stream) as AllObjectData;

            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }
}