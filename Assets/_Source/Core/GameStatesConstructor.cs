using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class GameStatesConstructor : MonoBehaviour
    {
        [Header("DemonBodyChoice")]
        [SerializeField] private CinemachineVirtualCamera _virtualCamera1;
        [SerializeField] private Transform _cameraFollowObject1;
        
        [Header("DemonOutfitChoice")]
        [SerializeField] private CinemachineVirtualCamera _virtualCamera2;
        [SerializeField] private Transform _cameraFollowObject2;
        
        [Header("Packaging")]
        [SerializeField] private CinemachineVirtualCamera _virtualCamera3;
        [SerializeField] private Transform _cameraFollowObject3;
        
        [Header("DemonSummoning")]
        [SerializeField] private CinemachineVirtualCamera _virtualCamera4;
        [SerializeField] private Transform _cameraFollowObject4;
        
        private Dictionary<GameScreen, IState<GameScreen>> _gameStates;

        public Dictionary<GameScreen, IState<GameScreen>> Construct(CinemachineMoving cineMachineMoving)
        {
            _gameStates = new Dictionary<GameScreen, IState<GameScreen>>()
            {
                {GameScreen.DemonBodyChoice, new DemonBodyChoiceGameState(cineMachineMoving,_virtualCamera1,_cameraFollowObject1)},
                {GameScreen.DemonOutfitChoice, new DemonOutfitChoiceGameState(cineMachineMoving,_virtualCamera2,_cameraFollowObject2)},
                {GameScreen.Packaging, new PackagingGameState(cineMachineMoving,_virtualCamera3,_cameraFollowObject3)},
                {GameScreen.DemonSummoning, new DemonSummoningGameState(cineMachineMoving,_virtualCamera4,_cameraFollowObject4)},
            };
            
            return _gameStates;
        }
    }
}
