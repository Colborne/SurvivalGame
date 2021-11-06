using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public int seed;

    public void SaveGame()
    {
        SaveLoad.SaveData(this);
    }

    public void Load()
    {
        WorldData data = SaveLoad.LoadWorld();
        seed = data.seed;
    }
}
