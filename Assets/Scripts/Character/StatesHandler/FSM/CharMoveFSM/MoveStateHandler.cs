using State.CoreFSM;

namespace State.Character.Move
{
    public class MoveStateHandler: StateMachine<MoveStateType, IMoveState>
    { 
        
    }
    public enum MoveStateType
    {
        Idle,
        Walk,
        Run,
        Sprint,
        Crouch,
        Aim
    }
    public interface IMoveState : IState
    {
    }
}

