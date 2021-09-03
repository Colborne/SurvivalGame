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

    public Transform rightHand;
    public Transform leftHand;
    public Transform helmet;
    public Transform chest;
    public Transform legs;
    public Transform boots;
    public Transform back;
    public Transform body;

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
}
