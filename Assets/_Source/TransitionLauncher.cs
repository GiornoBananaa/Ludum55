using System;
using CameraSystem;
using Core;
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
    
    public void Construct(Game game,CinemachineMoving cineMachineMoving)
    {
        _game = game;
        _cineMachineMoving = cineMachineMoving;
    }
    
    private void Start()
    {
        _leftButton.onClick.AddListener(MoveLeft);
        _rightButton.onClick.AddListener(MoveRight);
    }
    
    private void MoveLeft()
    {
        _current--;
        if (_current < 0)
        {
            _current = _gameScreens.Length - 1;
        }
        _cineMachineMoving.MoveLeft();
        _game.ChangeState(_gameScreens[_current]);
    }
    
    private void MoveRight()
    {
        _current++;
        if (_current >= _gameScreens.Length)
        {
            _current = 0;
        }
        _cineMachineMoving.MoveRight();
        _game.ChangeState(_gameScreens[_current]);
    }
}


