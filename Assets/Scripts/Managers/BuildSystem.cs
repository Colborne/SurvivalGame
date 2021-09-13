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

        if(Physics.Raycast(Cam.position, Cam.forward, out Hit, 40f))
        {
            Builder.position = new Vector3(
            Mathf.RoundToInt(Hit.point.x)  != 0 ? Mathf.RoundToInt(Hit.point.x/4) * 4: 3,
            (Mathf.RoundToInt(Hit.point.y) != 0 ? Mathf.RoundToInt(Hit.point.y/4) * 4: 0), //+ Builder.localScale.y ,
            Mathf.RoundToInt(Hit.point.z)  != 0 ? Mathf.RoundToInt(Hit.point.z/4) * 4: 3);

            Builder.eulerAngles = new Vector3(0,Mathf.RoundToInt(Cam.eulerAngles.y) != 0 ? Mathf.RoundToInt(Cam.eulerAngles.y / 90f) * 90 : 0, 0);

            if(FindObjectOfType<InputManager>().leftMouseInput)
            {
                FindObjectOfType<InputManager>().leftMouseInput = false;
                if (prefab == 0)
                    Instantiate(FloorPrefab, Builder.position, Builder.rotation);
                else
                    Instantiate(WallPrefab, Builder.position, Builder.rotation * Quaternion.Euler(-90,0,0));
            }

            /*            
            Mathf.RoundToInt(Hit.point.x),
            Mathf.RoundToInt(Hit.point.y) + FloorBuild.localScale.y ,
            Mathf.RoundToInt(Hit.point.z));
            */
        }
    }
}
