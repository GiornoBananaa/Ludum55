using CameraSystem;
using GameStatesSystem;
using InputSystem;
using PackagingGame;
using SummoningGame;
using TaskSystem;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        private const string DEMON_PARTS_DATA_PATH = "DemonParts";
        
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private CinemachineMoving _cineMachineMoving;
        [SerializeField] private TransitionLauncher _transitionLauncher;
        [SerializeField] private SwitchBodyParts _switchBodyParts;
        [SerializeField] private SwitchClothes _switchClothes;
        [SerializeField] private Transform _boxInPackaging;
        [SerializeField] private Transform _boxInSummoning;
        [SerializeField] private TaskView _taskView;
        [SerializeField] private EndResultView _endResultView;
        [SerializeField] private Lighter _lighter;
        [SerializeField] private DraggableItem[] _draggableItemsInBox;
        
        private Game _game;
        private DemonParts _demonParts;
        private TaskGeneration _taskGeneration;
        private GameStatesConstructor _gameStatesConstructor;
        
        private void Awake()
        {
            //-SO-

            _demonParts = Resources.Load<DemonParts>(DEMON_PARTS_DATA_PATH);
            
            //--
            
            _gameStatesConstructor = new GameStatesConstructor();
            
            _taskGeneration = new TaskGeneration(_taskView,_demonParts);
            _switchBodyParts.Construct(_demonParts);
            _switchClothes.Construct(_demonParts);
            
            _game = new Game(_gameStatesConstructor.Construct(_switchClothes,_switchBodyParts,
                _boxInPackaging,_boxInSummoning, _taskGeneration,_endResultView,_lighter,_draggableItemsInBox));
            _game.ChangeState(GameScreen.DemonBodyChoice);
            _inputListener.Construct();
            _transitionLauncher.Construct(_game, _cineMachineMoving);
            _endResultView.Construct(_game);
        }
    }
}
