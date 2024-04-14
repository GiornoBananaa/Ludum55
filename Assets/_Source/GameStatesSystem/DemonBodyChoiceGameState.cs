using Core;

namespace GameStatesSystem
{
    public class DemonBodyChoiceGameState : IState<GameScreen>
    {
        private IStateMachine<GameScreen> _owner;
        
        public void SetOwner(IStateMachine<GameScreen> owner)
        {
            _owner = owner;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}