
using NPC.Behaviour;

namespace NPC.FSM
{
    public abstract class NPCMoveFSMBase : IMoveFSM
    {     
        public NPCMoveFSMBase(
            NPCMoveStateHandler moveHandler,
            Behaviour.IBehaviourHandler cicleBeh)
        { 
            this.cicleBeh = cicleBeh;
            this.moveHandler = moveHandler;
        } 
        public readonly Behaviour.IBehaviourHandler cicleBeh;

        protected readonly NPCMoveStateHandler moveHandler;
        public abstract void EnterState(); 
        public abstract void ExitState(); 
        public virtual void UpdateState() { }
        public virtual void LateUpdateState() { }
        public virtual void FixedUpdateState() { }
    }
}

