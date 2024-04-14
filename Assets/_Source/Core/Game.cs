using System;
using System.Collections.Generic;
using CameraSystem;
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
        private CinemachineMoving _cineMachineMoving;
        
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
            _states[CurrentState].SetOwner(this);
            _states[CurrentState].Enter();
        }
    }
    
    public class DemonBodyChoiceGameState : IState<GameScreen>
    {
        private IStateMachine<GameScreen> _owner;
        
        public void SetOwner(IStateMachine<GameScreen> owner)
        {
            _owner = owner;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
    
    public class DemonOutfitChoiceGameState : IState<GameScreen>
    {
        private IStateMachine<GameScreen> _owner;
        
        public void SetOwner(IStateMachine<GameScreen> owner)
        {
            _owner = owner;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
    
    public class PackagingGameState : IState<GameScreen>
    {
        private IStateMachine<GameScreen> _owner;
        
        public void SetOwner(IStateMachine<GameScreen> owner)
        {
            _owner = owner;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
    
    public class DemonSummoningGameState : IState<GameScreen>
    {
        private IStateMachine<GameScreen> _owner;
        
        public void SetOwner(IStateMachine<GameScreen> owner)
        {
            _owner = owner;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}
