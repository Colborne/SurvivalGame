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

    Transform built;
    
    RaycastHit Hit;

    private void Update() 
    {
        if(FindObjectOfType<InputManager>().rightMouseInput)
        {
            FindObjectOfType<InputManager>().rightMouseInput = !FindObjectOfType<InputManager>().rightMouseInput;
            FindObjectOfType<InputManager>().buildWindowFlag = !FindObjectOfType<InputManager>().buildWindowFlag;
            BuildWindow.SetActive(!BuildWindow.active);
        }

        Builder.GetComponent<MeshFilter>().mesh = buildables[iteration].mesh;

        if(Physics.Raycast(Cam.position, Cam.forward, out Hit, 22f))
        {
            Builder.position = new Vector3(
            Mathf.RoundToInt(Hit.point.x)  != 0 ? Mathf.RoundToInt(Hit.point.x/4) * 4: 3,
            (Mathf.RoundToInt(Hit.point.y) != 0 ? Mathf.RoundToInt(Hit.point.y/4) * 4: 0), //+ Builder.localScale.y ,
            Mathf.RoundToInt(Hit.point.z)  != 0 ? Mathf.RoundToInt(Hit.point.z/4) * 4: 3);

            Builder.eulerAngles = new Vector3(0,Mathf.RoundToInt(Cam.eulerAngles.y) != 0 ? Mathf.RoundToInt(Cam.eulerAngles.y / 90f) * 90 : 0, 0);

            if(FindObjectOfType<InputManager>().leftMouseInput && !FindObjectOfType<InputManager>().inventoryFlag && !FindObjectOfType<InputManager>().buildWindowFlag)
            {
                FindObjectOfType<InputManager>().leftMouseInput = false;

                if (checkIfPosEmpty(Builder.position, Builder.rotation) 
                && GameManager.Instance.CraftingCheck(buildables[iteration].GetComponent<CraftingRecipe>().items, buildables[iteration].GetComponent<CraftingRecipe>().amountRequired)){ //if(GameManager.Instance.CheckInventoryForItem(GetComponent<BuildRecipe>().item, GetComponent<BuildRecipe>().amountRequired, true))
                        Instantiate(buildables[iteration].prefab, Builder.position, Builder.rotation);
                        GameManager.Instance.Craft(buildables[iteration].GetComponent<CraftingRecipe>().items, buildables[iteration].GetComponent<CraftingRecipe>().amountRequired);
                }
                
                                    //if (checkIfPosEmpty(Builder.position, Builder.rotation * Quaternion.Euler(-90,0,0)))
                    //    Instantiate(WallPrefab, Builder.position, Builder.rotation * Quaternion.Euler(-90,0,0));
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
}