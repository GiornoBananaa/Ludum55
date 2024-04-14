using System.Collections.Generic;
using Core;
using UnityEngine;

namespace GameStatesSystem
{
    public class GameStatesConstructor : MonoBehaviour
    {
        private Dictionary<GameScreen, GameState> _gameStates;

        public Dictionary<GameScreen, GameState> Construct(SwitchClothes switchClothes, SwitchBodyParts switchBodyParts)
        {
            _gameStates = new Dictionary<GameScreen, GameState>()
            {
                {GameScreen.DemonBodyChoice, new DemonBodyChoiceGameState(switchBodyParts)},
                {GameScreen.DemonOutfitChoice, new DemonOutfitChoiceGameState(switchClothes,switchBodyParts)},
                {GameScreen.Packaging, new PackagingGameState()},
                {GameScreen.DemonSummoning, new DemonSummoningGameState()},
            };
            
            return _gameStates;
        }
    }
}
