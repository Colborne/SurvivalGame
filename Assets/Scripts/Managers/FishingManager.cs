using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{    
    AnimatorManager animatorManager;
    GameManager gm;
    InputManager input;
    public GameObject cast;
    public GameObject[] fish;

    public float fishingTimer = 0f;
    public float caught;
    bool anim = false;

    private void Awake() 
    {
        gm = FindObjectOfType<GameManager>();
        input = FindObjectOfType<InputManager>();
        animatorManager = FindObjectOfType<AnimatorManager>();
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

        if(fishingTimer >= caught && !anim)
        {
            animatorManager.PlayTargetAnimation("FishOnTheLine", true);
            anim = true;
        }
        
        if(input.leftMouseInput)
        {
            if(fishingTimer >= caught && fishingTimer <= caught + 2f)
            {
                var _catch = Instantiate(fish[Random.Range(0, fish.Length)], cast.transform.position, Random.rotation);
                Rigidbody rb = _catch.GetComponentInChildren<Rigidbody>();
                rb.AddForce(transform.up * 1500f);
            }    
            caught = Random.Range(1f,8f);
            anim = false;
            fishingTimer = 0f;
        }

        if(input.hasCast)
        {
            if(cast.transform.position.y < -2f)
                fishingTimer += Time.deltaTime;
        }
    }
}        