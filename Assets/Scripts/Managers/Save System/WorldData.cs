using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WorldData
{
    public int seed;
    public WorldData(World world) 
    {
        seed = world.seed;
    }
}
