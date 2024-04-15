using PackagingGame;
using SummoningGame;
using UnityEngine;

namespace GameStatesSystem
{
    public class DemonSummoningGameState : GameState
    {
        private Transform _boxInPackaging;
        private Transform _boxInSummoning;
        private GameObject _boxCopy;
        private Lighter _lighter;

        public DemonSummoningGameState(Transform boxInPackaging,Transform boxInSummoning, Lighter lighter)
        {
            _boxInPackaging = boxInPackaging;
            _boxInSummoning = boxInSummoning;
            _lighter = lighter;
        }
        
        public override void Enter()
        {
            if(_boxCopy != null)
                Object.Destroy(_boxCopy);
            _boxCopy = Object.Instantiate(_boxInPackaging.gameObject, _boxInSummoning.position,_boxInSummoning.rotation,_boxInSummoning.parent);
            _boxCopy.transform.localScale = _boxInSummoning.localScale;
            _lighter.SetBox(_boxCopy);
            /*foreach (Transform child in _boxInSummoning)
            {
                Object.Destroy(child.gameObject);
            }*/
            foreach (Transform child in _boxCopy.transform)
            {
                if (child.TryGetComponent(out LineRenderer line))
                {
                    line.widthMultiplier = line.widthMultiplier * child.transform.lossyScale.x/child.lossyScale.x;
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