using Core;

namespace GameStatesSystem
{
    public class DemonBodyChoiceGameState : GameState
    {
        private readonly SwitchBodyParts _switchBodyParts;

        public DemonBodyChoiceGameState(SwitchBodyParts switchBodyParts)
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