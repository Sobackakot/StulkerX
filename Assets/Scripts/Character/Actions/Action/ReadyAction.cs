
using State.CoreFSM;
using Character.Actions;
using Character.Context;
using State.Character.Battle;

public class ReadyAction 
{
    //public ReadyAction(IStateMachine<ReadyStateType, IReadyForBattleState> fsm) : base(fsm) { }

    //public override void Subscribe(IContextEvents context)
    //{
    //    if (context == null || fsm == null) return;
    //    context.onExecuteReadyTransition += fsm.TransitionFSM;
    //}

    //public override void Unsubscribe(IContextEvents context)
    //{
    //    if (context == null || fsm == null) return;
    //    context.onExecuteReadyTransition -= fsm.TransitionFSM;
    //}
}
