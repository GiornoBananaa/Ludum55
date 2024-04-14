using System.Collections.Generic;
using ArmySystem;
using UnityEngine;

namespace InputSystem
{
    public class InputListener: MonoBehaviour
    {
        private GlobalIInputActions _inputAction;
        [SerializeField] private TaskCall _taskCall;
        private bool _taskOpen = true;
        
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
            if (Input.GetKeyDown(KeyCode.Tab) & !_taskOpen)
            {
                _taskCall.TaskEnable();
                _taskOpen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Tab) & _taskOpen)
            {
                _taskCall.TaskDisable();
                _taskOpen = false;
            }
        }

        private void OnDestroy()
        {
            DisableReadingInput();
        }
    }
}
