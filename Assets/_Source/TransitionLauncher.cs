using System;
using AudioSystem;
using CameraSystem;
using Core;
using DG.Tweening;
using GameStatesSystem;
using UnityEngine;
using UnityEngine.UI;

public class TransitionLauncher : MonoBehaviour
{
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    
    private CinemachineMoving _cineMachineMoving;
    private Game _game;
    private int _current;
    private GameScreen[] _gameScreens = (GameScreen[])Enum.GetValues(typeof(GameScreen));
    private AudioPlayer _audioPlayer;
    
    public void Construct(Game game,CinemachineMoving cineMachineMoving,AudioPlayer audioPlayer)
    {
        _audioPlayer = audioPlayer;
        _game = game;
        _cineMachineMoving = cineMachineMoving;
    }
    
    private void Start()
    {
        _leftButton.onClick.AddListener(MoveLeft);
        _rightButton.onClick.AddListener(MoveRight);
    }
    
    public void MoveLeft()
    {
        if(_cineMachineMoving.IsMoving) 
            return;
            
        _current--;
        if (_current < 0)
        {
            _current = _gameScreens.Length - 2;
        }
        _audioPlayer.Play(Sounds.ScreenTransition);
        _cineMachineMoving.MoveLeft().onComplete += () => _game.OnGameScreenChanged?.Invoke(_gameScreens[_current]);
        _game.ChangeState(_gameScreens[_current]);
    }
    
    public void MoveRight()
    {
        if(_cineMachineMoving.IsMoving) 
            return;
        
        _current++;
        if (_current >= _gameScreens.Length - 1)
        {
            _current = 0;
        }
        _audioPlayer.Play(Sounds.ScreenTransition);
        _cineMachineMoving.MoveRight().onComplete += () => _game.OnGameScreenChanged?.Invoke(_gameScreens[_current]);
        _game.ChangeState(_gameScreens[_current]);
    }

    public void MoveStart()
    {
        for (int i = 0; i < _gameScreens.Length-1; i++)
        {
            if (_gameScreens[i] == GameScreen.DemonBodyChoice)
            {
                _current = i;
                break;
            }
        }
        
        _audioPlayer.Play(Sounds.ScreenTransition);
        _cineMachineMoving.MoveStart().onComplete += () => _game.OnGameScreenChanged?.Invoke(_gameScreens[_current]);
        _game.ChangeState(_gameScreens[_current]);
    }
}


