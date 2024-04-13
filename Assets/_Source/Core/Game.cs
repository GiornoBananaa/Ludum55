using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Core
{
    public enum GameScreen
    {
        DemonBodyGathering = 1,
        DemonOutfitGathering = 2,
        BoxingGame = 3,
        DemonSummoning = 4,
    }
    
    public class Game : IStateMachine<GameScreen>
    {
        private Dictionary<GameScreen,IState<GameScreen>> _states;
        
        public GameScreen CurrentState { get; private set; }
        
        public Game(Dictionary<GameScreen, IState<GameScreen>> states)
        {
            _states = states;
        }
        
        public void ChangeState(GameScreen state)
        {
            _states[CurrentState]?.Exit();
            CurrentState = state;
            _states[CurrentState].Enter();
        }
    }
    
    public class DemonGatheringGameState : IState<GameScreen>
    {
        private readonly CinemachineMoving _cineMachineMoving;
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly Transform _cameraFollowObject;
        private IStateMachine<GameScreen> _owner;

        public DemonGatheringGameState(CinemachineMoving cineMachineMoving,CinemachineVirtualCamera virtualCamera, Transform cameraFollowObject)
        {
            _cineMachineMoving = cineMachineMoving;
            _virtualCamera = virtualCamera;
            _cameraFollowObject = cameraFollowObject;
        }
        
        public void SetOwner(IStateMachine<GameScreen> owner)
        {
            _owner = owner;
        }

        public void Enter()
        {
            _cineMachineMoving.SwitchCamera(_virtualCamera,_cameraFollowObject);
        }

        public void Exit()
        {
            
        }
    }
    
    public class BoxingGameState : IState<GameScreen>
    {
        private readonly CinemachineMoving _cineMachineMoving;
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly Transform _cameraFollowObject;
        private IStateMachine<GameScreen> _owner;

        public BoxingGameState(CinemachineMoving cineMachineMoving,CinemachineVirtualCamera virtualCamera, Transform cameraFollowObject)
        {
            _cineMachineMoving = cineMachineMoving;
            _virtualCamera = virtualCamera;
            _cameraFollowObject = cameraFollowObject;
        }
        
        public void SetOwner(IStateMachine<GameScreen> owner)
        {
            _owner = owner;
        }

        public void Enter()
        {
            _cineMachineMoving.SwitchCamera(_virtualCamera,_cameraFollowObject);
        }

        public void Exit()
        {
            
        }
    }
}
