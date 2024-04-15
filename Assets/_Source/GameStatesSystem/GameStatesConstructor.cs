using System.Collections.Generic;
using Core;
using PackagingGame;
using SummoningGame;
using TaskSystem;
using UnityEngine;

namespace GameStatesSystem
{
    public class GameStatesConstructor
    {
        private Dictionary<GameScreen, GameState> _gameStates;

        public Dictionary<GameScreen, GameState> Construct(SwitchClothes switchClothes, SwitchBodyParts switchBodyParts,
            Transform boxInPackaging,Transform boxInSummoning,TaskGeneration taskGeneration, EndResultView endResultView,
            Lighter lighter, DraggableItem[] draggableItemsInBox)
        {
            
            _gameStates = new Dictionary<GameScreen, GameState>()
            {
                {GameScreen.DemonBodyChoice, new BodyChoiceGameState(switchBodyParts)},
                {GameScreen.DemonOutfitChoice, new OutfitChoiceGameState(switchClothes,switchBodyParts)},
                {GameScreen.Packaging, new PackagingGameState(draggableItemsInBox)},
                {GameScreen.DemonSummoning, new DemonSummoningGameState(boxInPackaging,boxInSummoning,lighter)},
                {GameScreen.DemonSummoning, new EndResultGameState(switchBodyParts,switchClothes,endResultView,taskGeneration)},
            };
            
            return _gameStates;
        }
    }
}
