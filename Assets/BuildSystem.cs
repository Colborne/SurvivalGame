using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] Transform Cam;
    [SerializeField] Transform FloorBuild;
    [SerializeField] Transform FloorPrefab;

    RaycastHit Hit;

    private void Update() 
    {
        if(Physics.Raycast(Cam.position, Cam.forward, out Hit, 40f))
        {
            FloorBuild.position = new Vector3(
            Mathf.RoundToInt(Hit.point.x)  != 0 ? Mathf.RoundToInt(Hit.point.x/4) * 4: 3,
            (Mathf.RoundToInt(Hit.point.y) != 0 ? Mathf.RoundToInt(Hit.point.y/4) * 4: 0) + FloorBuild.localScale.y ,
            Mathf.RoundToInt(Hit.point.z)  != 0 ? Mathf.RoundToInt(Hit.point.z/4) * 4: 3);

            FloorBuild.eulerAngles = new Vector3(0,Mathf.RoundToInt(transform.eulerAngles.y) != 0 ? Mathf.RoundToInt(transform.eulerAngles.y / 90f) * 90 : 0, 0);

            if(FindObjectOfType<InputManager>().leftMouseInput)
            {
                FindObjectOfType<InputManager>().leftMouseInput = false;
                Instantiate(FloorPrefab, FloorBuild.position, FloorBuild.rotation * Quaternion.Euler(-90,0,0));
            }

            /*            
            Mathf.RoundToInt(Hit.point.x),
            Mathf.RoundToInt(Hit.point.y) + FloorBuild.localScale.y ,
            Mathf.RoundToInt(Hit.point.z));
            */
        }
    }
}
