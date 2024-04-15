using SummoningGame;
using UnityEngine;

namespace GameStatesSystem
{
    public class DemonSummoningGameState : GameState
    {
        private Transform _boxInPackaging;
        private Transform _boxInSummoning;
        private Lighter _lighter;

        public DemonSummoningGameState(Transform boxInPackaging,Transform boxInSummoning, Lighter lighter)
        {
            _boxInPackaging = boxInPackaging;
            _boxInSummoning = boxInSummoning;
            _lighter = lighter;
        }
        
        public override void Enter()
        {
            foreach (Transform child in _boxInSummoning)
            {
                Object.Destroy(child.gameObject);
            }
            foreach (Transform child in _boxInPackaging)
            {
                GameObject copy = Object.Instantiate(child.gameObject, _boxInSummoning, false);
                if (copy.TryGetComponent(out LineRenderer line))
                {
                    line.widthMultiplier = line.widthMultiplier * copy.transform.lossyScale.x/child.lossyScale.x;
                }
            }
        }

        public override void Exit()
        {
            
        }

        public override void Reset()
        {
            _lighter.Reset();
            foreach (Transform child in _boxInPackaging)
            {
                if(child.TryGetComponent(out LineRenderer line))
                    Object.Destroy(line.gameObject);
            }
        }
    }
}