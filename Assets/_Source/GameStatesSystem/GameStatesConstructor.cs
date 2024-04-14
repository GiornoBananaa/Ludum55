using System.Collections.Generic;
using Core;
using UnityEngine;

namespace GameStatesSystem
{
    public class GameStatesConstructor : MonoBehaviour
    {
        private Dictionary<GameScreen, GameState> _gameStates;

        public Dictionary<GameScreen, GameState> Construct()
        {
            _gameStates = new Dictionary<GameScreen, GameState>()
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
