namespace State
{
    public interface IState
    {
        void EnterState();
        void ExitState();
        void UpdateState();
        void LateUpdateState();
        void FixedUpdateState();
    }
}

