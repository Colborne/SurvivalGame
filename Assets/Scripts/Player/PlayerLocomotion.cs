using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;
    StatsManager stats;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;
    
    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public LayerMask groundLayer;

    [Header("Movement Flags")]

    public bool isSprinting;
    public bool isSneaking;
    public bool isGrounded;
    public bool isJumping;
    public bool isAttacking;
    public bool isFalling;

    [Header("Movement Speeds")]
    public float sneakingSpeed = 1.5f;
    public float walkingSpeed = 2.5f;
    public float runningSpeed = 5;
    public float sprintingSpeed = 7;
    public float rotationSpeed = 15;

    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        stats = GetComponent<StatsManager>();
        cameraObject = Camera.main.transform;
    }
    public void HandleAllMovement()
    {
        HandleFallingAndLanding();

        if(inputManager.inventoryFlag)
            return;
        
        if(playerManager.isInteracting && inputManager.attackChargeTimer == 0f && inputManager.blockChargeTimer == 0f)
            return;

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if(isJumping)
            return;
        if(isFalling)
            return;

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if(isSprinting && stats.currentStamina >= .5f)
        {
            stats.UseStamina(.5f);
            moveDirection = moveDirection * sprintingSpeed * (((1000f - stats.inventoryWeight) / 1000f) + .1f);
        }
        else if(isSneaking)
        {
            moveDirection = moveDirection * sneakingSpeed * (((1000f - stats.inventoryWeight) / 1000f) + .1f);
        }
        else
        {
            if(inputManager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * runningSpeed * (((1000f- stats.inventoryWeight) / 1000f) + .1f);
            }        
            else
            {
                moveDirection = moveDirection * walkingSpeed * (((1000f - stats.inventoryWeight) / 1000f) + .1f);
            }
        }

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        if(isJumping)
            return;
        if(isFalling)
            return;


        if(inputManager.attackChargeTimer == 0f)
        {
            Vector3 targetDirection = Vector3.zero;
            targetDirection = cameraObject.forward * inputManager.verticalInput;
            targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0;

            if(targetDirection == Vector3.zero)
                targetDirection = transform.forward;
            
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(inputManager.mousePosition);
            var look = ray.direction;
            look.y = 0;
            transform.rotation = Quaternion.LookRotation(look);
        }
    }
    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + 1f;
        Vector3 targetPosition = transform.position;

        if(!isGrounded && !isJumping)
        {
            if(!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }
       
        if(Physics.SphereCast(rayCastOrigin, .1f, -Vector3.up, out hit, 2f, groundLayer))
        {
            if(!isGrounded && !playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Landing", true);

            }

            Vector3 rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y;
            inAirTimer = 0;
            isGrounded = true;
        } 
        else
        {
            isGrounded = false;
        } 

        if(isGrounded && !isJumping )
        {
            isFalling = false;
            if(playerManager.isInteracting || inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
        }    
        isFalling = !isGrounded;
    }

    public void HandleJumping()
    {
        if(isGrounded)
        {
            if(stats.currentStamina >= 5)
            {
                stats.UseStamina(5);
                animatorManager.animator.SetBool("isJumping", true);
                animatorManager.PlayTargetAnimation("Jumping", true);
                
                float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
                Vector3 playerVelocity = moveDirection;
                playerVelocity.y = jumpingVelocity;
                playerRigidbody.velocity = playerVelocity;
            }
        }
    }
}
