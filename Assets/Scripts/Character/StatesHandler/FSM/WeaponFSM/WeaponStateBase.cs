using Behaviour;
using Behaviour.Handler;
using Character.Context;
using State.CoreFSM;
using System.Collections.Generic;

namespace State.Character.Weapon
{
    public abstract class WeaponStateBase : IWeaponState
    {
        public WeaponStateBase(
            
            IStateMachine<WeaponStateType, IWeaponState> weaponHandler,
            IContextStates contextStates, 
            IBehaviourHandler behaviourHandler)
        {
            this.weaponFSM = weaponHandler;
            this.contextStates = contextStates;
            this.behaviourHandler = behaviourHandler;
        }
        protected List<IUnitBehaviour> activeBehaviours = new();
        protected readonly IStateMachine<WeaponStateType, IWeaponState> weaponFSM;
        protected readonly IContextStates contextStates;
        protected readonly IBehaviourHandler behaviourHandler;
        public abstract void EnterState();

        public abstract void ExitState();

        public abstract void UpdateState();
        public abstract void LateUpdateState();
        public abstract void FixedUpdateState();

    }
   
}
