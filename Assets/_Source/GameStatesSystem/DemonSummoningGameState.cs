using UnityEngine;

namespace GameStatesSystem
{
    public class DemonSummoningGameState : GameState
    {
        private Transform _boxInPackaging;
        private Transform _boxInSummoning;

        public DemonSummoningGameState(Transform boxInPackaging,Transform boxInSummoning)
        {
            _boxInPackaging = boxInPackaging;
            _boxInSummoning = boxInSummoning;
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
            
        }
    }
}