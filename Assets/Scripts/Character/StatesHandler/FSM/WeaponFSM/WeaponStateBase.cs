using Behaviour;
using Behaviour.Handler;
using State.CoreFSM;
using StateData.Character;
using System.Collections.Generic;

namespace State.Character.Weapon
{
    public abstract class WeaponStateBase : IWeaponState
    {
        public WeaponStateBase(
            
            IStateMachine<WeaponStateType, IWeaponState> weaponHandler, 
            CharacterStateContext stateData, 
            IBehaviourHandler behaviourHandler)
        {
            this.weaponFSM = weaponHandler;
            this.stateData = stateData;
            this.behaviourHandler = behaviourHandler;
        }
        protected List<IUnitBehaviour> activeBehaviours = new();
        protected readonly IStateMachine<WeaponStateType, IWeaponState> weaponFSM;
        protected readonly CharacterStateContext stateData;
        protected readonly IBehaviourHandler behaviourHandler;
        public abstract void EnterState();

        public abstract void ExitState();

        public abstract void UpdateState();
        public abstract void LateUpdateState();
        public abstract void FixedUpdateState();

    }
   
}
