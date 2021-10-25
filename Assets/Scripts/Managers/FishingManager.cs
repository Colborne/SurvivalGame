using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{    
    public GameObject cast;
    public GameObject[] fish;
    GameManager gm;
    InputManager input;
    public float fishingTimer = 0f;
    public float caught;

    private void Awake() 
    {
        gm = FindObjectOfType<GameManager>();
        input = FindObjectOfType<InputManager>();
        caught = Random.Range(1f,8f);
    }
    private void Update() 
    {  
        if(gm.weaponID == 64){
            cast.gameObject.SetActive(true);
            cast.transform.position = transform.position + transform.forward * 3f;
        }
        else
        {
            cast.gameObject.SetActive(false);   
        }

        if(input.leftMouseInput)
        {
            if(fishingTimer >= caught)
            {
                if(fishingTimer <= caught + 2f)
                {
                    var _catch = Instantiate(fish[Random.Range(0, fish.Length)], cast.transform.position + new Vector3(0,1,0), Random.rotation);
                    Rigidbody rb = _catch.GetComponentInChildren<Rigidbody>();
                    _catch.transform.LookAt(transform.position);
                    rb.AddForce(transform.up * 1000f);
                }
            }
            caught = Random.Range(1f,8f);
        }

        if(input.hasCast)
        {
            if(cast.transform.position.y < -2f)
                fishingTimer += Time.deltaTime;
        }
        else
            fishingTimer = 0f;
    }
}        