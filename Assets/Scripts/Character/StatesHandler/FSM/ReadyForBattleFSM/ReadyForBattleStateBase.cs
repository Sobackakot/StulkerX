using Behaviour;
using Behaviour.Handler;
using Character.Context;
using State.CoreFSM;
using System.Collections.Generic;

namespace State.Character.Battle 
{
    public abstract class ReadyForBattleStateBase : IReadyForBattleState
    {
        public ReadyForBattleStateBase(
            
            IStateMachine<ReadyStateType, IReadyForBattleState> battleFSM,
            IContextStates contextStates,  
            IBehaviourHandler behaviourHandler)
        {
            this.battleFSM = battleFSM;
            this.contextStates = contextStates;
            this.behaviourHandler = behaviourHandler;
        }
        protected List<IUnitBehaviour> activeBehaviours = new();

        protected readonly IStateMachine<ReadyStateType, IReadyForBattleState> battleFSM;
        protected readonly IContextStates contextStates;
        protected readonly IBehaviourHandler behaviourHandler;
        public abstract void EnterState();

        public abstract void ExitState();

        public abstract void UpdateState();
        public abstract void LateUpdateState();
        public abstract void FixedUpdateState();
    } 
}

