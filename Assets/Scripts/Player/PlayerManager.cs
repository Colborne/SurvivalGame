using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    CameraManager cameraManager;
    PlayerLocomotion playerLocomotion;
    StatsManager statsManager;
    public GameObject interactableUIGameObject;
    public GameObject itemInteractableUIGameObject;
    
    InteractableUI interactableUI;

    public bool isInteracting;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        interactableUI = FindObjectOfType<InteractableUI>();
        statsManager = GetComponent<StatsManager>();
    }

    private void Update() 
    {
        CheckForInteractableObject();
        inputManager.HandleAllInputs();
        statsManager.RegenerateStamina();
    }

    private void FixedUpdate() 
    {
        playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();

        isInteracting = animator.GetBool("isInteracting");   
        playerLocomotion.isJumping = animator.GetBool("isJumping");
        playerLocomotion.isAttacking = animator.GetBool("isAttacking");
        animator.SetBool("isGrounded", playerLocomotion.isGrounded);
        
        inputManager.inventoryInput = false; //do this for all inputs to check once per frame
    }

    private void CheckForInteractableObject()
    {
        RaycastHit hit;

        if(Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 3f))
        {
            if(hit.collider.tag == "Interactable")
            {
                Debug.Log("Ray");
                Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                if(interactableObject != null)
                {
                    string interactableText = interactableObject.interactableText;
                    interactableUI.interactableText.text = interactableText;
                    interactableUIGameObject.SetActive(true);
                
                    if(inputManager.interactInput)
                    {
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
        }
        else
        {
            if(interactableUIGameObject != null)
            {
                interactableUIGameObject.SetActive(false);
            }

            if(itemInteractableUIGameObject != null && inputManager.interactInput)
            {
                itemInteractableUIGameObject.SetActive(false);
            }
        }
    }
}
