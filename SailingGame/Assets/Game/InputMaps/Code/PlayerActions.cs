// GENERATED AUTOMATICALLY FROM 'Assets/Game/InputMaps/PlayerActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Wokarol.Input
{
    public class @PlayerActions : IInputActionCollection, IDisposable
    {
        private InputActionAsset asset;
        public @PlayerActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""1512b5a0-9fdd-43ab-a2ae-878094c3936a"",
            ""actions"": [
                {
                    ""name"": ""Direction"",
                    ""type"": ""Button"",
                    ""id"": ""361ac12b-4210-408b-b52b-b8aeea65fa48"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DirectionByPointer"",
                    ""type"": ""Button"",
                    ""id"": ""9cec0931-2f3d-4ee0-bdce-fd37fb6d8a51"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5d1799ef-b76b-41c9-bb00-a7b21cd7393a"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d76c89c-7602-4738-88dd-f8609f5468c7"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""DirectionByPointer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
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
            // Main
            m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
            m_Main_Direction = m_Main.FindAction("Direction", throwIfNotFound: true);
            m_Main_DirectionByPointer = m_Main.FindAction("DirectionByPointer", throwIfNotFound: true);
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

        // Main
        private readonly InputActionMap m_Main;
        private IMainActions m_MainActionsCallbackInterface;
        private readonly InputAction m_Main_Direction;
        private readonly InputAction m_Main_DirectionByPointer;
        public struct MainActions
        {
            private @PlayerActions m_Wrapper;
            public MainActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Direction => m_Wrapper.m_Main_Direction;
            public InputAction @DirectionByPointer => m_Wrapper.m_Main_DirectionByPointer;
            public InputActionMap Get() { return m_Wrapper.m_Main; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
            public void SetCallbacks(IMainActions instance)
            {
                if (m_Wrapper.m_MainActionsCallbackInterface != null)
                {
                    @Direction.started -= m_Wrapper.m_MainActionsCallbackInterface.OnDirection;
                    @Direction.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnDirection;
                    @Direction.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnDirection;
                    @DirectionByPointer.started -= m_Wrapper.m_MainActionsCallbackInterface.OnDirectionByPointer;
                    @DirectionByPointer.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnDirectionByPointer;
                    @DirectionByPointer.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnDirectionByPointer;
                }
                m_Wrapper.m_MainActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Direction.started += instance.OnDirection;
                    @Direction.performed += instance.OnDirection;
                    @Direction.canceled += instance.OnDirection;
                    @DirectionByPointer.started += instance.OnDirectionByPointer;
                    @DirectionByPointer.performed += instance.OnDirectionByPointer;
                    @DirectionByPointer.canceled += instance.OnDirectionByPointer;
                }
            }
        }
        public MainActions @Main => new MainActions(this);
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        public interface IMainActions
        {
            void OnDirection(InputAction.CallbackContext context);
            void OnDirectionByPointer(InputAction.CallbackContext context);
        }
    }
}
