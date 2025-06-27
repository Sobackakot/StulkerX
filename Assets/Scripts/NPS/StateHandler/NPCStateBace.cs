
namespace State.Behaviour
{
    public abstract class NPCStateBace
    {
        public NPCStateBace(NPCStateBootstrap stateNPC)
        {
            this.stateNPC = stateNPC;
            EnterState();
        }
        ~NPCStateBace()
        {
            ExitState();
        }
        protected readonly NPCStateBootstrap stateNPC;
        public virtual void EnterState()
        {

        }

        public virtual void ExitState()
        {

        }

        public virtual void UpdateState()
        {

        }
    }
}

