using Character.Context;
using State.Character.Move;
using State.CoreFSM;

namespace Character.Actions
{
    public abstract class CharacterAction : IAction<IContextEvents>
    {
        public CharacterAction(IStateMachine<MoveStateType, IMoveState> fsm)
        {
            this.fsm = fsm;
        }
        public IStateMachine<MoveStateType, IMoveState> fsm { get; set; }
        public abstract void Subscribe(IContextEvents context);

        public abstract void Unsubscribe(IContextEvents context);
    }
}