using Core;
using PackagingGame;

namespace GameStatesSystem
{
    public class PackagingGameState : IState<GameScreen>
    {
        private IStateMachine<GameScreen> _owner;
        private TapePutter _tapePutter;//TODO Save tapePutter
        
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