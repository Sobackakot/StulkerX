using Behaviour;
using Behaviour.Handler;
using State.Character.Move;
using State.CoreFSM;
using StateData.Character;
using System.Collections.Generic;

namespace State.Character.Battle 
{
    public abstract class ReadyForBattleStateBase : IReadyForBattleState
    {
        public ReadyForBattleStateBase(
            
            IStateMachine<ReadyStateType, IReadyForBattleState> battleFSM, 
            CharacterStateContext stateData,  
            IBehaviourHandler behaviourHandler)
        {
            this.battleFSM = battleFSM;
            this.stateData = stateData;
            this.behaviourHandler = behaviourHandler;
        }
        protected List<IUnitBehaviour> activeBehaviours = new();

        protected readonly IStateMachine<ReadyStateType, IReadyForBattleState> battleFSM;
        protected readonly CharacterStateContext stateData;
        protected readonly IBehaviourHandler behaviourHandler;
        public abstract void EnterState();

        public abstract void ExitState();

        public abstract void UpdateState();
        public abstract void LateUpdateState();
        public abstract void FixedUpdateState();
    } 
}

