using System;
using UnityEngine;
using UnityEngine.UI;

namespace ArmySystem.SummoningSystem
{
    [RequireComponent(typeof(Button))]
    public class SummonButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private MinionType _minionType;
        private MinionSummoner _minionSummoner;

        public void Construct(MinionSummoner minionSummoner)
        {
            _minionSummoner = minionSummoner;
        }
        
        private void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _minionSummoner.AddSummoningClick(_minionType);
        }
    }
}
