// GENERATED AUTOMATICALLY FROM 'Assets/Resources/Input System/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player Movement"",
            ""id"": ""bdd75272-0f2f-4cc4-b760-7cc500480391"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""067c8338-d5e0-447c-9c38-77ba46a3bfbd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""43cfc644-e928-4ff6-aa4c-2e55a42ff137"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""7a1a0b02-b39f-443f-a4b9-61e5f34040d7"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""39ba58b5-5149-43fe-be04-62d2c27d9be8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6fedf27c-dbb3-4290-a7e3-591e186ffa88"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""60a319cb-91e4-4ece-900f-63836aba50f6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9b6550ed-fe89-4314-9fc2-20fd9c4017ef"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b31c69a0-da8e-4809-8721-2962bce0c6ae"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player Actions"",
            ""id"": ""0f647c07-e457-4c5c-b323-9a89f9b16f7a"",
            ""actions"": [
                {
                    ""name"": ""Shift"",
                    ""type"": ""Button"",
                    ""id"": ""a4e5a3ac-f392-435d-9190-fa0cc5e11102"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""c9adbbac-95d2-4346-9ce2-2eebc18cb689"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""d73262a6-c872-4b4c-8037-8fcd8d6bc5e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ctrl"",
                    ""type"": ""Button"",
                    ""id"": ""1747b603-5b5c-42f9-9fa1-30c64f4d5e35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""2835b586-4bec-46e8-83b6-6d7a0b44ae35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftMouse"",
                    ""type"": ""Button"",
                    ""id"": ""c4d6f42c-d061-4dd6-a08c-0a81c33e9337"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightMouse"",
                    ""type"": ""Button"",
                    ""id"": ""6b54359c-3b32-4aa5-819a-332eb542ec54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MiddleMouse"",
                    ""type"": ""Button"",
                    ""id"": ""205baed6-83e2-4b7c-aef3-7f1ac8262a46"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4cd17c90-07cc-4849-a3d5-6cee02cd6e87"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c7a48ad-078f-4bd7-a7b5-3d7c500b5ad4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66bbfcb0-94f7-4bc4-b7d7-35bedf6ce5dd"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""507fa2e5-cead-4bd0-8767-7805ee337452"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fad54df1-22cc-4514-b5c3-c428ed9df914"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ctrl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc820a1f-7915-4e14-a09d-a2841b057bd4"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e593b34a-277e-4483-a520-9be7e86f093a"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ceeec385-1e64-49e4-8144-6ee7ef89392c"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MiddleMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""7e7bb465-623f-49c9-9a88-115dc6778d06"",
            ""actions"": [
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""99992af1-b8a6-4fe5-be6a-16d5bc6e846b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f8b2937b-52a7-48d9-a23e-66faccb54937"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Movement
        m_PlayerMovement = asset.FindActionMap("Player Movement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Camera = m_PlayerMovement.FindAction("Camera", throwIfNotFound: true);
        // Player Actions
        m_PlayerActions = asset.FindActionMap("Player Actions", throwIfNotFound: true);
        m_PlayerActions_Shift = m_PlayerActions.FindAction("Shift", throwIfNotFound: true);
        m_PlayerActions_Interact = m_PlayerActions.FindAction("Interact", throwIfNotFound: true);
        m_PlayerActions_Inventory = m_PlayerActions.FindAction("Inventory", throwIfNotFound: true);
        m_PlayerActions_Ctrl = m_PlayerActions.FindAction("Ctrl", throwIfNotFound: true);
        m_PlayerActions_Jump = m_PlayerActions.FindAction("Jump", throwIfNotFound: true);
        m_PlayerActions_LeftMouse = m_PlayerActions.FindAction("LeftMouse", throwIfNotFound: true);
        m_PlayerActions_RightMouse = m_PlayerActions.FindAction("RightMouse", throwIfNotFound: true);
        m_PlayerActions_MiddleMouse = m_PlayerActions.FindAction("MiddleMouse", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_MousePosition = m_UI.FindAction("MousePosition", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player Movement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Camera;
    public struct PlayerMovementActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Camera => m_Wrapper.m_PlayerMovement_Camera;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Camera.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // Player Actions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_Shift;
    private readonly InputAction m_PlayerActions_Interact;
    private readonly InputAction m_PlayerActions_Inventory;
    private readonly InputAction m_PlayerActions_Ctrl;
    private readonly InputAction m_PlayerActions_Jump;
    private readonly InputAction m_PlayerActions_LeftMouse;
    private readonly InputAction m_PlayerActions_RightMouse;
    private readonly InputAction m_PlayerActions_MiddleMouse;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shift => m_Wrapper.m_PlayerActions_Shift;
        public InputAction @Interact => m_Wrapper.m_PlayerActions_Interact;
        public InputAction @Inventory => m_Wrapper.m_PlayerActions_Inventory;
        public InputAction @Ctrl => m_Wrapper.m_PlayerActions_Ctrl;
        public InputAction @Jump => m_Wrapper.m_PlayerActions_Jump;
        public InputAction @LeftMouse => m_Wrapper.m_PlayerActions_LeftMouse;
        public InputAction @RightMouse => m_Wrapper.m_PlayerActions_RightMouse;
        public InputAction @MiddleMouse => m_Wrapper.m_PlayerActions_MiddleMouse;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @Shift.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShift;
                @Shift.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShift;
                @Shift.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShift;
                @Interact.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInteract;
                @Inventory.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInventory;
                @Ctrl.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCtrl;
                @Ctrl.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCtrl;
                @Ctrl.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCtrl;
                @Jump.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @LeftMouse.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLeftMouse;
                @LeftMouse.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLeftMouse;
                @LeftMouse.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLeftMouse;
                @RightMouse.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRightMouse;
                @RightMouse.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRightMouse;
                @RightMouse.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRightMouse;
                @MiddleMouse.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMiddleMouse;
                @MiddleMouse.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMiddleMouse;
                @MiddleMouse.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMiddleMouse;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shift.started += instance.OnShift;
                @Shift.performed += instance.OnShift;
                @Shift.canceled += instance.OnShift;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Ctrl.started += instance.OnCtrl;
                @Ctrl.performed += instance.OnCtrl;
                @Ctrl.canceled += instance.OnCtrl;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @LeftMouse.started += instance.OnLeftMouse;
                @LeftMouse.performed += instance.OnLeftMouse;
                @LeftMouse.canceled += instance.OnLeftMouse;
                @RightMouse.started += instance.OnRightMouse;
                @RightMouse.performed += instance.OnRightMouse;
                @RightMouse.canceled += instance.OnRightMouse;
                @MiddleMouse.started += instance.OnMiddleMouse;
                @MiddleMouse.performed += instance.OnMiddleMouse;
                @MiddleMouse.canceled += instance.OnMiddleMouse;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_MousePosition;
    public struct UIActions
    {
        private @PlayerControls m_Wrapper;
        public UIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePosition => m_Wrapper.m_UI_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @MousePosition.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
    }
    public interface IPlayerActionsActions
    {
        void OnShift(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnCtrl(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLeftMouse(InputAction.CallbackContext context);
        void OnRightMouse(InputAction.CallbackContext context);
        void OnMiddleMouse(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnMousePosition(InputAction.CallbackContext context);
    }
}
