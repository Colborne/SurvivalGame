using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text;

public class Buildable : MonoBehaviour, IPointerClickHandler
{
    public CraftingRecipe recipe;
    public GameObject prefab;
    public Mesh mesh;
    public int value;
    public Quaternion orientation;

    BuildSystem buildSystem;

    private void Awake() {
        mesh = prefab.GetComponent<MeshFilter>().sharedMesh;
        buildSystem = FindObjectOfType<BuildSystem>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            buildSystem.iteration = value;
            buildSystem.CloseWindow();
        }  
    }      
}
