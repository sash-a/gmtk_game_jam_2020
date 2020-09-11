// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""LevelDesign"",
            ""id"": ""912f5c18-f35e-43ec-8a43-3e33aeec78ef"",
            ""actions"": [
                {
                    ""name"": ""Position"",
                    ""type"": ""Value"",
                    ""id"": ""c2747272-8ed3-480a-9d76-745c15a79a26"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""4819140c-2931-4cf6-926f-2c32a53afc3d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""72663f25-ef29-49bf-bbc9-f112138562a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e27ef067-d652-4d06-ba08-65faaec59b58"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a40bf02-6ba7-4865-a602-948ee2c4d640"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43ec607c-769f-4119-be39-f745740875f1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""a9512c22-fbe3-400d-b364-f9075ec98110"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""291ab6aa-30f1-47e8-afe2-57837e53a2d7"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c1732318-a803-4d0c-88a7-c9a5dbdb3bd2"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Player"",
            ""id"": ""04df905c-d9c6-41ec-a43e-941b644e7208"",
            ""actions"": [
                {
                    ""name"": ""Split"",
                    ""type"": ""Button"",
                    ""id"": ""7a627f9d-aad1-493f-9f9c-4b9bef97c77d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""9add0b23-510d-4371-887e-5cd294eeb38b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""15adae4f-f476-4ece-b07b-1029d0140b4b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""928b0b92-d10f-4907-a1e1-34fb89398ada"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Split"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86bfb745-239f-4077-a78a-98a6157edde9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Horizontal"",
                    ""id"": ""64987e76-b31c-4538-a520-4876a86f51dd"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""58788e43-8bf7-4dbc-872d-11ac67c80f45"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6074331b-2b5c-4717-8869-782a9fdfa39b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // LevelDesign
        m_LevelDesign = asset.FindActionMap("LevelDesign", throwIfNotFound: true);
        m_LevelDesign_Position = m_LevelDesign.FindAction("Position", throwIfNotFound: true);
        m_LevelDesign_Select = m_LevelDesign.FindAction("Select", throwIfNotFound: true);
        m_LevelDesign_Zoom = m_LevelDesign.FindAction("Zoom", throwIfNotFound: true);
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Split = m_Player.FindAction("Split", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
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

    // LevelDesign
    private readonly InputActionMap m_LevelDesign;
    private ILevelDesignActions m_LevelDesignActionsCallbackInterface;
    private readonly InputAction m_LevelDesign_Position;
    private readonly InputAction m_LevelDesign_Select;
    private readonly InputAction m_LevelDesign_Zoom;
    public struct LevelDesignActions
    {
        private @Controls m_Wrapper;
        public LevelDesignActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Position => m_Wrapper.m_LevelDesign_Position;
        public InputAction @Select => m_Wrapper.m_LevelDesign_Select;
        public InputAction @Zoom => m_Wrapper.m_LevelDesign_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_LevelDesign; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LevelDesignActions set) { return set.Get(); }
        public void SetCallbacks(ILevelDesignActions instance)
        {
            if (m_Wrapper.m_LevelDesignActionsCallbackInterface != null)
            {
                @Position.started -= m_Wrapper.m_LevelDesignActionsCallbackInterface.OnPosition;
                @Position.performed -= m_Wrapper.m_LevelDesignActionsCallbackInterface.OnPosition;
                @Position.canceled -= m_Wrapper.m_LevelDesignActionsCallbackInterface.OnPosition;
                @Select.started -= m_Wrapper.m_LevelDesignActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_LevelDesignActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_LevelDesignActionsCallbackInterface.OnSelect;
                @Zoom.started -= m_Wrapper.m_LevelDesignActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_LevelDesignActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_LevelDesignActionsCallbackInterface.OnZoom;
            }
            m_Wrapper.m_LevelDesignActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Position.started += instance.OnPosition;
                @Position.performed += instance.OnPosition;
                @Position.canceled += instance.OnPosition;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
            }
        }
    }
    public LevelDesignActions @LevelDesign => new LevelDesignActions(this);

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Split;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Move;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Split => m_Wrapper.m_Player_Split;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Split.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSplit;
                @Split.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSplit;
                @Split.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSplit;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Split.started += instance.OnSplit;
                @Split.performed += instance.OnSplit;
                @Split.canceled += instance.OnSplit;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface ILevelDesignActions
    {
        void OnPosition(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
    public interface IPlayerActions
    {
        void OnSplit(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
}
