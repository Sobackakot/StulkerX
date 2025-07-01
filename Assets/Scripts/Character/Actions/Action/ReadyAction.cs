
using State.CoreFSM;
using Character.Actions;
using Character.Context;
using State.Character.Battle;

public class ReadyAction : CharacterAction
{
    public ReadyAction(IStateMachine<ReadyStateType, IReadyForBattleState> fsm) : base(fsm) { }

    public override void Subscribe(IContext context)
    {
        if (context == null || fsm == null) return;
        context.onExecuteReadyTransition += fsm.Transition;
    }

    public override void Unsubscribe(IContext context)
    {
        if (context == null || fsm == null) return;
        context.onExecuteReadyTransition -= fsm.Transition;
    }
}
