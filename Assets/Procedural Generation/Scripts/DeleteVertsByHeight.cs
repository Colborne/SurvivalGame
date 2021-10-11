using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
 
 public class DeleteVertsByHeight : MonoBehaviour {
 
     public Material GrassMaterial;
     public float heightReferenceFloat = 0;
     public float heightReferenceFloatTop = 0;
     float heightCutOff;
     float heightCutOffTop;
    float errorAdjustment = 2;
 
     void Start() 
     {
         Invoke("MeshCreation", .1f);
     }
     public void MeshCreation()
     {
        Transform grassMesh = Instantiate(transform.Find("Terrain Chunk"));
        grassMesh.parent = transform;

        Mesh mesh = grassMesh.GetComponent<MeshFilter>().mesh;
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uv = mesh.uv;
        Vector3[] normals = mesh.normals;
        List<Vector3> vertList = new List<Vector3>();
        List<Vector2> uvList = new List<Vector2>();
        List<Vector3> normalsList = new List<Vector3>();
        List<int> trianglesList = new List<int>();

        heightCutOff = heightReferenceFloat;
        heightCutOffTop = heightReferenceFloatTop;      

        int i = 0;
        while (i < vertices.Length) {
                vertList.Add (vertices[i]); 
                uvList.Add (uv[i]);
                normalsList.Add (normals[i]);
            i++;
        }
        for (int triCount = 0; triCount < triangles.Length; triCount += 3) {
            if ((transform.TransformPoint(vertices[triangles[triCount  ]]).y > heightCutOff+errorAdjustment)  &&
                (transform.TransformPoint(vertices[triangles[triCount+1]]).y > heightCutOff+errorAdjustment)  &&
                (transform.TransformPoint(vertices[triangles[triCount+2]]).y > heightCutOff+errorAdjustment) &&
                (transform.TransformPoint(vertices[triangles[triCount  ]]).y < heightCutOffTop+errorAdjustment)  &&
                (transform.TransformPoint(vertices[triangles[triCount+1]]).y < heightCutOffTop+errorAdjustment)  &&
                (transform.TransformPoint(vertices[triangles[triCount+2]]).y < heightCutOffTop+errorAdjustment)) {
                
                trianglesList.Add (triangles[triCount]);
                trianglesList.Add (triangles[triCount+1]);
                trianglesList.Add (triangles[triCount+2]);
            }
        }

        triangles = trianglesList.ToArray ();
        vertices = vertList.ToArray ();
        uv = uvList.ToArray ();
        normals = normalsList.ToArray ();
        //mesh.Clear();
        mesh.triangles = triangles;
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.normals = normals;
        grassMesh.GetComponent<Renderer>().material = GrassMaterial;
     }
 }