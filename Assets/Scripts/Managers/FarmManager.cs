using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public FarmSystem farm;
    private void Update() 
    {  
        if(FindObjectOfType<GameManager>().weaponID == 177){
            FindObjectOfType<InputManager>().farmFlag = true;
            farm.gameObject.SetActive(true);
        }
        else
        {
            FindObjectOfType<InputManager>().farmFlag = false;
            farm.gameObject.SetActive(false);   
        }
    }        
}
