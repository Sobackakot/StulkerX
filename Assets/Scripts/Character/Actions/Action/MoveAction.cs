
using Character.Actions;
using Character.Context;
using State.Character.Move;
using State.CoreFSM;
public class MoveAction : CharacterAction
{
    public MoveAction(IStateMachine<MoveStateType, IMoveState> fsm) : base(fsm) { }

    public override void Subscribe(IContext context)
    {
        context.onExecuteMoveTransition += fsm.Transition;
    }

    public override void Unsubscribe(IContext context)
    {
        context.onExecuteMoveTransition -= fsm.Transition;
    }
}
