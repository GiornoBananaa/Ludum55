using Core;

namespace GameStatesSystem
{
    public class DemonBodyChoiceGameState : GameState
    {
        private SwitchBodyParts _switchBodyParts;
        
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