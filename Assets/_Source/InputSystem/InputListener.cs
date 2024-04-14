using System.Collections.Generic;
using ArmySystem;
using UnityEngine;

namespace InputSystem
{
    public class InputListener: MonoBehaviour
    {
        private GlobalIInputActions _inputAction;
        private  MinionSummoner _minionSummoner;
        private  Dictionary<KeyCode, MinionType> _summoningMinionTypesByKeyCodes;
        
        public void Construct(MinionSummoner minionSummoner, Dictionary<KeyCode, MinionType> summoningMinionTypesByKeyCodes)
        {
            _inputAction = new();
            _summoningMinionTypesByKeyCodes = summoningMinionTypesByKeyCodes;
            _minionSummoner = minionSummoner;
            EnableReadingInput();
        }

        private void Update()
        {
            ReadMinionSummoning();
        }

        private void EnableReadingInput()
        {
            _inputAction.Enable();
        }
        
        private void DisableReadingInput()
        {
            _inputAction.Disable();
        }
        
        private void ReadMinionSummoning()
        {
            foreach (var typeByKeyCode in _summoningMinionTypesByKeyCodes)
            {
                if (Input.GetKeyDown(typeByKeyCode.Key))
                {
                    _minionSummoner.AddSummoningClick(typeByKeyCode.Value);
                }
            }
        }

        private void OnDestroy()
        {
            DisableReadingInput();
        }
    }
}
