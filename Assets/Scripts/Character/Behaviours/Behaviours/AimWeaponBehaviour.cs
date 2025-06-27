using Behaviour.Character;
using Behaviour.Character.Base;
using Behaviour.Handler;
using StateData.Character;
using UnityEngine;
public class AimWeaponBehaviour : BehaviourCharBase
{
    public AimWeaponBehaviour(

       CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        CharacterInputEventHandler inputEvent,
        IBehaviourHandler behaviourHandler) : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler.Register<IAimWeaponBehaviour>(this);
    } 
    public override void EnableBeh()
    { 
        AimingWeapon(stateData.isAim);
    }
    public override void DisableBeh()
    {
        AimingWeapon(stateData.isAim);
    }
    public override void UpdateBeh()
    {
    }
    public override void LateUpdateBeh()
    {
       
    }
    public override void FixedUpdateBeh()
    {
    } 
    public override void AimingWeapon(bool isAim) 
    {
        animator.AimingAnimation(isAim); 
    }
}
