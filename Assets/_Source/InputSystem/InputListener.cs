using System.Collections.Generic;
using ArmySystem;
using UI;
using UnityEngine;

namespace InputSystem
{
    public class InputListener: MonoBehaviour
    {
        private GlobalIInputActions _inputAction;
        [SerializeField] private TaskCall _taskCall;
        
        public void Construct()
        {
            _inputAction = new();
            EnableReadingInput();
        }

        private void Update()
        {
            CallTask();
        }

        private void EnableReadingInput()
        {
            _inputAction.Enable();
        }
        
        private void DisableReadingInput()
        {
            _inputAction.Disable();
        }
        private void CallTask()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if(!_taskCall.IsOpened)
                    _taskCall.TaskEnable();
                else
                    _taskCall.TaskDisable();
            }
        }

        private void OnDestroy()
        {
            DisableReadingInput();
        }
    }
}
