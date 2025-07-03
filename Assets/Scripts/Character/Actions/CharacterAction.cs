using Character.Context;
using State.CoreFSM;

namespace Character.Actions
{
    public abstract class CharacterAction : IAction<IContextEvents>
    {
        public CharacterAction(IFSM fsm)
        {
            this.fsm = fsm;
        }
        public IFSM fsm { get; set; }
        public abstract void Subscribe(IContextEvents context);

        public abstract void Unsubscribe(IContextEvents context);
    }
}