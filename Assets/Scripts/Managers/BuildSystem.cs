using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] Transform Cam;
    [SerializeField] Transform Builder;
    [SerializeField] GameObject BuildWindow;
    [SerializeField] Buildable[] buildables;
    public int iteration = 0;
    RaycastHit Hit;
    Quaternion orientation;

    InputManager input;
    int buildRotation = 0;
    public int last = -1;
    public float distance = 22.5f;


    private void Start() 
    {
        input = FindObjectOfType<InputManager>();
    }

    private void Update() 
    {
        if(input.scrollInput > 0)
        {
            buildRotation++;
            input.scrollInput = 0;
        }
        else if(input.scrollInput < 0)
        {
            buildRotation--;
            input.scrollInput = 0;
        }
        else
            buildRotation = buildRotation;
        
        if(buildRotation > 16)
            buildRotation = -16;
        if(buildRotation < -16)
            buildRotation = 16;

        if(input.rightMouseInput && !input.inventoryFlag )
        {
            input.rightMouseInput = !input.rightMouseInput;
            CloseWindow();
        }

        if(iteration != last)
        {
            Builder.GetComponent<MeshFilter>().mesh = buildables[iteration].mesh;
            orientation = buildables[iteration].orientation;
            Builder.localScale = buildables[iteration].prefab.transform.localScale;
            last = iteration;
        }

        if(Physics.Raycast(Cam.position, Cam.forward, out Hit, distance))
        {     
            Builder.position = new Vector3(
            Mathf.RoundToInt(Hit.point.x)  != 0 ? Mathf.RoundToInt(Hit.point.x/4) * 4: 3,
            Mathf.RoundToInt(Hit.point.y) != 0 ? Mathf.RoundToInt(Hit.point.y/4) * 4: 0, // + Builder.localScale.y,
            Mathf.RoundToInt(Hit.point.z)  != 0 ? Mathf.RoundToInt(Hit.point.z/4) * 4: 3);

            orientation = Quaternion.Euler(0, 45 * buildRotation, 0);
            
            Builder.eulerAngles = orientation.eulerAngles; //new Vector3(0,Mathf.RoundToInt(Cam.eulerAngles.y) != 0 ? Mathf.RoundToInt(Cam.eulerAngles.y / 90f) * 90 : 0, 0) + orientation.eulerAngles;           

            if(input.leftMouseInput && !input.inventoryFlag && !input.buildWindowFlag)
            {
                input.leftMouseInput = false;

                if (checkIfPosEmpty(Builder.position, Builder.rotation) 
                && GameManager.Instance.CraftingCheck(buildables[iteration].GetComponent<CraftingRecipe>().items, buildables[iteration].GetComponent<CraftingRecipe>().amountRequired)){ //if(GameManager.Instance.CheckInventoryForItem(GetComponent<BuildRecipe>().item, GetComponent<BuildRecipe>().amountRequired, true))
                    Instantiate(buildables[iteration].prefab, Builder.position, Builder.rotation);
                    GameManager.Instance.Craft(buildables[iteration].GetComponent<CraftingRecipe>().items, buildables[iteration].GetComponent<CraftingRecipe>().amountRequired);
                }
            }
        }
        
    }
    public bool checkIfPosEmpty(Vector3 targetPos, Quaternion targetRot)
    {
        GameObject[] allMovableThings = GameObject.FindGameObjectsWithTag("Build");
        foreach(GameObject current in allMovableThings)
        {
            if(current.transform.position == targetPos && current.transform.rotation == targetRot)
                return false;
        }
        return true;
    }

    public void CloseWindow()
    {
        input.buildWindowFlag = !input.buildWindowFlag;
        BuildWindow.SetActive(!BuildWindow.active);
        input.TooltipCanvas.SetActive(!input.TooltipCanvas.active);    
    }
}