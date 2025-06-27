using State.CoreFSM;

namespace State.Character.Battle
{
    public class ReadyForBattleStateHandler: StateMachine<ReadyStateType, IReadyForBattleState>
    { 
    }
    public enum ReadyStateType
    {
        Ready, 
        None
    }
    public interface IReadyForBattleState : IState
    {
    }
}