using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public BuildSystem build;
    private void Update() 
    {  
        if(FindObjectOfType<GameManager>().weaponID == 15){
            FindObjectOfType<InputManager>().buildFlag = true;
            build.gameObject.SetActive(true);
        }
        else
        {
            FindObjectOfType<InputManager>().buildFlag = false;
            build.gameObject.SetActive(false);   
        }
    }        
}
