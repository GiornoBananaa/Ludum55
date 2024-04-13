namespace Core
{
    public interface IStateMachine<T>
    {
        T CurrentState { get; }
        void ChangeState(T state);
    }
}