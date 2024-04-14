using System.Collections.Generic;
using CameraSystem;
using GameStatesSystem;
using InputSystem;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        private const string SUMMONER_DATA_PATH = "SummonerData";
        
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private GameStatesConstructor _gameStatesConstructor;
        [SerializeField] private CinemachineMoving _cineMachineMoving;
        [SerializeField] private TransitionLauncher _transitionLauncher;
        
        private Game _game;
        
        private void Awake()
        {
            //-SO-
            
            //--
            
            _game = new Game(_gameStatesConstructor.Construct());
            _game.ChangeState(GameScreen.DemonBodyChoice);
            _inputListener.Construct();
            _transitionLauncher.Construct(_game, _cineMachineMoving);
        }
    }
}
