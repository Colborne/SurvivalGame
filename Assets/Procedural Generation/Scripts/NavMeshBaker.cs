using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshBaker : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface navMeshSurface;
    GameObject chunk;

    void Start()
    {
        Invoke("CreateMesh", .05f);
    }

    void CreateMesh()
    {
        chunk = GameObject.Find("Island Generator/Terrain Chunk");
        chunk.AddComponent<NavMeshSurface>();
        navMeshSurface = GetComponentInChildren<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
    }
}
