//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Character/Controls.inputactions
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

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""3b0583c6-c334-4fa9-9b3c-9149af807b8e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""a89b6105-db52-464c-b4a7-96f84e7d0e37"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a1f4caf2-f470-44f6-bd9b-5de686023fe3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Modifier"",
                    ""type"": ""Button"",
                    ""id"": ""f5134603-52aa-49c2-acd7-ebd6dcf1245a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""Value"",
                    ""id"": ""b11940ce-5f76-4f9e-8f1e-acf894670169"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""a703de0a-d5eb-4bfb-9dd5-27472a78edd9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4b939c73-b492-42f9-87a8-65082fedf5ab"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.3,max=1)"",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""aa5b4f96-9e64-48a2-b421-356e5aa3f466"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4a05da98-f64f-469f-8223-b315ff726e89"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""00a011d1-1375-46e4-afa0-00a6ae48788d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9d5a916c-7e57-40e0-a7fa-ff2976ca4514"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f18e5b40-dbac-4ceb-9874-b62521ea1f56"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ARROW KEYS"",
                    ""id"": ""18d70545-8c36-45a6-ac4f-a3bdc0e14727"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""21f9e7bd-bb98-42d7-bcac-f15bcf0b5699"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""27bfed44-e201-41c0-a2ec-40fb3d3752e2"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b48ed138-4913-40b8-b13b-93691534d2ff"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""013294bd-863d-4afa-a86d-6881e12fc50a"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1d4856d0-1b92-49f6-8068-769886a7421d"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87944871-0d13-4177-91f0-d2b7f50bc9c3"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""270dab67-9059-4bfa-b3cf-8568a46dc47e"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Modifier"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""435b873a-61ec-4c23-9255-409189039756"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Modifier"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f303ef7c-f771-4c2a-8995-0c88ad44756b"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f10c1385-9fc4-461e-b796-0bebcc814285"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0770ba2c-c788-4459-acf7-ecfec906769f"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef02f5d4-74ec-4f5d-addd-5f3567e6f826"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mouse and Keyboard"",
            ""bindingGroup"": ""Mouse and Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Modifier = m_Player.FindAction("Modifier", throwIfNotFound: true);
        m_Player_Camera = m_Player.FindAction("Camera", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Modifier;
    private readonly InputAction m_Player_Camera;
    private readonly InputAction m_Player_Attack;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Modifier => m_Wrapper.m_Player_Modifier;
        public InputAction @Camera => m_Wrapper.m_Player_Camera;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Modifier.started += instance.OnModifier;
            @Modifier.performed += instance.OnModifier;
            @Modifier.canceled += instance.OnModifier;
            @Camera.started += instance.OnCamera;
            @Camera.performed += instance.OnCamera;
            @Camera.canceled += instance.OnCamera;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Modifier.started -= instance.OnModifier;
            @Modifier.performed -= instance.OnModifier;
            @Modifier.canceled -= instance.OnModifier;
            @Camera.started -= instance.OnCamera;
            @Camera.performed -= instance.OnCamera;
            @Camera.canceled -= instance.OnCamera;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    private int m_MouseandKeyboardSchemeIndex = -1;
    public InputControlScheme MouseandKeyboardScheme
    {
        get
        {
            if (m_MouseandKeyboardSchemeIndex == -1) m_MouseandKeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse and Keyboard");
            return asset.controlSchemes[m_MouseandKeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnModifier(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
    }
}
