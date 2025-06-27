
using State.CoreFSM;
using Character.Actions;
using Character.Context;
using State.Character.Battle;

public class ReadyAction : CharacterAction
{
    public ReadyAction(IStateMachine<ReadyStateType, IReadyForBattleState> fsm) : base(fsm) { }

    public override void Subscribe(IContext context)
    {
        context.onExecuteReadyTransition += fsm.Transition;
    }

    public override void Unsubscribe(IContext context)
    {
        context.onExecuteReadyTransition -= fsm.Transition;
    }
}
