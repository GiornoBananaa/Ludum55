using AudioSystem;
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
        private const string AUDIO_DATA_PATH = "AudioDataSO";
        
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private CinemachineMoving _cineMachineMoving;
        [SerializeField] private TransitionLauncher _transitionLauncher;
        [SerializeField] private SwitchBodyParts _switchBodyParts;
        [SerializeField] private SwitchClothes _switchClothes;
        [SerializeField] private Transform _boxInPackaging;
        [SerializeField] private Transform _boxInSummoning;
        [SerializeField] private TaskView _taskView;
        [SerializeField] private TaskCall _tasksViewTaskCall;
        [SerializeField] private TaskCall _resultTaskCall;
        [SerializeField] private EndResultView _endResultView;
        [SerializeField] private Lighter _lighter;
        [SerializeField] private DraggableItem[] _draggableItemsInBox;
        [SerializeField] private AudioPlayer _audioPlayer;
        [SerializeField] private Mark[] _marks;
        [SerializeField] private Marker[] _markers;
        [SerializeField] private Candle[] _candles;
        [SerializeField] private Tape _tape;
        
        private Game _game;
        private DemonParts _demonParts;
        private TaskGeneration _taskGeneration;
        private GameStatesConstructor _gameStatesConstructor;
        private AudioDataSO _audioDataSo;
        
        private void Awake()
        {
            //-SO-

            _demonParts = Resources.Load<DemonParts>(DEMON_PARTS_DATA_PATH);
            _audioDataSo = Resources.Load<AudioDataSO>(AUDIO_DATA_PATH);
            
            //--
            
            _audioPlayer.Construct(_audioDataSo.GetSounds());
            
            _gameStatesConstructor = new GameStatesConstructor();
            
            _taskGeneration = new TaskGeneration(_taskView,_demonParts);
            _switchBodyParts.Construct(_demonParts,_audioPlayer);
            _switchClothes.Construct(_demonParts,_audioPlayer);
            
            _game = new Game(_gameStatesConstructor.Construct(_switchClothes,_switchBodyParts,
                _boxInPackaging,_boxInSummoning, _taskGeneration,_endResultView,_lighter,_draggableItemsInBox,_tape,_marks));
            _game.ChangeState(GameScreen.DemonBodyChoice);
            _inputListener.Construct();
            _transitionLauncher.Construct(_game, _cineMachineMoving,_audioPlayer);
            _endResultView.Construct(_transitionLauncher);
            foreach (var candle in _candles)
            {
                candle.Construct(_audioPlayer);
            }
            foreach (var marker in _markers)
            {
                marker.Construct(_audioPlayer);
            }
            foreach (var mark in _marks)
            {
                //mark.Construct(_audioPlayer);
            }
            _lighter.Construct(_audioPlayer, _game);
            _tape.Construct(_audioPlayer);
            _resultTaskCall.Construct(_audioPlayer);
            _tasksViewTaskCall.Construct(_audioPlayer);
        }
    }
}
