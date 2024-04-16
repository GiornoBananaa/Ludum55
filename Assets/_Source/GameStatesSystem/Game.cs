using System;
using System.Collections.Generic;
using CameraSystem;
using Core;

namespace GameStatesSystem
{
    public class Game : IStateMachine<GameScreen>
    {
        private Dictionary<GameScreen,GameState> _states;
        private CinemachineMoving _cineMachineMoving;

        public Action<GameScreen> OnGameScreenChanged;
        
        public GameScreen CurrentState { get; private set; }
        
        public Game(Dictionary<GameScreen, GameState> states)
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

        public void Reset()
        {
            foreach (var state in _states.Values)
            {
                state.Reset();
            }
        }
    }
}
