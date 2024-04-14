using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class GameStatesConstructor : MonoBehaviour
    {
        private Dictionary<GameScreen, IState<GameScreen>> _gameStates;

        public Dictionary<GameScreen, IState<GameScreen>> Construct()
        {
            _gameStates = new Dictionary<GameScreen, IState<GameScreen>>()
            {
                {GameScreen.DemonBodyChoice, new DemonBodyChoiceGameState()},
                {GameScreen.DemonOutfitChoice, new DemonOutfitChoiceGameState()},
                {GameScreen.Packaging, new PackagingGameState()},
                {GameScreen.DemonSummoning, new DemonSummoningGameState()},
            };
            
            return _gameStates;
        }
    }
}
