using Core;

namespace GameStatesSystem
{
    public abstract class GameState : IState<GameScreen>
    {
        protected Game Owner;
        
        public void SetOwner(IStateMachine<GameScreen> owner)
        {
            Owner = (Game)owner;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Reset();
    }
}