
using Character.Actions;
using Character.Context;
using State.Character.Move;
using State.CoreFSM;
public class MoveAction : CharacterAction
{
    public MoveAction(IStateMachine<MoveStateType, IMoveState> fsm) : base(fsm) { }

    public override void Subscribe(IContextEvents context)
    {
        if (context == null || fsm == null) return;
        context.OnMovementStateChanged += fsm.SetFSM;
    }

    public override void Unsubscribe(IContextEvents context)
    {
        if (context == null || fsm == null) return;
        context.OnMovementStateChanged -= fsm.SetFSM;
    }
}
