using System.Collections.Generic;
using ArmySystem;
using UnityEngine;

namespace InputSystem
{
    public class InputListener: MonoBehaviour
    {
        private GlobalIInputActions _inputAction;
        
        public void Construct()
        {
            _inputAction = new();
            EnableReadingInput();
        }

        private void Update()
        {
            
        }

        private void EnableReadingInput()
        {
            _inputAction.Enable();
        }
        
        private void DisableReadingInput()
        {
            _inputAction.Disable();
        }
        
        private void OnDestroy()
        {
            DisableReadingInput();
        }
    }
}
