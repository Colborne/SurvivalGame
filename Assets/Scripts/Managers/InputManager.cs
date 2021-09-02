using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{    
    PlayerControls playerControls;
    PlayerManager playerManager;
    PlayerLocomotion playerLocomotion;
    PlayerAttacker playerAttacker;
    PlayerInventory playerInventory;
    AnimatorManager animatorManager;
    WeaponSlotManager weaponSlotManager;
    StatsManager stats;
    public Vector2 movementInput;
    public Vector2 cameraInput;
    public Vector2 mousePosition;
    public GameObject InventoryWindow;
    public GameObject EquipmentWindow;
    public float cameraInputX;
    public float cameraInputY;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public bool shiftInput;
    public bool ctrlInput;
    public bool jumpInput;
    public bool leftMouseInput;
    public bool rightMouseInput;
    public bool middleMouseInput;
    public bool interactInput;
    public bool inventoryInput;
    public bool modifierInput;
    public float attackChargeTimer = 0f;

    public bool inventoryFlag;

    private void Awake() 
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        stats = GetComponent<StatsManager>();
        playerAttacker = GetComponent<PlayerAttacker>();
        playerInventory = GetComponent<PlayerInventory>();
        playerManager = GetComponent<PlayerManager>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
    }
    private void OnEnable() 
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.UI.MousePosition.performed += i => mousePosition = i.ReadValue<Vector2>();
            playerControls.PlayerActions.Inventory.performed += i => inventoryInput = true;
            playerControls.PlayerActions.Inventory.canceled += i => inventoryInput = false;
            playerControls.PlayerActions.Shift.performed += i => shiftInput = true;
            playerControls.PlayerActions.Shift.canceled += i => shiftInput = false;
            playerControls.PlayerActions.Shift.performed += i => modifierInput = true;
            playerControls.PlayerActions.Shift.canceled += i => modifierInput = false;
            playerControls.PlayerActions.Jump.performed += i => jumpInput = true;
            playerControls.PlayerActions.Jump.canceled += i => jumpInput = false;
            playerControls.PlayerActions.Ctrl.performed += i => ctrlInput = true;
            playerControls.PlayerActions.Ctrl.canceled += i => ctrlInput = false;
            playerControls.PlayerActions.Interact.performed += i => interactInput = true;
            playerControls.PlayerActions.Interact.canceled += i => interactInput = false;
            playerControls.PlayerActions.LeftMouse.performed += i => leftMouseInput = true;
            playerControls.PlayerActions.LeftMouse.canceled += i => leftMouseInput = false;
            playerControls.PlayerActions.RightMouse.performed += i => rightMouseInput = true;
            playerControls.PlayerActions.RightMouse.canceled += i => rightMouseInput = false;
            playerControls.PlayerActions.MiddleMouse.performed += i => middleMouseInput = true;
            playerControls.PlayerActions.MiddleMouse.canceled += i => middleMouseInput = false;
        }
        playerControls.Enable();
    }

    private void OnDisable() 
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        if(!inventoryFlag){
            HandleMovementInput();
            HandleSprintingInput();
            HandleSneakingInput();
            HandleJumpingInput();
            HandleAttackInput();
    }

        HandleMouse();
        HandleInteractingButtonInput();
        HandleInventoryInput();
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting, playerLocomotion.isSneaking);
    }
    private void HandleSprintingInput()
    {
        if(shiftInput && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    private void HandleSneakingInput()
    {
        
        if(ctrlInput && moveAmount > 0.5f)
        {
            playerLocomotion.isSneaking = true;
        }
        else
        {
            playerLocomotion.isSneaking = false;
        }
        
    }

    private void HandleJumpingInput()
    {
        if(jumpInput)
        {
            jumpInput = false;
            if(!animatorManager.animator.GetBool("isInteracting"))
            {
                playerLocomotion.HandleJumping();
            }
        }
    }

    private void HandleAttackInput()
    {
        if(attackChargeTimer > 0f)
        {
            attackChargeTimer += 1f * Time.deltaTime;
            if(!leftMouseInput)
            {
                attackChargeTimer = 0f;
                playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            }
        }
        else
        {
            if(leftMouseInput)
            {
                if(!animatorManager.animator.GetBool("isAttacking") 
                    && !animatorManager.animator.GetBool("isInteracting") 
                    && !animatorManager.animator.GetBool("isJumping") )
                {
                    if(!playerInventory.rightWeapon.canCharge)
                    {
                        leftMouseInput = false;
                        playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
                    }
                    else
                    {
                        attackChargeTimer += 1f * Time.deltaTime;
                    }
                }
            }
            else if(middleMouseInput)
            {            
                if(!animatorManager.animator.GetBool("isAttacking") 
                    && !animatorManager.animator.GetBool("isInteracting") 
                    && !animatorManager.animator.GetBool("isJumping") )
                {
                    middleMouseInput = false;
                    playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
                }
            }
            else if(rightMouseInput)
            {            
                if(!animatorManager.animator.GetBool("isAttacking") 
                    && !animatorManager.animator.GetBool("isInteracting") 
                    && !animatorManager.animator.GetBool("isJumping") )
                {
                    rightMouseInput = false;
                    playerAttacker.HandleLeftAction(playerInventory.leftWeapon);
                }
            }
        }
    }

    private void HandleMouse()
    {
        if(!inventoryFlag)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void HandleInteractingButtonInput()
    {
        if(interactInput)
        {
            interactInput = false;
        }
    }

    private void HandleInventoryInput()
    {
        if(inventoryInput)
        {
            inventoryFlag = !inventoryFlag;
            if(inventoryFlag){
                InventoryWindow.SetActive(true);
                EquipmentWindow.SetActive(true);
            }
            else
            {
                InventoryWindow.SetActive(false);
                EquipmentWindow.SetActive(false);
            }
        }
    }
}
