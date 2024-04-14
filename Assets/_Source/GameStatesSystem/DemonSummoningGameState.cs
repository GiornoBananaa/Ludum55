using Core;

namespace GameStatesSystem
{
    public class DemonSummoningGameState : IState<GameScreen>
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