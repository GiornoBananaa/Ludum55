using System;
using ArmySystem;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class TransitionLauncher : MonoBehaviour
{
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private MinionType _minionType;
    private Game _game;
    private int _current = 0;
    private GameScreen[] _gameScreens = (GameScreen[])Enum.GetValues(typeof(GameScreen));
    
    public void Construct(Game game)
    {
        _game = game;
    }
    
    private void Start()
    {
        _leftButton.onClick.AddListener(OnLeftArrowClick);
        _rightButton.onClick.AddListener(OnRightArrowClick);
    }
    
    private void OnLeftArrowClick()
    {
        _current--;
        if (_current < 0)
            _current = _gameScreens.Length-1;
        _game.ChangeState(_gameScreens[_current]);
    }
    
    private void OnRightArrowClick()
    {
        _current++;
        if (_current >= _gameScreens.Length)
            _current = 0;
        
        _game.ChangeState(_gameScreens[_current]);
    }
}


