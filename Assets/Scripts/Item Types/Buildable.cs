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
    TooltipTrigger tooltipTrigger;

    private void Awake() {
        mesh = prefab.GetComponentInChildren<MeshFilter>().sharedMesh;  
        buildSystem = FindObjectOfType<BuildSystem>();
        tooltipTrigger = GetComponent<TooltipTrigger>();
    }
    
    private void Start()
    {
        tooltipTrigger.header = prefab.name;
        StringBuilder builder = new StringBuilder();
        for(int i = 0; i< recipe.items.Length; i++)
        {
            builder.Append(recipe.items[i].item.itemName);
            builder.Append(" x");
            builder.Append(recipe.amountRequired[i].ToString()).AppendLine();
        }
        tooltipTrigger.content = builder.ToString();
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
