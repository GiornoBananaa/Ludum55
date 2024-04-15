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
        [SerializeField] private SwitchBodyParts _switchBodyParts;
        [SerializeField] private SwitchClothes _switchClothes;
        [SerializeField] private Transform _boxInPackaging;
        [SerializeField] private Transform _boxInSummoning;
        
        private Game _game;
        
        private void Awake()
        {
            //-SO-
            
            //--
            
            _game = new Game(_gameStatesConstructor.Construct(_switchClothes,_switchBodyParts,_boxInPackaging,_boxInSummoning));
            _game.ChangeState(GameScreen.DemonBodyChoice);
            _inputListener.Construct();
            _transitionLauncher.Construct(_game, _cineMachineMoving);
        }
    }
}
