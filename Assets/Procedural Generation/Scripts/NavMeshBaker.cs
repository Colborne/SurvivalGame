using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;
public class NavMeshBaker : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface navMeshSurface;
    GameObject chunk;
    ObjectGenerator objectGenerator;
    DeleteVertsByHeight deleteVerts;
    LevelManager level;

    void Start()
    {
        objectGenerator = GetComponent<ObjectGenerator>();
        deleteVerts = GetComponent<DeleteVertsByHeight>();
        level = FindObjectOfType<LevelManager>();
    }
    private void Update() 
    {
        if(chunk != null)
            CreateMesh();
        else
            chunk = GameObject.Find("Island Generator/Terrain Chunk");
    }
    public void CreateMesh()
    {
        chunk.AddComponent<NavMeshSurface>();
        navMeshSurface = GetComponentInChildren<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();

        deleteVerts.MeshCreation();
  
        if(File.Exists(Application.persistentDataPath + "/mako.objs"))
            objectGenerator.load();
        else
            objectGenerator.spawn();
            
        level.Loaded();
        Destroy(this);
    }
}
