using Core;

namespace GameStatesSystem
{
    public abstract class GameState : IState<GameScreen>
    {
        protected IStateMachine<GameScreen> Owner;
        
        public void SetOwner(IStateMachine<GameScreen> owner)
        {
            Owner = owner;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Reset();
    }
}