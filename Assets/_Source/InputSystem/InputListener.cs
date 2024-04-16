using System.Collections.Generic;
using ArmySystem;
using UI;
using UnityEngine;

namespace InputSystem
{
    public class InputListener: MonoBehaviour
    {
        private TransitionLauncher _transitionLauncher;
        [SerializeField] private TaskCall _taskCall;
        
        public void Construct(TransitionLauncher transitionLauncher)
        {
            _transitionLauncher = transitionLauncher;
        }

        private void Update()
        {
            CallTask();
            MoveScreen();
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
        
        private void MoveScreen()
        {
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            if (horizontalAxis<0)
            {
                _transitionLauncher.MoveLeft();
            }
            else if (horizontalAxis>0)
            {
                _transitionLauncher.MoveRight();
            }
        }
    }
}
