using Behaviour;
using Behaviour.Handler;
using Character.Context;
using State.CoreFSM;
using StateData.Character;
using System.Collections.Generic;

namespace State.Character.Move
{
    public abstract class MoveStateBase: IMoveState
    {
        public MoveStateBase(
            IStateMachine<MoveStateType, IMoveState> moveFSM,
            IContextStates contextStates, 
            IBehaviourHandler behaviourHandler)
        {
            this.moveFSM = moveFSM;
            this.contextStates = contextStates;
            this.behaviourHandler = behaviourHandler;
        }
        protected List<IUnitBehaviour> activeBehaviours = new();

        protected readonly IStateMachine<MoveStateType, IMoveState> moveFSM; 
        protected readonly IContextStates contextStates; 
        protected readonly IBehaviourHandler behaviourHandler; 
        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState(); 
        public abstract void LateUpdateState(); 
        public abstract void FixedUpdateState(); 
         
    } 
}

