// GENERATED AUTOMATICALLY FROM 'Assets/Code/Grid/LevelDesignControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @LevelDesignControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @LevelDesignControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""LevelDesignControls"",
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
    public struct LevelDesignActions
    {
        private @LevelDesignControls m_Wrapper;
        public LevelDesignActions(@LevelDesignControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Position => m_Wrapper.m_LevelDesign_Position;
        public InputAction @Select => m_Wrapper.m_LevelDesign_Select;
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
            }
        }
    }
    public LevelDesignActions @LevelDesign => new LevelDesignActions(this);
    public interface ILevelDesignActions
    {
        void OnPosition(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
}
