using Core;

namespace GameStatesSystem
{
    public class OutfitChoiceGameState : GameState
    {
        private readonly SwitchClothes _switchClothes;
        private readonly SwitchBodyParts _switchBodyParts;

        public OutfitChoiceGameState(SwitchClothes switchClothes, SwitchBodyParts switchBodyParts)
        {
            _switchClothes = switchClothes;
            _switchBodyParts = switchBodyParts;
        }
        
        public override void Enter()
        {
            _switchClothes.SetBodyParts(_switchBodyParts.ImageHead,_switchBodyParts.ImageBody,_switchBodyParts.ImageLegs);
        }

        public override void Exit()
        {
            
        }

        public override void Reset()
        {
            _switchClothes.ResetDisplayImages();
        }
    }
}