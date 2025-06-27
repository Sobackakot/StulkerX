using Character.Context;
using State.CoreFSM;

namespace Character.Actions
{
    public abstract class CharacterAction : IAction<IContext>
    {
        public CharacterAction(IFSM fsm)
        {
            this.fsm = fsm;
        }
        public IFSM fsm { get; set; }
        public abstract void Subscribe(IContext context);

        public abstract void Unsubscribe(IContext context);
    }
}