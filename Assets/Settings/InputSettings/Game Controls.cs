//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Settings/InputSettings/Game Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @GameControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Game Controls"",
    ""maps"": [
        {
            ""name"": ""Player Controls"",
            ""id"": ""f04505c2-fdb5-4feb-b193-34e6bb040a89"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""426f1f9f-e715-490c-990e-7c07b1a6cea6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""320ba30a-dbbe-4124-93a2-57fd05b716db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""f7c118b7-1233-429a-8c48-dd1efb4da520"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""ef1c93c6-f9d6-4a7c-9d7b-bd0612f4164f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftMouseHold"",
                    ""type"": ""Button"",
                    ""id"": ""9db7b1bf-d231-4dc3-bd0e-edc3353aa5b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""61fe65cf-a1b5-41d8-ad53-6e324ef71389"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""033e4d4c-a9d3-4666-89a2-47bb4c05f2ee"",
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
                    ""id"": ""469b9f83-3a85-446e-b6b1-0058a7c6ca78"",
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
                    ""id"": ""ea939cf3-0c04-44bd-bd99-e80ae6381206"",
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
                    ""id"": ""fa45d935-acce-4081-a77e-86e7478bc47b"",
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
                    ""id"": ""4de6f948-a3c4-4191-a98c-dc8f31e4e264"",
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
                    ""id"": ""41f11655-f30e-4b63-b2d6-d3fe6c642d76"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c023c1b-eb50-46c5-a584-88d43488792f"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c62e882-a9fa-4b16-af6a-4ab5eaeda107"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftMouseHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera Controls"",
            ""id"": ""a8b890f6-b3cb-4d2f-840d-19886f121e5e"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""279fad7f-b535-4844-9571-461dd230884b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""69b9abab-4993-4c56-9488-a05c8cce8691"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6d534417-b1af-4ca8-8f5a-ee45ca7770f4"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a420904-c6f4-4ffb-b95d-58b25148d7b8"",
                    ""path"": ""<VirtualMouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu Controls"",
            ""id"": ""10dfc408-ad58-4d87-883f-396738d52fad"",
            ""actions"": [
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""c860de1d-8917-4f7a-b353-c8054dc394f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenInventory"",
                    ""type"": ""Button"",
                    ""id"": ""29004df9-5b59-442d-b2f5-ef817e5c9bd1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenSkillTree"",
                    ""type"": ""Button"",
                    ""id"": ""c674c18b-38db-4b42-a4b9-83e6474be65f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenCrafting"",
                    ""type"": ""Button"",
                    ""id"": ""632deb80-b18e-41b1-9401-1f7d052e9d87"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ea91d392-ea94-4bc6-a4ce-de63bd6cecfc"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da677818-fddb-45f0-958c-cf323de0899a"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b104ba75-8d99-446f-9a41-f2de2772526f"",
                    ""path"": ""<Keyboard>/n"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenSkillTree"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c360f31-2055-4845-aeaa-3d86e2bfccae"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCrafting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Combat"",
            ""id"": ""b6baa639-682c-4c1b-a96c-e712fd8c75f1"",
            ""actions"": [
                {
                    ""name"": ""FirstSkill"",
                    ""type"": ""Button"",
                    ""id"": ""e0781768-6235-4f1c-8394-55612e74309f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondSkill"",
                    ""type"": ""Button"",
                    ""id"": ""de3fea22-0b2d-4ed4-8d70-e98828eb2fdf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ThirdSkill"",
                    ""type"": ""Button"",
                    ""id"": ""840f1e1d-6598-416c-a410-459789c3aabb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UltimateSkill"",
                    ""type"": ""Button"",
                    ""id"": ""7c5e47dc-c35f-45de-b7b3-aa0e14d6eb13"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightMouse"",
                    ""type"": ""Button"",
                    ""id"": ""b954df27-659e-4765-ba6b-7e868dcc3c9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftMouse"",
                    ""type"": ""Button"",
                    ""id"": ""1ad3522d-2962-4662-9248-e5f643ac386d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""97d7ef44-8e42-459c-a724-1a6dfd738537"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1254c80f-66bf-428e-b567-2bb1ef28afe7"",
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
                    ""id"": ""f58edf81-6ff7-48fb-b5e1-6c1bca4e5324"",
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
                    ""id"": ""10aca93d-6860-46c9-90c3-d03b59fe6dc0"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68c31c74-a440-4433-a6b9-4a326e73be8a"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThirdSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3009fc98-e131-4897-9d52-1eec76d540a4"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UltimateSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Controls
        m_PlayerControls = asset.FindActionMap("Player Controls", throwIfNotFound: true);
        m_PlayerControls_Movement = m_PlayerControls.FindAction("Movement", throwIfNotFound: true);
        m_PlayerControls_Jump = m_PlayerControls.FindAction("Jump", throwIfNotFound: true);
        m_PlayerControls_Sprint = m_PlayerControls.FindAction("Sprint", throwIfNotFound: true);
        m_PlayerControls_Interact = m_PlayerControls.FindAction("Interact", throwIfNotFound: true);
        m_PlayerControls_LeftMouseHold = m_PlayerControls.FindAction("LeftMouseHold", throwIfNotFound: true);
        // Camera Controls
        m_CameraControls = asset.FindActionMap("Camera Controls", throwIfNotFound: true);
        m_CameraControls_Look = m_CameraControls.FindAction("Look", throwIfNotFound: true);
        m_CameraControls_MousePosition = m_CameraControls.FindAction("MousePosition", throwIfNotFound: true);
        // Menu Controls
        m_MenuControls = asset.FindActionMap("Menu Controls", throwIfNotFound: true);
        m_MenuControls_Escape = m_MenuControls.FindAction("Escape", throwIfNotFound: true);
        m_MenuControls_OpenInventory = m_MenuControls.FindAction("OpenInventory", throwIfNotFound: true);
        m_MenuControls_OpenSkillTree = m_MenuControls.FindAction("OpenSkillTree", throwIfNotFound: true);
        m_MenuControls_OpenCrafting = m_MenuControls.FindAction("OpenCrafting", throwIfNotFound: true);
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_FirstSkill = m_Combat.FindAction("FirstSkill", throwIfNotFound: true);
        m_Combat_SecondSkill = m_Combat.FindAction("SecondSkill", throwIfNotFound: true);
        m_Combat_ThirdSkill = m_Combat.FindAction("ThirdSkill", throwIfNotFound: true);
        m_Combat_UltimateSkill = m_Combat.FindAction("UltimateSkill", throwIfNotFound: true);
        m_Combat_RightMouse = m_Combat.FindAction("RightMouse", throwIfNotFound: true);
        m_Combat_LeftMouse = m_Combat.FindAction("LeftMouse", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player Controls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Movement;
    private readonly InputAction m_PlayerControls_Jump;
    private readonly InputAction m_PlayerControls_Sprint;
    private readonly InputAction m_PlayerControls_Interact;
    private readonly InputAction m_PlayerControls_LeftMouseHold;
    public struct PlayerControlsActions
    {
        private @GameControls m_Wrapper;
        public PlayerControlsActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerControls_Movement;
        public InputAction @Jump => m_Wrapper.m_PlayerControls_Jump;
        public InputAction @Sprint => m_Wrapper.m_PlayerControls_Sprint;
        public InputAction @Interact => m_Wrapper.m_PlayerControls_Interact;
        public InputAction @LeftMouseHold => m_Wrapper.m_PlayerControls_LeftMouseHold;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSprint;
                @Interact.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @LeftMouseHold.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLeftMouseHold;
                @LeftMouseHold.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLeftMouseHold;
                @LeftMouseHold.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLeftMouseHold;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @LeftMouseHold.started += instance.OnLeftMouseHold;
                @LeftMouseHold.performed += instance.OnLeftMouseHold;
                @LeftMouseHold.canceled += instance.OnLeftMouseHold;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);

    // Camera Controls
    private readonly InputActionMap m_CameraControls;
    private ICameraControlsActions m_CameraControlsActionsCallbackInterface;
    private readonly InputAction m_CameraControls_Look;
    private readonly InputAction m_CameraControls_MousePosition;
    public struct CameraControlsActions
    {
        private @GameControls m_Wrapper;
        public CameraControlsActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_CameraControls_Look;
        public InputAction @MousePosition => m_Wrapper.m_CameraControls_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_CameraControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraControlsActions set) { return set.Get(); }
        public void SetCallbacks(ICameraControlsActions instance)
        {
            if (m_Wrapper.m_CameraControlsActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnLook;
                @MousePosition.started -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_CameraControlsActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_CameraControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public CameraControlsActions @CameraControls => new CameraControlsActions(this);

    // Menu Controls
    private readonly InputActionMap m_MenuControls;
    private IMenuControlsActions m_MenuControlsActionsCallbackInterface;
    private readonly InputAction m_MenuControls_Escape;
    private readonly InputAction m_MenuControls_OpenInventory;
    private readonly InputAction m_MenuControls_OpenSkillTree;
    private readonly InputAction m_MenuControls_OpenCrafting;
    public struct MenuControlsActions
    {
        private @GameControls m_Wrapper;
        public MenuControlsActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Escape => m_Wrapper.m_MenuControls_Escape;
        public InputAction @OpenInventory => m_Wrapper.m_MenuControls_OpenInventory;
        public InputAction @OpenSkillTree => m_Wrapper.m_MenuControls_OpenSkillTree;
        public InputAction @OpenCrafting => m_Wrapper.m_MenuControls_OpenCrafting;
        public InputActionMap Get() { return m_Wrapper.m_MenuControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuControlsActions set) { return set.Get(); }
        public void SetCallbacks(IMenuControlsActions instance)
        {
            if (m_Wrapper.m_MenuControlsActionsCallbackInterface != null)
            {
                @Escape.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnEscape;
                @OpenInventory.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnOpenInventory;
                @OpenSkillTree.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnOpenSkillTree;
                @OpenSkillTree.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnOpenSkillTree;
                @OpenSkillTree.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnOpenSkillTree;
                @OpenCrafting.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnOpenCrafting;
                @OpenCrafting.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnOpenCrafting;
                @OpenCrafting.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnOpenCrafting;
            }
            m_Wrapper.m_MenuControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @OpenInventory.started += instance.OnOpenInventory;
                @OpenInventory.performed += instance.OnOpenInventory;
                @OpenInventory.canceled += instance.OnOpenInventory;
                @OpenSkillTree.started += instance.OnOpenSkillTree;
                @OpenSkillTree.performed += instance.OnOpenSkillTree;
                @OpenSkillTree.canceled += instance.OnOpenSkillTree;
                @OpenCrafting.started += instance.OnOpenCrafting;
                @OpenCrafting.performed += instance.OnOpenCrafting;
                @OpenCrafting.canceled += instance.OnOpenCrafting;
            }
        }
    }
    public MenuControlsActions @MenuControls => new MenuControlsActions(this);

    // Combat
    private readonly InputActionMap m_Combat;
    private ICombatActions m_CombatActionsCallbackInterface;
    private readonly InputAction m_Combat_FirstSkill;
    private readonly InputAction m_Combat_SecondSkill;
    private readonly InputAction m_Combat_ThirdSkill;
    private readonly InputAction m_Combat_UltimateSkill;
    private readonly InputAction m_Combat_RightMouse;
    private readonly InputAction m_Combat_LeftMouse;
    public struct CombatActions
    {
        private @GameControls m_Wrapper;
        public CombatActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @FirstSkill => m_Wrapper.m_Combat_FirstSkill;
        public InputAction @SecondSkill => m_Wrapper.m_Combat_SecondSkill;
        public InputAction @ThirdSkill => m_Wrapper.m_Combat_ThirdSkill;
        public InputAction @UltimateSkill => m_Wrapper.m_Combat_UltimateSkill;
        public InputAction @RightMouse => m_Wrapper.m_Combat_RightMouse;
        public InputAction @LeftMouse => m_Wrapper.m_Combat_LeftMouse;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void SetCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterface != null)
            {
                @FirstSkill.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnFirstSkill;
                @FirstSkill.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnFirstSkill;
                @FirstSkill.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnFirstSkill;
                @SecondSkill.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnSecondSkill;
                @SecondSkill.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnSecondSkill;
                @SecondSkill.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnSecondSkill;
                @ThirdSkill.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnThirdSkill;
                @ThirdSkill.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnThirdSkill;
                @ThirdSkill.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnThirdSkill;
                @UltimateSkill.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnUltimateSkill;
                @UltimateSkill.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnUltimateSkill;
                @UltimateSkill.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnUltimateSkill;
                @RightMouse.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnRightMouse;
                @RightMouse.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnRightMouse;
                @RightMouse.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnRightMouse;
                @LeftMouse.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnLeftMouse;
                @LeftMouse.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnLeftMouse;
                @LeftMouse.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnLeftMouse;
            }
            m_Wrapper.m_CombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @FirstSkill.started += instance.OnFirstSkill;
                @FirstSkill.performed += instance.OnFirstSkill;
                @FirstSkill.canceled += instance.OnFirstSkill;
                @SecondSkill.started += instance.OnSecondSkill;
                @SecondSkill.performed += instance.OnSecondSkill;
                @SecondSkill.canceled += instance.OnSecondSkill;
                @ThirdSkill.started += instance.OnThirdSkill;
                @ThirdSkill.performed += instance.OnThirdSkill;
                @ThirdSkill.canceled += instance.OnThirdSkill;
                @UltimateSkill.started += instance.OnUltimateSkill;
                @UltimateSkill.performed += instance.OnUltimateSkill;
                @UltimateSkill.canceled += instance.OnUltimateSkill;
                @RightMouse.started += instance.OnRightMouse;
                @RightMouse.performed += instance.OnRightMouse;
                @RightMouse.canceled += instance.OnRightMouse;
                @LeftMouse.started += instance.OnLeftMouse;
                @LeftMouse.performed += instance.OnLeftMouse;
                @LeftMouse.canceled += instance.OnLeftMouse;
            }
        }
    }
    public CombatActions @Combat => new CombatActions(this);
    public interface IPlayerControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnLeftMouseHold(InputAction.CallbackContext context);
    }
    public interface ICameraControlsActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
    public interface IMenuControlsActions
    {
        void OnEscape(InputAction.CallbackContext context);
        void OnOpenInventory(InputAction.CallbackContext context);
        void OnOpenSkillTree(InputAction.CallbackContext context);
        void OnOpenCrafting(InputAction.CallbackContext context);
    }
    public interface ICombatActions
    {
        void OnFirstSkill(InputAction.CallbackContext context);
        void OnSecondSkill(InputAction.CallbackContext context);
        void OnThirdSkill(InputAction.CallbackContext context);
        void OnUltimateSkill(InputAction.CallbackContext context);
        void OnRightMouse(InputAction.CallbackContext context);
        void OnLeftMouse(InputAction.CallbackContext context);
    }
}
