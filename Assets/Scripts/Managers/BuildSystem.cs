using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] Transform Cam;
    [SerializeField] Transform Builder;
    [SerializeField] Transform FloorBuild;
    [SerializeField] Transform WallBuild;
    [SerializeField] Transform FloorPrefab;
    [SerializeField] Transform WallPrefab;

    Transform built;

    RaycastHit Hit;
    int prefab = 0;

    private void Update() 
    {
        if(FindObjectOfType<InputManager>().buildInput)
        {
            if(prefab == 1)
                prefab = 0;
            else
                prefab = 1;
            FindObjectOfType<InputManager>().buildInput = !FindObjectOfType<InputManager>().buildInput;
        }

        if(prefab == 0)
        {
            FloorBuild.gameObject.SetActive(true);
            Builder = FloorBuild;
            WallBuild.gameObject.SetActive(false);
        }
        else        
        {
            WallBuild.gameObject.SetActive(true);
            Builder = WallBuild;
            FloorBuild.gameObject.SetActive(false);
        }

        if(Physics.Raycast(Cam.position, Cam.forward, out Hit, 22f))
        {
            Builder.position = new Vector3(
            Mathf.RoundToInt(Hit.point.x)  != 0 ? Mathf.RoundToInt(Hit.point.x/4) * 4: 3,
            (Mathf.RoundToInt(Hit.point.y) != 0 ? Mathf.RoundToInt(Hit.point.y/4) * 4: 0), //+ Builder.localScale.y ,
            Mathf.RoundToInt(Hit.point.z)  != 0 ? Mathf.RoundToInt(Hit.point.z/4) * 4: 3);

            Builder.eulerAngles = new Vector3(0,Mathf.RoundToInt(Cam.eulerAngles.y) != 0 ? Mathf.RoundToInt(Cam.eulerAngles.y / 90f) * 90 : 0, 0);

            if(FindObjectOfType<InputManager>().leftMouseInput && !FindObjectOfType<InputManager>().inventoryFlag)
            {
                FindObjectOfType<InputManager>().leftMouseInput = false;
                
                if (prefab == 0)
                {
                    if (checkIfPosEmpty(Builder.position, Builder.rotation) 
                    && GameManager.Instance.CraftingCheck(GetComponent<CraftingRecipe>().items, GetComponent<CraftingRecipe>().amountRequired)) //if(GameManager.Instance.CheckInventoryForItem(GetComponent<BuildRecipe>().item, GetComponent<BuildRecipe>().amountRequired, true))
                        Instantiate(FloorPrefab, Builder.position, Builder.rotation);
                }
                else
                {
                    if (checkIfPosEmpty(Builder.position, Builder.rotation * Quaternion.Euler(-90,0,0)))
                        Instantiate(WallPrefab, Builder.position, Builder.rotation * Quaternion.Euler(-90,0,0));
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
}