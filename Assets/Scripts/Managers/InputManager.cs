using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{    
    PlayerControls playerControls;
    PlayerManager playerManager;
    PlayerLocomotion playerLocomotion;
    PlayerAttacker playerAttacker;
    AnimatorManager animatorManager;
    EquipmentManager equipmentManager;
    StatsManager stats;
    public Vector2 movementInput;
    public Vector2 cameraInput;
    public Vector2 mousePosition;
    public GameObject InventoryWindow;
    public GameObject TooltipCanvas;
    public float cameraInputX;
    public float cameraInputY;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public float scrollInput;
    public bool shiftInput;
    public bool ctrlInput;
    public bool jumpInput;
    public bool leftMouseInput;
    public bool rightMouseInput;
    public bool middleMouseInput;
    public bool interactInput;
    public bool inventoryInput;
    public bool modifierInput;
    public bool confirmInput;
    public bool cancelInput;
    public bool buildInput;
    public float attackChargeTimer = 0f;
    public float blockChargeTimer = 0f;
    public bool inventoryFlag;
    public bool buildFlag;
    public bool buildWindowFlag;
    public bool craftWindowFlag;
    public bool fishingFlag = false;
    public bool hasCast = false;
    public int idleAnim = 0;

    private void Awake() 
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        stats = GetComponent<StatsManager>();
        playerAttacker = GetComponent<PlayerAttacker>();
        playerManager = GetComponent<PlayerManager>();
        equipmentManager = GetComponent<EquipmentManager>();
    }
    private void OnEnable() 
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.UI.MousePosition.performed += i => mousePosition = i.ReadValue<Vector2>();
            playerControls.PlayerActions.MouseWheel.performed += i => scrollInput = i.ReadValue<float>();
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
            playerControls.PlayerActions.Confirm.performed += i => confirmInput = true;
            playerControls.PlayerActions.Confirm.canceled += i => confirmInput = false;
            playerControls.PlayerActions.Cancel.performed += i => cancelInput = true;
            playerControls.PlayerActions.Cancel.canceled += i => cancelInput = false;
            playerControls.PlayerActions.Interact.performed += i => interactInput = true;
            playerControls.PlayerActions.Interact.canceled += i => interactInput = false;
            playerControls.PlayerActions.LeftMouse.performed += i => leftMouseInput = true;
            playerControls.PlayerActions.LeftMouse.canceled += i => leftMouseInput = false;
            playerControls.PlayerActions.RightMouse.performed += i => rightMouseInput = true;
            playerControls.PlayerActions.RightMouse.canceled += i => rightMouseInput = false;
            playerControls.PlayerActions.MiddleMouse.performed += i => middleMouseInput = true;
            playerControls.PlayerActions.MiddleMouse.canceled += i => middleMouseInput = false;
            playerControls.PlayerActions.Build.performed += i => buildInput = true;
            playerControls.PlayerActions.Build.canceled += i => buildInput = false;
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
            HandleFishing();
            
            if(!buildFlag)
                HandleAttackInput();
        }
        
        HandleMouse();
        HandleConfirmButtonInput();
        HandleCancelButtonInput();
        
        if(!buildWindowFlag)
            HandleInventoryInput();
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        
        if(equipmentManager.rightWeapon != null)
        {
            if(equipmentManager.rightWeapon.idleAnim == 1) 
                idleAnim = 1;
            
            if(equipmentManager.rightWeapon.idleAnim == 2) 
                idleAnim = 2;
            
            if(equipmentManager.rightWeapon.idleAnim == 0) 
                idleAnim = 0;
        }
        
        animatorManager.UpdateAnimatorValues(idleAnim, moveAmount, playerLocomotion.isSprinting, playerLocomotion.isSneaking, playerLocomotion.isSwimming);
    }
    private void HandleSprintingInput()
    {
        if(shiftInput && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
            playerLocomotion.isSneaking = false;
        }
        else
            playerLocomotion.isSprinting = false; 
    }

    private void HandleSneakingInput()
    {  
        if(ctrlInput){
            playerLocomotion.isSneaking = !playerLocomotion.isSneaking;   
            ctrlInput = false;
        }
    }

    private void HandleJumpingInput()
    {
        if(jumpInput)
        {
            jumpInput = false;
            playerLocomotion.isSneaking = false;
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
            if(attackChargeTimer < 1f)
                attackChargeTimer += 1f * Time.deltaTime;
            
            if(!leftMouseInput)
            {
                playerAttacker.HandleRangedAction(equipmentManager.rightWeapon);
            }
        }
        else if(blockChargeTimer > 0f)
        {
            if(blockChargeTimer < 1f)
                blockChargeTimer += 1f * Time.deltaTime;
            
            if(!rightMouseInput)
            {
                playerAttacker.HandleEndBlock(equipmentManager.leftWeapon);
            }
            if(leftMouseInput)
            {
                playerAttacker.HandleEndBlock(equipmentManager.leftWeapon);
                if(!equipmentManager.rightWeapon.canCharge)
                {
                    leftMouseInput = false;
                    playerAttacker.HandleLightAttack(equipmentManager.rightWeapon);
                }
            }
        }
        else
        {
            if(equipmentManager.rightWeapon != null)
            {
                if(leftMouseInput)
                {
                    if(!animatorManager.animator.GetBool("isAttacking") 
                        && !animatorManager.animator.GetBool("isInteracting") 
                        && !animatorManager.animator.GetBool("isJumping") )
                    {
                        if(!equipmentManager.rightWeapon.canCharge)
                        {
                            leftMouseInput = false;
                            if(equipmentManager.rightWeapon.projectile != null)
                                playerAttacker.HandleRangedAction(equipmentManager.rightWeapon);
                            else
                                playerAttacker.HandleLightAttack(equipmentManager.rightWeapon);
                        }
                        else if(GameManager.Instance.CheckInventoryForItem(equipmentManager.rightWeapon.projectile.GetComponentInChildren<Projectile>().item, 1, true))
                        {
                            attackChargeTimer += 1f * Time.deltaTime;
                            playerAttacker.HandleChargeAction(equipmentManager.rightWeapon);
                        }
                    }
                }
                if(middleMouseInput)
                {            
                    if(!animatorManager.animator.GetBool("isAttacking") 
                        && !animatorManager.animator.GetBool("isInteracting") 
                        && !animatorManager.animator.GetBool("isJumping") )
                    {
                        middleMouseInput = false;
                        playerAttacker.HandleHeavyAttack(equipmentManager.rightWeapon);
                    }
                }
            }
            if(equipmentManager.leftWeapon != null){
                if(rightMouseInput)
                {            
                    if(!animatorManager.animator.GetBool("isAttacking") 
                        && !animatorManager.animator.GetBool("isInteracting") 
                        && !animatorManager.animator.GetBool("isJumping") )
                    {
                        if(!equipmentManager.leftWeapon.canCharge)
                        {
                            rightMouseInput = false;
                            playerAttacker.HandleLeftAction(equipmentManager.leftWeapon);
                        }
                        else
                        {
                            blockChargeTimer += 1f * Time.deltaTime;
                            playerAttacker.HandleBlockAction(equipmentManager.leftWeapon);
                        }
                    }
                }
            }
        }
    }

    private void HandleMouse()
    {
        if(!inventoryFlag && !buildWindowFlag)
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
    private void HandleConfirmButtonInput()
    {
        if(confirmInput)
        {
            confirmInput = false;
        }
    }
    private void HandleCancelButtonInput()
    {
        if(cancelInput)
        {
            cancelInput = false;
        }
    }

    private void HandleInventoryInput()
    {
        if(inventoryInput && !craftWindowFlag)
        {
            inventoryFlag = !inventoryFlag;
            if(inventoryFlag)
            {
                InventoryWindow.SetActive(true);
                TooltipCanvas.SetActive(true);
            }
            else
            {
                InventoryWindow.SetActive(false);
                TooltipCanvas.SetActive(false);
            }
        }
    }

    private void HandleFishing()
    {
        if(GameManager.Instance.weaponID == 64)
            fishingFlag = true;
        else
            fishingFlag = false;
        
        if(fishingFlag)
        {
            if(leftMouseInput)
            {
                leftMouseInput = false;
                if(!hasCast)
                    playerAttacker.HandleLightAttack(equipmentManager.rightWeapon);
                else
                    playerAttacker.HandleString("FishingEnd", "isAttacking", true, "Sounds/handleSmallLeather2");
                hasCast = !hasCast;
            }
        }
    }
}
