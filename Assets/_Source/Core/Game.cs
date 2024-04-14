using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Core
{
    public enum GameScreen
    {
        DemonBodyChoice = 0,
        DemonOutfitChoice = 1,
        Packaging = 2,
        DemonSummoning = 3,
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
            if(_states.ContainsKey(CurrentState))
                _states[CurrentState].Exit();
            CurrentState = state;
            _states[CurrentState].Enter();
        }
    }
    
    public class DemonBodyChoiceGameState : IState<GameScreen>
    {
        private readonly CinemachineMoving _cineMachineMoving;
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly Transform _cameraFollowObject;
        private IStateMachine<GameScreen> _owner;

        public DemonBodyChoiceGameState(CinemachineMoving cineMachineMoving,CinemachineVirtualCamera virtualCamera, Transform cameraFollowObject)
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
    
    public class DemonOutfitChoiceGameState : IState<GameScreen>
    {
        private readonly CinemachineMoving _cineMachineMoving;
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly Transform _cameraFollowObject;
        private IStateMachine<GameScreen> _owner;

        public DemonOutfitChoiceGameState(CinemachineMoving cineMachineMoving,CinemachineVirtualCamera virtualCamera, Transform cameraFollowObject)
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
    
    public class PackagingGameState : IState<GameScreen>
    {
        private readonly CinemachineMoving _cineMachineMoving;
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly Transform _cameraFollowObject;
        private IStateMachine<GameScreen> _owner;

        public PackagingGameState(CinemachineMoving cineMachineMoving,CinemachineVirtualCamera virtualCamera, Transform cameraFollowObject)
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
    
    public class DemonSummoningGameState : IState<GameScreen>
    {
        private readonly CinemachineMoving _cineMachineMoving;
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly Transform _cameraFollowObject;
        private IStateMachine<GameScreen> _owner;

        public DemonSummoningGameState(CinemachineMoving cineMachineMoving,CinemachineVirtualCamera virtualCamera, Transform cameraFollowObject)
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
