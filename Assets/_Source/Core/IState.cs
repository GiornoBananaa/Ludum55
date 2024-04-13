namespace Core
{
    public interface IState<T>
    {
        void SetOwner(IStateMachine<T> owner);
        void Enter();
        void Exit();
    }
}