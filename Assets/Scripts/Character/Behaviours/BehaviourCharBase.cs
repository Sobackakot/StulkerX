using Behaviour.Handler; 
using StateData.Character; 
using UnityEngine;

namespace Behaviour.Character.Base
{
    public abstract class BehaviourCharBase :
        IIdleBehaviour,
        IMoveBehaviour,
        IWalkBehaviour,
        ICrouchBehaviour,
        IRunBehaviour,
        ISprintBehaviour,
        IRotateBehaviour,
        ILeanBehaviour,
        IJumpBehaviour,
        IParkourBehaviour,
        IPickUpItemBehaviour,
        IReadyForBattleBehaviour,
        IAimWeaponBehaviour,
        IEquipWeaponBehaviour,
        IReloadWeaponBehaviour,
        IFireWeaponBehaviour

    {
        public BehaviourCharBase(
            
            CharacterInspector character,
            CharacterAnimatorInspector animator,
            CharacterStateContext stateData,
            CharacterInputEventHandler inputEvent,
            IBehaviourHandler behaviourHandler)
        {  
            this.character = character;
            this.animator = animator;
            this.stateData = stateData;
            this.inputEvent = inputEvent;
            this.behaviourHandler =  behaviourHandler; 
        }
        
        public CharacterInspector character { get; private set; }
        public CharacterAnimatorInspector animator { get; private set; }

        public CharacterStateContext stateData { get; private set; }
        public CharacterInputEventHandler inputEvent { get; private set; }

        public readonly IBehaviourHandler behaviourHandler;

        public virtual void EnableBeh() { }
        public virtual void DisableBeh() { }
        public virtual void UpdateBeh() { }
        public virtual void LateUpdateBeh() { }
        public virtual void FixedUpdateBeh() { }


        public virtual void Idling() { } 
        public virtual void RotationBehaviour(float angleRotate, Vector3 direction) { } 
   
        public virtual void MovingBehaviour(float speed, Vector3 direction) 
        { 
            character.rbCharacter.MovePosition(character.rbCharacter.position + direction * speed * Time.fixedDeltaTime);
        }
        public virtual void WalkingBehaviour(float speed, Vector3 direction) { }
        public virtual void RunningBehaviour(float speed, Vector3 direction) { }
        public virtual void SprintingBehaviour(float speed, Vector3 direction) { }
        public virtual void CrouchingBehaviour(float speed, Vector3 direction) { }
        public virtual void ReadyForBattle(bool isReady) { }
        public virtual void FiringWeapon() { }
        public virtual void ReloadingWeapon() { }
        public virtual void EquipingWeapon(bool isReady) { }

        public virtual void LeaningLeftBehaviour() { }
        public virtual void LeaningRightBehaviour() { }
        public virtual void JumpingBehaviour() { }
        public virtual void PickUpItem() { }
        public virtual void ParkouringBehaviour() { }
        public virtual void AimingWeapon(bool isAim) { }
    }
}

