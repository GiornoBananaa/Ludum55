using Core;

namespace GameStatesSystem
{
    public class BodyChoiceGameState : GameState
    {
        private readonly SwitchBodyParts _switchBodyParts;

        public BodyChoiceGameState(SwitchBodyParts switchBodyParts)
        {
            _switchBodyParts = switchBodyParts;
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }

        public override void Reset()
        {
            _switchBodyParts.ResetDisplayImages();
        }
    }
}